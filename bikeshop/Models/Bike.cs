using System.Collections.Generic;

namespace bikeshop.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public float Price { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}