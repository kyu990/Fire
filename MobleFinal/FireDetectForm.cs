using LiveStreaming;
using OpenCvSharp;
using MobleFinal._Service;
using MobleFinal.DAO;
using MobleFinal.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobleFinal
{
    public partial class FireDetectForm : Form
    {
        private LiveStream Stream;
        public static bool pumpController;
        private static FireDetectForm instance;

        public FireDetectForm()
        {
            InitializeComponent();
            Stream = new LiveStream(VideoPlayer);
            VideoPlayer.MouseClick += VideoPlayer_MouseClick;
            pumpController = false;
            instance = this;
        }

        public static void SetSensorData(Sensor sensorData)
        {
            if (instance != null)
            {
                instance.UpdateSensorData(sensorData);
            }
        }

        private void UpdateSensorData(Sensor sensorData)
        {
            if (InvokeRequired) // 메인 스레드 사용을 위한 예외
            {
                Invoke(new Action(() => UpdateSensorData(sensorData)));
            }
            else
            {
                if(sensorData.Fire == true)
                {
                    FireData.Text = "False";
                }
                else
                {
                    FireData.Text = "True";
                }
                // FireData.Text = sensorData.Fire.ToString();
                GasData.Text = sensorData.Gas.ToString();
                CdsData.Text = sensorData.Cds.ToString();
                TempData.Text = sensorData.Temp.ToString();
                HumidityData.Text = sensorData.Humidity.ToString();
                BatteryData.Text = sensorData.Battery.ToString();
            }

        }

        private void bt_stop_Click(object sender, EventArgs e)
        {
            if (Stream.bCameraActive)
            {
                Stream.CameraOff();

            }
        }

        private void bt_start_Click(object sender, EventArgs e)
        {
            if (Stream.bCameraActive)
            {
                Stream.CameraOff();

            }
            else
            {

                Stream.CameraOn();

            }
        }

        private void VideoPlayer_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            Stream.Grab();
        }

        private void btnCalibration_Click(object sender, EventArgs e)
        {
            Stream.Calibration();
        }

        // sendDataToArduino 메서드에서, 전송이 성공 (true 반환)하면 pumpController를 false로 설정하여 while 루프를 종료.
        // sendDataToArduino 메서드에서, 전송이 실패(false 반환)하면 await Task.Delay(500);을 사용
        private async void PumpOnButton_Click(object sender, EventArgs e)
        {
            //string dataToSend = "1"; // 아두이노로 보낼 메세지 (1이 펌프 on)
            //byte[] responseData = Encoding.UTF8.GetBytes(dataToSend + "\n");
            if (pumpController == true)
            {
                pumpController = false;
                PumpLabel.Text = "Off";
            }
            else
            {
                pumpController = true;
                PumpLabel.Text = "On";
            }
        }
    }
}
