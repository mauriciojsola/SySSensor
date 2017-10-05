using System;

namespace SySSensor.Web.Models.Logs
{
    public class LogFileViewModel
    {
        public string Filename { get; set; }
        public string FileContent { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? ProcessDate { get; set; }
    }
}