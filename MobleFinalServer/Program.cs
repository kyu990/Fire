using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MobleFinalServer.Models;
using MobleFinalServer.Repository;
using MobleFinalServer.Service;
using System.Reflection.Metadata.Ecma335;

namespace MobleFinalServer
{
    public class Program
    {
        public static int HttpsExternalPort;
        public static int HttpsInternalPort;
        public static int HlsPort;
        public static int TcpPort;
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ���� ���
            builder.Services.AddControllersWithViews();

            // CORS ��å �߰�
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

			builder.Services.Configure<FormOptions>(options =>
			{
				options.MultipartBodyLengthLimit = 1024*1000*10; // ��: 256 MB
			});

			// IHttpContextAccessor ���
			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // ���� Ȱ��ȭ
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // SignalR Ȱ��ȭ
            builder.Services.AddSignalR();

            // DB ���� ����, appsetting.json�� ���ǵǾ� �ִ� DB �ּ� ����
            builder.Services.AddSingleton<UserRepository>(options =>
            {
                var configuration = options.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new UserRepository(connectionString);
            });

            builder.Services.AddSingleton<SensorRepository>(options =>
            {
                var configuration = options.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new SensorRepository(connectionString);
            });

            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(5);
            });

            builder.Services.AddSingleton<EmailSender>(options =>
            {
                var logger = options.GetRequiredService<ILogger<EmailSender>>();
                return new EmailSender("codingtest99@naver.com", "coding1231", logger);
            });

            HttpsExternalPort = int.Parse(builder.Configuration["HttpsExternalPort"]);
            HttpsInternalPort = int.Parse(builder.Configuration["HttpsInternalPort"]);
            HlsPort = int.Parse(builder.Configuration["HlsPort"]);
            TcpPort = int.Parse(builder.Configuration["TcpPort"]);

            // Tcp ���� Ȱ��ȭ
            builder.Services.AddSingleton<TcpServer>(options =>
            {
                var hub = options.GetRequiredService<IHubContext<SensorHub>>();
                var logger = options.GetRequiredService<ILogger<TcpServer>>();
                return new TcpServer(TcpPort, hub, logger);
            });

            builder.Services.AddHostedService<TcpServerHostedService>();

            var app = builder.Build();

            // HTTP ��û ���������� ����
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapHub<SensorHub>("/sensorhub");

            app.UseCors("CorsPolicy");

            await PortForwarding.StopPortForwardingAsync();
            PortForwarding.StartPortForwarding();

            app.Run();
        }
    }
}