using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobleFinal.DAO
{
    internal class EncryptDao
    {
        // 파일 이름을 가져오고 설정합니다.
        public string Filename { get; set; }

        // 파일 경로를 가져오고 설정합니다.
        public string Filepath { get; set; }

        // 키를 가져오고 설정합니다.
        public byte[] Key { get; set; }

        // 초기화 벡터를 가져오고 설정합니다.
        public byte[] IV { get; set; }

        // 기본 생성자
        public EncryptDao()
        {
            // 생성자 코드 추가 가능
        }
    }
}
