using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace bikeshop.Models
{
    public enum OrderType { 
        Buy,
        Rest
    }

    public class Order
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public double Price { get; set; }
        public OrderType Type { get; set; }
        public DateTime Datetime { get; set; }

        public int BikeId { get; set; }
        public Bike Bike { get; set; }
    }
}