using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace MobleFinal
{
    public partial class VideoPlayerForm : Form
    {
        //LibVLC라이브러리를 클래스로 선언
        private LibVLC _libVLC;
        //LibVLC라이브러리를 클래스로 선언
        private MediaPlayer _mediaPlayer;
        //비트맵 클래스 선언
        private Bitmap bufferBitmap;
        //현재 라벨의 X좌표값으로 씀, 초기화 해둔 상태
        private decimal circlex = 0;
        //선의 길이 만약 패널의 길이가 줄어들 경우를 위해 만들어서 사용
        private decimal k = 1000;
        public VideoPlayerForm()
        {
            InitializeComponent();
            Core.Initialize();
            //각 클래스 선언
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            bufferBitmap = new Bitmap(panel1.Width, panel1.Height);
        }
        private void _mp_EndReached(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void VideoPlayerForm_Load(object sender, EventArgs e)
        {
            videoView1.MediaPlayer = _mediaPlayer;

            // 비디오 파일 로드
            _mediaPlayer.Play(new Media(_libVLC, "C:\\Project_Fire\\Play\\Y2meta.app-그라가스야 우니_-(1080p60).mp4"));

            timer1.Enabled = true;
            Play.Parent = panel1;
            Pause.Parent = panel1;
            minus.Parent = panel1;
            plus.Parent = panel1;
            Play.BackColor = panel1.BackColor;
            Pause.BackColor = panel1.BackColor;
            plus.BackColor = panel1.BackColor;
            minus.BackColor = panel1.BackColor;
            plus.FlatAppearance.BorderSize = 0;
            minus.FlatAppearance.BorderSize = 0;
            Play.FlatAppearance.BorderSize = 0;
            Pause.FlatAppearance.BorderSize = 0;
        }

        private void Play_Click(object sender, EventArgs e)
        {
            _mediaPlayer.Play();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            _mediaPlayer.Pause();
        }

        private void plus_Click(object sender, EventArgs e)
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

        private void minus_Click(object sender, EventArgs e)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(bufferBitmap))
            {
                //그린 도형들의 이미지를 부드럽게 만들어줌
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Pen pen = new Pen(Color.Black, 2);
                Pen penc = new Pen(Color.Blue, 2);
                //선의 시작 x값 x = 50, y = 10
                Point start = new Point(130, 10);
                //선의 끝 기본 x값에 선의 길이 1000, y좌표 10
                Point endline = new Point((int)k + 130, 10);
                g.Clear(panel1.BackColor); // 버퍼를 비워서 이전 그림이 남지 않도록 함
                g.DrawLine(pen, start, endline);
                g.FillEllipse(Brushes.Blue, (int)circlex + 130, 5, 10, 10);
                pen.Dispose();
                penc.Dispose();
            }

            // 버퍼에 그려진 내용을 패널에 표시
            e.Graphics.DrawImage(bufferBitmap, 0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //durationSeconds는 전체 영상길이를 1000으로 나눈 값 영상의 총 재생 시간을 초 단위로 만듬
            decimal durationSeconds = _mediaPlayer.Media.Duration / 1000;
            //현재 진행되는 영상 재생시간을 초 단위로 만듬
            decimal currentTimeSeconds = _mediaPlayer.Time / 1000;
            // 초당 이동 거리를 계산, decimal로 가장 많은 소수점까지 계산하여 영상의 길이가 늘어났을 경우의 오차를 최소화
            decimal MoveLine = k / durationSeconds;
            if (circlex > k)
            {
                circlex = k;
            }
            else
            {
                circlex = MoveLine * currentTimeSeconds;
            }

            // 시간을 시:분:초 형식으로 표시(decimal로 계산시 소수점 이하으
            //decimal hours = Math.Floor(currentTimeSeconds / 3600);
            //decimal minutes = Math.Floor((currentTimeSeconds % 3600) / 60);
            //decimal seconds = Math.Floor(currentTimeSeconds % 60);
            //label1.Text = $"{hours:00}:{minutes:00}:{seconds:00}";

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
            panel1.Invalidate();
        }
    }
}
