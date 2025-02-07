using System;

namespace Web.Models
{
    public class Part
    {
        public int Id { get; set; }
        public int CarId { get; set; } // Foreign Key
        public string PartName { get; set; }
        public string PartCondition { get; set; } // e.g., "new", "used"
        public DateTime ObtainedAt { get; set; } = DateTime.UtcNow;
        public decimal Price { get; set; }

        // Navigation Property
        public Car Car { get; set; }
    }
}