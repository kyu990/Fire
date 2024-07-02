using Microsoft.AspNetCore.Mvc;
using MobleFinalServer.Models;
using MobleFinalServer.Repository;
using MobleFinalServer.Service;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace MobleFinalServer.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { 2 })]
    public class HistoryController : Controller
    {
        private readonly SensorRepository _sensorRepository;
        private readonly ILogger<HistoryController> _logger;
        private const int pageSize = 10;
        private readonly FilePath _fileManager;
        private static readonly ConcurrentBag<History> histories = new();

        public HistoryController(SensorRepository sensorRepository, ILogger<HistoryController> logger)
        {
            _sensorRepository = sensorRepository;
            _logger = logger;
            _fileManager = new FilePath();
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            string serial = HttpContext.Session.GetString("UserClient");

            var sensors = await _sensorRepository.GetSensorDataByTimeAsync(serial, DateTime.Now, DateTime.Now);

            var files = await _fileManager.GetVideoListAsync();

            var historiesPage = files.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(file => new History
            {
                Id = histories.Count + 1,
                ClientSerial = serial,
                VideoName = file.Name,
                VideoPath = $"/History/StreamVideo?filePath={Uri.EscapeDataString(file.FullName)}", // 경로 인코딩
                Time = file.LastWriteTime,
                IsFire = false,
                SensorData = sensors
            }).ToList();

            foreach (var history in historiesPage)
            {
                histories.Add(history);
            }

            var viewModel = new HistoryView
            {
                Histories = historiesPage,
                CurrentPage = pageNumber,
                TotalPage = (int)Math.Ceiling(files.Count / (double)pageSize)
            };

            return View(viewModel);
        }

        public IActionResult ViewHistory(string videoName)
        {
            var history = histories.FirstOrDefault(h => h.VideoName == videoName);
            if (history == null)
            {
                return NotFound();
            }
            return View(history);
        }

        public async Task<IActionResult> StreamVideo(string filePath)
        {
            filePath = Uri.UnescapeDataString(filePath);
            _logger.LogInformation("Requested file path: {FilePath}", filePath);

            if (System.IO.File.Exists(filePath))
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "video/mp4", Path.GetFileName(filePath));
            }
            _logger.LogError("File not found: {FilePath}", filePath);
            return NotFound();
        }
    }
}
