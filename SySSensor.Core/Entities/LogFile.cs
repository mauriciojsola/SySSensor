using System;

namespace SySSensor.Core.Entities
{
    public class LogFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string FileContent { get; set; }
    }
}
