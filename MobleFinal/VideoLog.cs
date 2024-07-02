using MobleFinal._Service;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Security.Cryptography;
using MobleFinal.DTO;

namespace MobleFinal
{
    public partial class VideoLog : Form
    {
        private FileManage fileManage;
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private string filePath;
        //private System.Windows.Forms.Timer DelayTimer;   
        //비트맵 클래스 선언    
        private Bitmap bufferBitmap;
        //현재 라벨의 X좌표값으로 씀, 초기화 해둔 상태
        private decimal CirlcleX = 0;
        //선의 길이 만약 패널의 길이가 줄어들 경우를 위해 만들어서 사용
        private decimal LineLength = 1000;
        //암호화 코드
        //private EncryptService encryptService;
        public VideoLog()
        {
            InitializeComponent();
            fileManage = new FileManage();
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            bufferBitmap = new Bitmap(panel1.Width, panel1.Height);
            //encryptService = new EncryptService();
        }
        /// <summary>
        /// 이벤트
        /// </summary>
        /// 
        private void VideoLog_Load(object sender, EventArgs e)
        {
            List<FileInfo> fileList = fileManage.GetVideoList();
            int i = 1;
            foreach (FileInfo file in fileList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = i.ToString();
                item.SubItems.Add(file.Name);
                item.SubItems.Add(file.FullName);
                videoList.Items.Add(item);
                i++;
            }
            timer1.Enabled = false;
            PlayPause.Parent = panel2;
            Minus.Parent = panel2;
            Plus.Parent = panel2;
            PlayPause.BackColor = panel2.BackColor;
            Plus.BackColor = panel2.BackColor;
            Minus.BackColor = panel2.BackColor;
            Plus.FlatAppearance.BorderSize = 0;
            Minus.FlatAppearance.BorderSize = 0;
            PlayPause.FlatAppearance.BorderSize = 0;

        }

        //if (videoList.SelectedItems.Count > 0)
        //{
        //    videoView1.MediaPlayer = _mediaPlayer;
        //    filePath = videoList.SelectedItems[0].SubItems[2].Text;

        //    var media = new Media(_libVLC, filePath);
        //    _mediaPlayer.Media = media;
        //    _mediaPlayer.Play();
        //}
        //timer1.Enabled = true;

        private void videoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (videoList.SelectedItems.Count > 0)
            {
                string filePath = videoList.SelectedItems[0].SubItems[2].Text;
                Console.WriteLine("Encrypted file path: " + filePath); // 파일 경로 출력

                // 파일명에서 영상 파일명 추출
                string filename = videoList.SelectedItems[0].SubItems[1].Text;
                string output = FileManage.VideoPath + @"temp\res.mp4";

                // SQLiteHandler 객체 생성
                SqliteHandler sqliteHandler = new SqliteHandler();

                Video video = sqliteHandler.GetVideo(filename);

                // 암호화된 파일의 경로와 복호화된 파일을 저장할 경로 지정
                AesEncryption.DecryptFile(video.FilePath, output, video.Password);

                // 파일 경로를 사용하여 Uri 객체 생성
                Uri fileUri = new Uri(output);

                videoView1.MediaPlayer = _mediaPlayer;
                var media = new Media(_libVLC, fileUri);
                _mediaPlayer.Media = media;
                _mediaPlayer.Play();
            }
            timer1.Enabled = true;

            videoList.Items.Clear();
            List<FileInfo> fileList = fileManage.GetVideoList();
            int i = 1;
            foreach (FileInfo file in fileList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = i.ToString();
                item.SubItems.Add(file.Name);
                item.SubItems.Add(file.FullName);
                videoList.Items.Add(item);
                i++;
            }
        }



