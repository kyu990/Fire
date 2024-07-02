using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobleFinal.DTO;
using OpenCvSharp;

namespace MobleFinal._Service
{
    internal class Info
    {
        public readonly static string ClientSerial = "24y5m29d";
        public readonly static int HlsPort = 11002;
        public readonly static int TcpPort = 11001;
        public readonly static int ArduPort = 65432;
        public static BlockingCollection<Mat> frameBuffer;

        static User user;
        public static User User
        {
            get
            {
                if (user == null)
                {
                    user = new User();
                }
                return user;
            }
            set {
                user = value;
            }
        }
    }
}
