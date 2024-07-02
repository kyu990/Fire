using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobleFinal.DTO
{
    public class Sensor
    {
        public int Id {  get; set; }
        public string ClientSerial { get; set; }
        public bool Fire {  get; set; }
        public double Temp {  get; set; }
        public double Humidity {  get; set; }
        public double Gas {  get; set; }
        public double Cds { get; set; }
        public double Battery { get; set; }
        public Timestamp Time { get; set; }

         
        public Sensor()
        {

        }
        // 환이의 실속있는 강의
        //public Sensor(double fire)
        //{
        //  this.fire = fire;
        //}
        //public Sensor(double fire, double cds) : this(fire)
        //{
        //    this.cds = cds;
        //}

        //public bool Fire { get => fire; set => fire = value; }
        //public double Cds { get => cds; set => cds = value; }
        //public double Temp { get => temp; set => temp = value; }
        //public double Gas { get => gas; set => gas = value; }
    }
}
