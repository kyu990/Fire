using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobleFinal._Service
{
    internal class FileManage
    {
        public static string BinPath = GetBinDirectory();
        public static string VideoPath = GetVideoPath();
        public static string GetBinDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\bin\"));
        }

        public List<FileInfo> GetVideoList()
        {
            List<FileInfo> files = new List<FileInfo>();

            DirectoryInfo directory = new DirectoryInfo(VideoPath);

            foreach (FileInfo file in directory.GetFiles())
            {
                files.Add(file);
            }
            return files;
        }
        public string GetFFMPEGPath(string relativePath)
        {
            return Path.GetFullPath(Path.Combine(BinPath, "ffmpeg.exe"));
        }

        public static string GetVideoPath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\VideoLog\"));
        }

        public void VideoSave()
        {

        }
    }
}
