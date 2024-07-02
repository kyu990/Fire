using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolSms;

namespace MobleFinal._Service
{
    internal class SMSService
    {
        SmsApi api = new SmsApi(new SmsApiOptions
        {
            ApiKey = "NCSF2OAUFMT2VKUH",
            ApiSecret = "WFWPQ3WOS0M57CB5W6ALJSPJWZAXZXQ1",
            DefaultSenderId = "01022541485" // 문자 보내는 사람 번호, coolsms 홈페이지에서 발신자 등록한 번호 필수
        });


        public async Task SendSMS()
        {
            // 현재 날짜와 시간 가져오기
            DateTime now = DateTime.Now;

            // 원하는 형식으로 포맷팅
            string formattedTime = now.ToString("yyyy-MM-dd HH:mm:ss");

            // 콘솔에 출력
            //Console.WriteLine(formattedTime);
            try
            {
                var result = await api.SendMessageAsync("01022541485", $"{formattedTime}\n화재가 감지되었습니다.\n<a href=http://106.240.9.234:11000/>여기로 접속해서 확인하세요!!!</a>"); // 받을 사람 번호, 텍스트
                // 성공 처리, 예를 들면:
                MessageBox.Show("문자가 성공적으로 전송되었습니다.");
            }
            catch (Exception ex)
            {
                // 실패 처리, 예를 들면:
                MessageBox.Show("문자 전송 실패: " + ex.Message);
            }
        }


        // await SendSMS();
        // 5/23 15:26 실행은 이렇게. 문자 전송 기회는 이제 14번 남았습니다 ~.. (그다음부턴 유료)
        // 6/9 17:36 12번 가능
    }
}