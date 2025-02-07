using System;

namespace Web.Models
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}