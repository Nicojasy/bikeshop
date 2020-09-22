using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace bikeshop.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Sale { get; set; }

        public List<Bike> Bikes { get; set; } = new List<Bike>();
    }
}