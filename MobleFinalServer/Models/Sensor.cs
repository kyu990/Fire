using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using System.Text.Json.Serialization;

namespace MobleFinalServer.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string? ClientSerial { get; set; }

        [JsonPropertyName("Fire")]
        public double Fire { get; set; }

        [JsonPropertyName("Temp")]
        public double Temp { get; set; }

        [JsonPropertyName("Humidity")]
        public double Humidity { get; set; }

        [JsonPropertyName("Gas")]
        public double Gas { get; set; }

        [JsonPropertyName("CDS")]
        public double CDS { get; set; }

        public DateTime Time { get; set; }

        [JsonPropertyName("Battery")]
        [NotMapped]
        public double Battery { get; set; }
    }

    public class SensorView
    {
        public List<Sensor> Sensors { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}
