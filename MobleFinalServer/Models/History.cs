namespace MobleFinalServer.Models
{
    public class History
    {
        public int Id { get; set; }
        public string ClientSerial { get; set; }
        public string VideoName { get; set; }
        public string VideoPath { get; set; }
        public DateTime Time { get; set; }
        public bool IsFire { get; set; }
        public IEnumerable<Sensor> SensorData { get; set; }
    }
    public class HistoryView
    {
        public IEnumerable<History> Histories { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}
