using System;

namespace SySSensor.Core.Entities
{
    public class SensorLog
    {
        public int Id { get; set; }
        public string SensorId { get; set; }
        public DateTime ReadDate { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
