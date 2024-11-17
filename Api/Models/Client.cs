using System;
using Api.Models;

namespace Api.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Car> Cars { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}