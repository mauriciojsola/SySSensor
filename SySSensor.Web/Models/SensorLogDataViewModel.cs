using System;

namespace SySSensor.Web.Models
{
    public class SensorLogDataViewModel
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}