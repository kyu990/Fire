namespace MobleFinalServer.Service
{
    public class FilePath
    {
        public static readonly string VideoPath = GetVideoPath();
        public static readonly string PicturePath = GetPicturePath();
        private static string GetVideoPath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\MobleFinal\VideoLog"));
        }

        private static string GetPicturePath()
        {
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			return Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\MobleFinal\ProfileImage"));
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