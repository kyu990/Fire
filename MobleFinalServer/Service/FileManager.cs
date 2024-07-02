namespace MobleFinalServer.Service
{
    public class FileManager
    {
        private static readonly string VideoPath = GetVideoPath();

        private static string GetVideoPath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\MobleFinal\VideoLog"));
        }

        public async Task<List<FileInfo>> GetVideoListAsync()
        {
            var files = new List<FileInfo>();
            var directory = new DirectoryInfo(VideoPath);

            foreach (var file in directory.GetFiles())
            {
                files.Add(file);
            }

            return await Task.FromResult(files);
        }
    }
}