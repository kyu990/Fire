using MobleFinal._Service;

namespace MobleFinal
{
    internal static class Program
    {
        public static SocketService socketService;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.


            /// <summary>
            /// HLS ����
            /// </summary>
            HlsService hlsService = new HlsService();
            //await hlsService.VlcControllerAsync();
            Task.Run(async () => await hlsService.VlcControllerAsync());

            socketService = new SocketService();


            ///<summary>
            /// TCP ��� (�Ƶ��̳� <-> ����)
            /// </summary>
            Console.WriteLine("Starting server...");
            socketService.Arduino();

            Console.WriteLine("Press Enter to stop the server.");

            /// <summary>
            /// ���� ����
            /// </summary>
            ApplicationConfiguration.Initialize();
            LoginForm loginForm = new LoginForm();
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(loginForm);

            Console.WriteLine("Stopping server...");
            socketService.StopServer();

            Console.WriteLine("Server stopped.");
        }
    }
}