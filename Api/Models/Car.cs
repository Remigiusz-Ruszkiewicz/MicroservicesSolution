using System;
using System.Collections.Generic;

namespace Api.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string OwnerName { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateTime ScrapDate { get; set; }
        public string Status { get; set; } // e.g., "scrapped", "parts collected"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public List<Part> Parts { get; set; }
    }
}