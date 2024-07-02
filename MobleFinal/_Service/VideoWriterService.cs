using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using OpenCvSharp;

namespace MobleFinal._Service
{
    internal class VideoWriterService
    {
        public BlockingCollection<Mat> frameBuffer;
        private CancellationTokenSource captureCts;
        private Task captureTask;
        private Task writeTask;

        public VideoWriterService()
        {
            frameBuffer = new BlockingCollection<Mat>(new ConcurrentQueue<Mat>(), 100);
            captureCts = new CancellationTokenSource();
            Info.frameBuffer = frameBuffer;
        }

        public void StartCaptureAndStreaming(string outputDir, int frameWidth, int frameHeight, double fps)
        {
            //captureTask = Task.Run(() => CaptureVideo(captureCts.Token));
            writeTask = Task.Run(() => StreamToHLS(outputDir, frameWidth, frameHeight, fps));
        }

        public void StopCaptureAndStreaming()
        {
            captureCts.Cancel();
            captureTask.Wait();
            frameBuffer.CompleteAdding();
            writeTask.Wait();
        }

        private void CaptureVideo(CancellationToken cancellationToken)
        {
            using VideoCapture capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                Console.WriteLine("Camera not opened.");
                return;
            }

            Mat frame = new Mat();

            while (!cancellationToken.IsCancellationRequested)
            {
                capture.Read(frame);
                if (frame.Empty())
                {
                    continue;
                }

                frameBuffer.Add(frame.Clone());
                Cv2.ImShow("Frame", frame);
                if (Cv2.WaitKey(1) == 27) // ESC 키를 누르면 종료
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

            capture.Release();
            Cv2.DestroyAllWindows();
        }

        private void StreamToHLS(string outputDir, int frameWidth, int frameHeight, double fps)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string ffmpegPath = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\bin\ffmpeg.exe"));

            string arguments = $"-f rawvideo -pix_fmt bgr24 -s {frameWidth}x{frameHeight} -r {fps} -i - " +
                               $"-c:v libx264 -f hls -hls_time 1 -hls_list_size 3 -hls_flags delete_segments " +
                               $"{Path.Combine(outputDir, "playlist.m3u8")}";

            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = ffmpegPath;
            ffmpeg.StartInfo.Arguments = arguments;
            ffmpeg.StartInfo.RedirectStandardInput = true;
            ffmpeg.StartInfo.UseShellExecute = false;
            ffmpeg.StartInfo.CreateNoWindow = true;

            ffmpeg.Start();

            using (var stream = ffmpeg.StandardInput.BaseStream)
            {
                foreach (var frame in frameBuffer.GetConsumingEnumerable())
                {
                    byte[] data = new byte[frame.Total() * frame.ElemSize()];
                    System.Runtime.InteropServices.Marshal.Copy(frame.Data, data, 0, data.Length);
                    stream.Write(data, 0, data.Length);
                    frame.Dispose();
                }
            }

            ffmpeg.WaitForExit();
        }
    }
}
