using System;
using OpenCvSharp;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using System.Collections.Concurrent;
using OpenCvSharp.Features2D;
using System.Text.RegularExpressions;
using Mysqlx.Notice;
using OpenCvSharp.XFeatures2D;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using System.Security.Cryptography.Xml;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using MobleFinal._Service;
using FFmpeg.AutoGen;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LiveStreaming
{
    internal class LiveStream
    {
        public FileManage fileManage;
        public bool bCameraActive;
        private VideoCapture cap1;
        private VideoCapture cap2;
        private PictureBox pictureBox1;
        Mat now = new Mat();
        Mat previous = new Mat();
        Mat LightFrame = new Mat();
        public bool bFireDetection;
        private DateTime lastDetectionTime;
        private TimeSpan detectionCooldown = TimeSpan.FromSeconds(0.5);
        private Mat homography;
        private double[] dm_hm_homo = new double[9];
        private double[] m_dIdentityMatrix = new double[9] { 1, 0, 0, 0, 1, 0, 0, 0, 1 };
        private Mat m_hm = new Mat(3, 3, MatType.CV_64FC1);
        private Mat m_hmExpLow;
        private Mat m_hmExpHigh;
        private System.Timers.Timer recordTimer;
        private ConcurrentQueue<Mat> frameBuffer = new ConcurrentQueue<Mat>();
        private int frameRate = 15;
        private string videoFileName;
        private int iRecordLength = 60;
        private string saveDirectory;
        private SMSService SMSService;
        private bool smsSent = false;

        struct stBlobInfo
        {
            public OpenCvSharp.Rect rt;
            public Scalar sMean;
        }

        ConcurrentQueue<stBlobInfo> m_queDetectionRect = new ConcurrentQueue<stBlobInfo>();


        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder reVal, int size, string filepath);

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        public LiveStream(PictureBox pictureBox1)
        {
            fileManage = new FileManage();
            cap2 = new VideoCapture(1, VideoCaptureAPIs.DSHOW);
            bCameraActive = false;
            this.pictureBox1 = pictureBox1;
            //this.pictureBox2 = pictureBox2;
            cap1 = new VideoCapture(0, VideoCaptureAPIs.DSHOW);
            saveDirectory = FileManage.VideoPath;
            SMSService = new SMSService();

            cap1.FrameHeight = pictureBox1.Height;
            cap1.FrameWidth = pictureBox1.Width;
            cap2.FrameHeight = pictureBox1.Height;
            cap2.FrameWidth = pictureBox1.Width;

            Thread thMotionDetection = new Thread(new ThreadStart(delegate () { Thread_MotionDetection(); }));
            thMotionDetection.IsBackground = true;
            thMotionDetection.Start();
            Thread thStopDetection = new Thread(new ThreadStart(delegate () { Thread_StopDetection(); }));
            thStopDetection.IsBackground = true;
            thStopDetection.Start();
            //---------------------------------------------------------------------------
            //Task.Run(async () => await Thread_Message());
            //---------------------------------------------------------------------------
            InitializeRecordTimer();
            loadHomographyData();
        }

        public void CameraOn()
        {
            if (bCameraActive) return;
            bCameraActive = true;

            cap1.Set(VideoCaptureProperties.Exposure, -8);
            cap2.Set(VideoCaptureProperties.Exposure, -6);
            recordTimer.Start();
            var thread = new Thread(() =>
            {

                //카메라 노출값 조절
                while (bCameraActive)
                {
                    //원본 = now, 기능 구현 = firemask
                    cap1.Read(previous);
                    cap1.Read(now);
                    cap2.Read(LightFrame);

                    Mat motionMask = DrawDetection();
                    Info.frameBuffer.Add(motionMask.Clone());

                    if (!motionMask.Empty())
                    {
                        //Cv2.PutText(motionMask, $"{bFireDetection}", new OpenCvSharp.Point(100, 100), HersheyFonts.HersheySimplex, 1, new Scalar(0, 0, 255));
                        ShowImageOnPictureBox(motionMask);
                        frameBuffer.Enqueue(motionMask.Clone());

                        while (frameBuffer.Count > frameRate * iRecordLength)
                        {
                            frameBuffer.TryDequeue(out _); // 버퍼 크기가 녹화 시간보다 크지 않도록 보장
                        }
                        //Cv2.ImShow("now", now);
                        //Cv2.ImShow("LighFrame", LightFrame);
                        //Cv2.WaitKey(1);
                        // Update previous frame
                        now.CopyTo(previous);
                    }
                }

            });
            thread.IsBackground = true;
            thread.Start();
        }

        public void CameraOff()
        {
            if (!bCameraActive) return;

            bCameraActive = false;
            recordTimer.Stop();
            SaveBufferedFrames();
            pictureBox1.Image = null;
        }

        private void ShowImageOnPictureBox(Mat image)
        {
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new Action(() =>
                {
                    if (!pictureBox1.IsDisposed)
                    {
                        //Mat 객체를 Bitmap으로 변환하여 픽쳐박스에 할당
                        pictureBox1.Image = BitmapConverter.ToBitmap(image);
                    }
                }));
            }
            else
            {
                if (pictureBox1 != null && !pictureBox1.IsDisposed)
                {
                    pictureBox1.Image = BitmapConverter.ToBitmap(image);
                }
            }
        }

        //카메라 타이머 초기화 메서드
        private void InitializeRecordTimer()
        {
            //iRecordLength == 영상 길이(초단위)
            recordTimer = new System.Timers.Timer(iRecordLength * 1000); // Set timer to trigger after recordingDurationSeconds seconds
            recordTimer.Elapsed += OnRecordTimerElapsed;
            recordTimer.AutoReset = false; // Ensure the timer triggers only once
        }

        private void OnRecordTimerElapsed(object sender, ElapsedEventArgs e)
        {
            SaveBufferedFrames();
            frameBuffer.Clear(); // 저장 후 버퍼 비우기
            bFireDetection = false;// 저장 후 다시 falsed로 초기화
            if (bCameraActive)
            {
                recordTimer.Start(); // 카메라가 여전히 활성화되어 있으면 타이머 다시 시작
            }
        }
        //영상 저장 
        private void SaveBufferedFrames()
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string videoFilePath = "";
            string videoFileName = "";
            //true일 경우와 true가 아닐 경우의 저장 이름(현재 시간을 기준으로 저장)
            if (bFireDetection)
            {
                videoFileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_fire.mp4";
                videoFilePath = Path.Combine(saveDirectory, videoFileName);
            }
            else
            {
                videoFileName = $"{DateTime.Now:yyyyMMdd_HHmmss}.mp4";
                videoFilePath = Path.Combine(saveDirectory, videoFileName);
            }

            using (var videoWriter = new VideoWriter(videoFilePath, FourCC.AVC, frameRate, new OpenCvSharp.Size(1280, 720), false))
            {
                if (!videoWriter.IsOpened())
                {
                    throw new Exception("Failed to open video writer.");
                }
                while (frameBuffer.TryDequeue(out Mat frame))
                {
                    videoWriter.Write(frame);
                }
            }

            string password = AesEncryption.GeneratePassword();
            AesEncryption.EncryptFile(videoFilePath, videoFilePath + "_aes", password);

            //mp4 삭제하는 로직
            //try
            //{
            //    File.Delete(videoFilePath);
            //    Console.WriteLine($"Deleted original MP4 file: {videoFilePath}");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Error deleting original MP4 file: {e.Message}");
            //}

            SqliteHandler handler = new SqliteHandler();
            handler.InsertVideoData(videoFileName+"_aes", videoFilePath+"_aes", password);
        }

        public void Thread_MotionDetection()
        {
            double minBrightThresholdValue = 230; // 밝기 최소 임계값
            double maxBrightThresholdValue = 255;
            TimeSpan detectionCooldown = TimeSpan.FromSeconds(0.5); // 0.5초 쿨다운 (초당 2회)
            DateTime lastDetectionTime = DateTime.MinValue;
            while (true)
            {
                //if ((DateTime.Now - lastDetectionTime) < detectionCooldown)
                //{
                //    continue; // 쿨다운 기간 동안 감지 건너뜁니다.
                //}
                if (now.Empty() == false && previous.Empty() == false)
                {

                    Mat grayPrevious = previous.CvtColor(ColorConversionCodes.BGR2GRAY);
                    Mat grayNow = now.CvtColor(ColorConversionCodes.BGR2GRAY);


                    // 밝은 영역 검출
                    //double brightThresholdValue = 200; //밝기 임계값
                    Mat brightAreaMask = new Mat();

                    //Cv2.Threshold(grayNow, brightAreaMask, brightThresholdValue, 255, ThresholdTypes.Binary);
                    Cv2.InRange(grayNow, new Scalar(minBrightThresholdValue), new Scalar(maxBrightThresholdValue), brightAreaMask);

                    // 밝은 영역에 대한 마스크 적용
                    Mat maskedGrayPrevious = new Mat();
                    Mat maskedGrayNow = new Mat();
                    grayPrevious.CopyTo(maskedGrayPrevious, brightAreaMask);
                    grayNow.CopyTo(maskedGrayNow, brightAreaMask);

                    // 프레임 간 차이 계산 (밝은 영역에 대해서만)
                    Mat frameDiff = new Mat();
                    Cv2.Absdiff(maskedGrayPrevious, maskedGrayNow, frameDiff);

                    // 차이 프레임 이진화
                    Mat motionMask = new Mat();
                    double motionThresholdValue = 5; // 움직임 감지 임계값
                    Cv2.Threshold(frameDiff, motionMask, motionThresholdValue, 255, ThresholdTypes.Binary);

                    // 노이즈 제거 (모폴로지 연산)
                    Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
                    Cv2.MorphologyEx(motionMask, motionMask, MorphTypes.Open, kernel);
                    Cv2.MorphologyEx(motionMask, motionMask, MorphTypes.Close, kernel);

                    //윤곽선 검출 Point[][] contours == 좌표가 4개있다 1,2,3,4(위쪽부터 아래쪽으로 왼쪽부터)
                    Cv2.FindContours(motionMask, out OpenCvSharp.Point[][] contours, out HierarchyIndex[] hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                    //Mat resultFrame = now.Clone();
                    OpenCvSharp.Point[][] jj = contours;
                    //만약 검출이 되었다면
                    if (jj != null)
                    {
                        //좌표 2 by 2 좌표를 4개로 쪼개는 과정 
                        foreach (OpenCvSharp.Point[] jjs in jj)
                        {
                            //jjs[0].X -= 50;
                            //jjs[0].Y -= 50;
                            //jjs[1].X += 50;
                            //jjs[1].Y -= 50;
                            //jjs[2].X -= 50;
                            //jjs[2].Y += 50;
                            //jjs[3].X += 50;
                            //jjs[3].Y += 50;

                            //4개의 좌표로 4각형 바운딩 박스
                            Rect boundingBox = Cv2.BoundingRect(jjs);

                            //Concurrentque 생성(구조체임, 각각의 스레드에서 접근하기 위한 new 생성자로 생성)
                            stBlobInfo BlobInfo = new stBlobInfo();

                            //2차원 배열 생성
                            Point2f[] ptBound = new Point2f[4];

                            //사각형의 좌표 왼쪽 위, 오른쪽 위, 왼쪽 아래, 오른쪽 아래
                            ptBound[0] = new Point2f(boundingBox.Left, boundingBox.Top);
                            ptBound[1] = new Point2f(boundingBox.Right, boundingBox.Top);
                            ptBound[2] = new Point2f(boundingBox.Left, boundingBox.Bottom);
                            ptBound[3] = new Point2f(boundingBox.Right, boundingBox.Bottom);

                            double[] dataHM = new double[m_hm.Total() * m_hm.Channels()];
                            m_hm.GetArray(out dataHM);

                            float[] fDistortion = new float[4];

                            for (int n = 0; n < 4; n++)
                            {
                                ptBound[n].X = ptBound[n].X * (float)dataHM[0] + ptBound[n].Y * (float)dataHM[1] + (float)dataHM[2];
                                ptBound[n].Y = ptBound[n].X * (float)dataHM[3] + ptBound[n].Y * (float)dataHM[4] + (float)dataHM[5];
                                fDistortion[n] = (float)dataHM[6] + (float)dataHM[7] + (float)dataHM[8];
                            }

                            boundingBox = new Rect((int)ptBound[0].X, (int)ptBound[0].Y, (int)ptBound[3].X - (int)ptBound[0].X, (int)ptBound[3].Y - (int)ptBound[0].Y);

                            if (boundingBox.X < 0) boundingBox.X = 0;
                            if (boundingBox.Y < 0) boundingBox.Y = 0;
                            if (boundingBox.X > now.Cols - 2) boundingBox.X = now.Cols - 2;
                            if (boundingBox.Y > now.Rows - 2) boundingBox.Y = now.Rows - 2;
                            if (boundingBox.X + boundingBox.Width > now.Cols) boundingBox.Width = now.Cols - boundingBox.X - 1;
                            if (boundingBox.Y + boundingBox.Height > now.Rows) boundingBox.Height = now.Rows - boundingBox.Y - 1;
                            BlobInfo.rt = boundingBox;

                            Mat mtRoi = new Mat(grayNow, boundingBox);

                            BlobInfo.sMean = Cv2.Mean(now, motionMask);

                            m_queDetectionRect.Enqueue(BlobInfo);
                        }
                    }
                }
            }
        }



        public Mat CalculateHomography(OpenCvSharp.Point[] srcPoints, OpenCvSharp.Point[] dstPoints)
        {
            if (srcPoints.Length == 4 && dstPoints.Length == 4)
            {
                using (var srcArray = InputArray.Create(srcPoints))
                using (var dstArray = InputArray.Create(dstPoints))
                {
                    Mat homography = Cv2.FindHomography(srcArray, dstArray, HomographyMethods.Ransac);
                    return homography;
                }
            }
            else
            {
                throw new ArgumentException("Both srcPoints and dstPoints must contain exactly 4 points.");
            }
        }



        public void Thread_StopDetection()
        {
            double minBrightThresholdValue = 236; // 밝기 최소 임계값
            double maxBrightThresholdValue = 248;
            TimeSpan detectionCooldown = TimeSpan.FromSeconds(0.005); // 0.5초 쿨다운 (초당 2회)
            DateTime lastDetectionTime = DateTime.MinValue;
            while (true)
            {
                //if ((DateTime.Now - lastDetectionTime) < detectionCooldown)
                //{
                //    continue; // 쿨다운 기간 동안 감지 건너뜁니다.
                //}
                if (now.Empty() == false && previous.Empty() == false)
                {
                    Mat grayNow = now.CvtColor(ColorConversionCodes.BGR2GRAY);

                    // 밝은 영역 검출
                    Mat brightAreaMask = new Mat();
                    Cv2.InRange(grayNow, new Scalar(minBrightThresholdValue), new Scalar(maxBrightThresholdValue), brightAreaMask);
                    OpenCvSharp.Point[][] contours;
                    HierarchyIndex[] hierarchy;
                    Cv2.FindContours(brightAreaMask, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
                    OpenCvSharp.Point[][] ContoursPoint = contours;
                    int f = 1;
                    Cv2.ImWrite($"  2{f}.jpg", brightAreaMask);
                    if (ContoursPoint != null)
                    {
                        foreach (OpenCvSharp.Point[] contour in ContoursPoint)
                        {
                            if (contour.Length >= 4) // 유효한 인덱스 범위 검사 추가
                            {
                                contour[0].X -= 50;
                                contour[0].Y -= 50;
                                contour[1].X += 50;
                                contour[1].Y -= 50;
                                contour[2].X -= 50;
                                contour[2].Y += 50;
                                contour[3].X += 50;
                                contour[3].Y += 50;

                                Rect boundingBox = Cv2.BoundingRect(contour);
                                stBlobInfo BlobInfo = new stBlobInfo();

                                Point2f[] ptBound = new Point2f[4];
                                ptBound[0] = new Point2f(boundingBox.Left, boundingBox.Top);
                                ptBound[1] = new Point2f(boundingBox.Right, boundingBox.Top);
                                ptBound[2] = new Point2f(boundingBox.Left, boundingBox.Bottom);
                                ptBound[3] = new Point2f(boundingBox.Right, boundingBox.Bottom);

                                double[] dataHM = new double[m_hm.Total() * m_hm.Channels()];
                                m_hm.GetArray(out dataHM);

                                float[] fDistortion = new float[4];

                                for (int n = 0; n < 4; n++)
                                {
                                    ptBound[n].X = ptBound[n].X * (float)dataHM[0] + ptBound[n].Y * (float)dataHM[1] + (float)dataHM[2];
                                    ptBound[n].Y = ptBound[n].X * (float)dataHM[3] + ptBound[n].Y * (float)dataHM[4] + (float)dataHM[5];
                                    fDistortion[n] = (float)dataHM[6] + (float)dataHM[7] + (float)dataHM[8];
                                }

                                boundingBox = new Rect((int)ptBound[0].X, (int)ptBound[0].Y, (int)ptBound[3].X - (int)ptBound[0].X, (int)ptBound[3].Y - (int)ptBound[0].Y);



                                if (boundingBox.X < 0) boundingBox.X = 0;
                                if (boundingBox.Y < 0) boundingBox.Y = 0;
                                if (boundingBox.X > now.Cols - 2) boundingBox.X = now.Cols - 2;
                                if (boundingBox.Y > now.Rows - 2) boundingBox.Y = now.Rows - 2;
                                if (boundingBox.X + boundingBox.Width > now.Cols) boundingBox.Width = now.Cols - boundingBox.X - 1;
                                if (boundingBox.Y + boundingBox.Height > now.Rows) boundingBox.Height = now.Rows - boundingBox.Y - 1;
                                BlobInfo.rt = boundingBox;
                                Mat mtRoi = new Mat(grayNow, boundingBox);
                                BlobInfo.sMean = Cv2.Mean(now, brightAreaMask);
                                m_queDetectionRect.Enqueue(BlobInfo);
                            }
                        }
                    }
                }
            }
        }
        public Mat DrawDetection()
        {
            Mat mtDraw = LightFrame.Clone();
            while (m_queDetectionRect.TryDequeue(out stBlobInfo BlobInfo))
            {
                bFireDetection = true;
                Cv2.Rectangle(mtDraw, BlobInfo.rt, Scalar.Blue, 2);
                //Cv2.PutText(mtDraw, $"R:{BlobInfo.sMean.Val0:000} G: {BlobInfo.sMean.Val1:000} B:{BlobInfo.sMean.Val2:000}", new OpenCvSharp.Point(BlobInfo.rt.X, BlobInfo.rt.Y), HersheyFonts.HersheySimplex, 1, new Scalar(0, 0, 255));
            }
            return mtDraw;
        }

        //여기 주석 풀면 메세지 전송 됨 ---------------------------------------------------
        public async Task Thread_Message()
        {
            while (true)
            {
                if (bFireDetection && !smsSent)
                {
                    await Task.Run(() => SMSService.SendSMS());
                    smsSent = true;
                }
                await Task.Delay(1000);
            }
        }
        //------------------------------------------------------------------------------

        private void loadHomographyData()
        {
            double[] dVal = new double[9];
            for (int i = 0; i < 9; i++)
            {
                StringBuilder sb = new StringBuilder(255);
                GetPrivateProfileString("homography", $"m_hm{i}", m_dIdentityMatrix[i].ToString(), sb, 255, FileManage.BinPath + "Homography2.ini");
                dVal[i] = Convert.ToDouble(sb.ToString());
            }
            m_hm.SetArray(dVal);
        }

        public void Grab()
        {
            Cv2.ImWrite(FileManage.BinPath + "Exposure_LOW6.bmp", now);
            Cv2.ImWrite(FileManage.BinPath + "Exposure_HIGH6.bmp", LightFrame);
        }

        public void Calibration()
        {
            // 첫 번째 이미지를 로드
            Mat mtExpLow = Cv2.ImRead(FileManage.BinPath + "Exposure_LOW6.bmp", ImreadModes.Color);
            // 두 번째 이미지를 로드
            Mat mtExpHigh = Cv2.ImRead(FileManage.BinPath + "Exposure_HIGH6.bmp", ImreadModes.Color);

            Mat mtExpLow_Gray = mtExpLow.CvtColor(ColorConversionCodes.BGR2GRAY);
            Mat mtExpHigh_Gray = mtExpHigh.CvtColor(ColorConversionCodes.BGR2GRAY);


            SimpleBlobDetector.Params ExpLowParam = new SimpleBlobDetector.Params
            {
                ThresholdStep = 2,
                MinThreshold = 0,
                MaxThreshold = 23,
                FilterByArea = true,
                MinArea = 30,
                FilterByCircularity = true,
                MinCircularity = 0.85f,
                FilterByConvexity = false,
                MinConvexity = 0.87f,
                FilterByInertia = false,
                MinInertiaRatio = 0.01f
            };

            SimpleBlobDetector.Params ExpHighParam = new SimpleBlobDetector.Params
            {
                ThresholdStep = 10,
                MinThreshold = 0,
                MaxThreshold = 95,
                FilterByArea = true,
                MinArea = 30,
                FilterByCircularity = true,
                MinCircularity = 0.8f,
                FilterByConvexity = false,
                MinConvexity = 0.87f,
                FilterByInertia = false,
                MinInertiaRatio = 0.01f
            };

            var detectorExpLow = SimpleBlobDetector.Create(ExpLowParam);
            var detectorExpHigh = SimpleBlobDetector.Create(ExpHighParam);

            KeyPoint[] keypointsExpLow = detectorExpLow.Detect(mtExpLow_Gray);
            KeyPoint[] keypointsExpHigh = detectorExpHigh.Detect(mtExpHigh_Gray);

            Mat mtDrawExpLow = mtExpLow.Clone();
            for (int n = 0; n < keypointsExpLow.Length; n++)
                Cv2.Circle(mtDrawExpLow, new OpenCvSharp.Point(keypointsExpLow[n].Pt.X, keypointsExpLow[n].Pt.Y), 5, new Scalar(0, 0, 255));
            Mat mtDrawExpHigh = mtExpHigh.Clone();
            for (int n = 0; n < keypointsExpHigh.Length; n++)
                Cv2.Circle(mtDrawExpHigh, new OpenCvSharp.Point(keypointsExpHigh[n].Pt.X, keypointsExpHigh[n].Pt.Y), 5, new Scalar(0, 0, 255));
            Cv2.ImShow("ExpLow", mtDrawExpLow);
            Cv2.ImShow("Exphigh", mtDrawExpHigh);

            if (keypointsExpLow.Length < 4 || keypointsExpHigh.Length < 4) return;


            var sortedKeypointsExpLow = keypointsExpLow.OrderBy(kp => kp.Pt.Y * 2 + kp.Pt.X).ToArray();
            var sortedKeypointsExpHigh = keypointsExpHigh.OrderBy(kp => kp.Pt.Y * 2 + kp.Pt.X).ToArray();

            //// 매칭된 점들로부터 호모그라피 계산
            Point2f[] ptExpLow_px = new Point2f[4] { sortedKeypointsExpLow[0].Pt, sortedKeypointsExpLow[1].Pt, sortedKeypointsExpLow[2].Pt, sortedKeypointsExpLow[3].Pt };
            Point2f[] ptExpLow_mm = new Point2f[4] { new Point2f(0, 0), new Point2f((float)14.1, 0), new Point2f(0, (float)14.1), new Point2f((float)14.1, (float)14.1) };
            Point2f[] ptExpHigh_px = new Point2f[4] { sortedKeypointsExpHigh[0].Pt, sortedKeypointsExpHigh[1].Pt, sortedKeypointsExpHigh[2].Pt, sortedKeypointsExpHigh[3].Pt };
            Point2f[] ptExpHigh_mm = new Point2f[4] { new Point2f(0, 0), new Point2f((float)14.1, 0), new Point2f(0, (float)14.1), new Point2f((float)14.1, (float)14.1) };
            m_hmExpLow = Cv2.FindHomography(InputArray.Create(ptExpLow_px), InputArray.Create(ptExpLow_mm), HomographyMethods.Ransac);
            m_hmExpHigh = Cv2.FindHomography(InputArray.Create(ptExpHigh_px), InputArray.Create(ptExpHigh_mm), HomographyMethods.Ransac);

            m_hm = Cv2.FindHomography(InputArray.Create(ptExpLow_px), InputArray.Create(ptExpHigh_px), HomographyMethods.Ransac);
            //
            //// 호모그라피를 이용하여 이미지 변환
            //Mat resultImage = new Mat();
            //Cv2.WarpPerspective(srcImage1, resultImage, homography, srcImage2.Size());
            //
            //// 결과 이미지 저장
            //Cv2.ImWrite("result.jpg", resultImage);
            //
            //// 윈도우에 결과 이미지 표시
            //Cv2.ImShow("Result", resultImage);
            //Cv2.WaitKey(0);
            //Cv2.DestroyAllWindows();
            double[] dHm = new double[9];
            m_hm.GetArray<double>(out dHm);

            for (int i = 0; i < 9; i++)
            {
                WritePrivateProfileString("Homography", $"m_hm{i}", dHm[i].ToString(), FileManage.BinPath + "Homography3.ini");
            }
        }
    }
}