        private void Plus_Click(object sender, EventArgs e)
        {
            decimal videoterm = _mediaPlayer.Media.Duration;
            decimal currentTime = _mediaPlayer.Time;


            // 10초 앞으로 이동
            if (videoterm <= currentTime)
            {
                currentTime = videoterm;
                _mediaPlayer.Time = (long)videoterm;
            }
            else
            {
                _mediaPlayer.Time = (long)currentTime + 10000;
            }
        }

        private void Minus_Click(object sender, EventArgs e)
        {
            //currentTime은 현재 진행중인 _mediaPlayer.Time을 받음
            long currentTime = _mediaPlayer.Time;
            long sec = currentTime / 1000;
            //videoterm은 영상 전체의 시간을 받음
            decimal videoterm = _mediaPlayer.Media.Duration;
            // 10초 앞으로 이동
            if (sec - 10 <= 0)
            {
                _mediaPlayer.Time = 0;
            }
            else
            {
                _mediaPlayer.Time = currentTime - 10000;
                _mediaPlayer.Play();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_mediaPlayer.Media == null || _mediaPlayer.Media.Duration <= 0)
            {
                timer1.Enabled = false;
                return;
            }

            //durationSeconds는 전체 영상길이를 1000으로 나눈 값 영상의 총 재생 시간을 초 단위로 만듬
            decimal durationSeconds = _mediaPlayer.Media.Duration / 1000;
            //현재 진행되는 영상 재생시간을 초 단위로 만듬
            decimal currentTimeSeconds = _mediaPlayer.Time / 1000;
            // 초당 이동 거리를 계산, decimal로 가장 많은 소수점까지 계산하여 영상의 길이가 늘어났을 경우의 오차를 최소화
            decimal MoveLine = LineLength / durationSeconds;
            if (CirlcleX > LineLength)
            {
                CirlcleX = LineLength;
            }
            else
            {
                CirlcleX = MoveLine * currentTimeSeconds;
            }
            if (durationSeconds == currentTimeSeconds)
            {
                CirlcleX = MoveLine * durationSeconds;
            }

            // 시간을 시:분:초 형식으로 표시(decimal로 계산시 소수점 이하의 값이 계산의 오류를 만들어 냄
            // 그래서 계산은 long값으로 함
            // 영상의 재생 시간
            long hours = (long)(currentTimeSeconds / 3600);
            long minutes = (long)((currentTimeSeconds % 3600) / 60);
            long seconds = (long)(currentTimeSeconds % 60);
            label1.Text = $"{hours:00}:{minutes:00}:{seconds:00}";
            //영상 길이 전체의 시간
            long hours2 = (long)(durationSeconds / 3600);
            long minutes2 = (long)((durationSeconds % 3600) / 60);
            long seconds2 = (long)(durationSeconds % 60);
            label2.Text = $"{hours2:00}:{minutes2:00}:{seconds2:00}";
            panel2.Invalidate();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(bufferBitmap))
            {
                //그린 도형들의 이미지를 부드럽게 만들어줌
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Pen pen = new Pen(Color.Black, 2);
                Pen penc = new Pen(Color.Blue, 2);
                //선의 시작 x값 x = 50, y = 10
                System.Drawing.Point start = new System.Drawing.Point(50, 10);
                //선의 끝 기본 x값에 선의 길이 1000, y좌표 10
                System.Drawing.Point endline = new System.Drawing.Point((int)LineLength + 50, 10);
                g.Clear(panel1.BackColor); // 버퍼를 비워서 이전 그림이 남지 않도록 함
                g.DrawLine(pen, start, endline);
                g.FillEllipse(Brushes.Blue, (int)CirlcleX + 50, 5, 10, 10);
                pen.Dispose();
                penc.Dispose();
            }

            // 버퍼에 그려진 내용을 패널에 표시
            e.Graphics.DrawImage(bufferBitmap, 0, 0);
        }

        private void PlayPause_Click(object sender, EventArgs e)
        {
            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Pause();
            }
            else
            {
                _mediaPlayer.Play();
            }
        }
    }
}

