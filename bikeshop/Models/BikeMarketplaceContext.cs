using Microsoft.EntityFrameworkCore;
using System;

namespace bikeshop.Models
{
    public class BikeMarketplaceContext : DbContext
    {
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BikeMarketplaceContext(DbContextOptions<BikeMarketplaceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>().HasData(
                new Brand[]
                {
                    new Brand{ Id = 1, Name = "Merida", Sale=0.07f},
                    new Brand{ Id = 2, Name = "Giant", Sale=0},
                    new Brand{ Id = 3, Name = "Stinger", Sale=0.1f},
                    new Brand{ Id = 4, Name = "Trek", Sale=0.15f},
                    new Brand{ Id = 5, Name = "Electra", Sale=0},
                    new Brand{ Id = 6, Name = "Atom", Sale=0.2f},
                    new Brand{ Id = 7, Name = "Haro", Sale=0.09f},
                    new Brand{ Id = 8, Name = "Dahon", Sale=0.17f},
                    new Brand{ Id = 9, Name = "Orbea", Sale=0},
                });
            modelBuilder.Entity<Bike>().HasData(
                new Bike[]
                {
                    new Bike{Id = 1, Model = "Crossway 20-MD", Price=31000, BrandId = 1},
                    new Bike{Id = 2, Model = "ATX 2", Price=52000, BrandId = 2},
                    new Bike{Id = 3, Model = "Element D 26", Price=17000, BrandId = 3},
                    new Bike{Id = 4, Model = "Python Pro 29", Price=24000, BrandId = 3},
                    new Bike{Id = 5, Model = "Graphite Pro 27.5", Price=22000, BrandId = 3},
                    new Bike{Id = 6, Model = "Big.Nine 40", Price=7000, BrandId = 1},
                    new Bike{Id = 7, Model = "Roscoe 8", Price=135000, BrandId = 4},
                    new Bike{Id = 8, Model = "Domane SL 6", Price=280000, BrandId = 4},
                    new Bike{Id = 9, Model = "Fuel EX 9.8", Price=400000, BrandId = 4},
                    new Bike{Id = 10, Model = "Straight 8 8i", Price=75000, BrandId = 5},
                    new Bike{Id = 11, Model = "Delivery 3i Step-Over", Price=75000, BrandId = 5},
                    new Bike{Id = 12, Model = "Tandem Super Deluxe 7i", Price=100000, BrandId = 5},
                    new Bike{Id = 13, Model = "Nitro", Price=21000, BrandId = 6},
                    new Bike{Id = 14, Model = "Annex Mini", Price=22550, BrandId = 7},
                    new Bike{Id = 15, Model = "Curve i3 16", Price=41000, BrandId = 8},
                    new Bike{Id = 16, Model = "Hemingway D8", Price=48000, BrandId = 8},
                    new Bike{Id = 17, Model = "CARPE 10", Price=101000, BrandId = 9},
                    new Bike{Id = 18, Model = "CARPE 25", Price=81000, BrandId = 9},
                    new Bike{Id = 19, Model = "CARPE 20", Price=71000, BrandId = 9},
                });
            modelBuilder.Entity<Order>().HasData(
                new Order[]
                {
                    new Order{ Id = 1, Firstname = "Karl", Email = "karl@gmail.com", Price = 31000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-1), BikeId=1},
                    new Order{ Id = 2, Firstname = "Jessie", Email = "jessie@gmail.com", Price = 3100, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-33), BikeId=1},
                    new Order{ Id = 3, Firstname = "Brandon", Email = "brandon@gmail.com", Price = 52000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-1), BikeId=2},
                    new Order{ Id = 4, Firstname = "Austin", Email = "austin@gmail.com", Price = 17000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-10), BikeId=3},
                    new Order{ Id = 5, Firstname = "Gordon", Email = "gordon@gmail.com", Price = 1700, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-40), BikeId=3},
                    new Order{ Id = 6, Firstname = "Edna", Email = "edna@gmail.com", Price = 1700, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-2), BikeId=3},
                    new Order{ Id = 7, Firstname = "Maldwyn", Email = "maldwyn@gmail.com", Price = 24000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-1), BikeId=4},
                    new Order{ Id = 8, Firstname = "Saul", Email = "saul@gmail.com", Price = 2400, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-15), BikeId=4},
                    new Order{ Id = 9, Firstname = "Lewin", Email = "lewin@gmail.com", Price = 22000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-11), BikeId=5},
                    new Order{ Id = 10, Firstname = "Saul", Email = "saul@gmail.com", Price = 2200, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-13), BikeId=5},
                    new Order{ Id = 11, Firstname = "Oran", Email = "oran@gmail.com", Price = 2200, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-2), BikeId=5},
                    new Order{ Id = 12, Firstname = "Parsafal", Email = "parsafal@gmail.com", Price = 135000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-1), BikeId=7},
                    new Order{ Id = 13, Firstname = "Gordon", Email = "gordon@gmail.com", Price = 13500, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-35), BikeId=7},
                    new Order{ Id = 14, Firstname = "Issac", Email = "issac@gmail.com", Price = 28000, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-34), BikeId=8},
                    new Order{ Id = 15, Firstname = "Tyrone", Email = "tyrone@gmail.com", Price = 400000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-67), BikeId=9},
                    new Order{ Id = 16, Firstname = "Oluwatobiloba", Email = "oluwatobiloba@gmail.com", Price = 40000, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-2), BikeId=9},
                    new Order{ Id = 17, Firstname = "Herbie", Email = "herbie@gmail.com", Price = 75000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-122), BikeId=10},
                    new Order{ Id = 18, Firstname = "Saul", Email = "saul@gmail.com", Price = 7500, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-2), BikeId=10},
                    new Order{ Id = 19, Firstname = "Tanisha", Email = "tanisha@gmail.com", Price = 7500, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-32), BikeId=10},
                    new Order{ Id = 20, Firstname = "Tyrone", Email = "tyrone@gmail.com", Price = 75000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-1), BikeId=11},
                    new Order{ Id = 21, Firstname = "William", Email = "william@gmail.com", Price = 7500, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-21), BikeId=11},
                    new Order{ Id = 22, Firstname = "Keisha", Email = "keisha@gmail.com", Price = 100000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-6), BikeId=12},
                    new Order{ Id = 23, Firstname = "Issac", Email = "issac@gmail.com", Price = 10000, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-6), BikeId=12},
                    new Order{ Id = 24, Firstname = "Hywel", Email = "hywel@gmail.com", Price = 10000, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-2), BikeId=12},
                    new Order{ Id = 25, Firstname = "Teresa", Email = "teresa@gmail.com", Price = 2100, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-7), BikeId=13},
                    new Order{ Id = 26, Firstname = "Hywel", Email = "hywel@gmail.com", Price = 22550, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-1), BikeId=14},
                    new Order{ Id = 27, Firstname = "William", Email = "william@gmail.com", Price = 2255, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-5), BikeId=14},
                    new Order{ Id = 28, Firstname = "Jessie", Email = "jessie@gmail.com", Price = 2255, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-2), BikeId=14},
                    new Order{ Id = 29, Firstname = "Oluwatobiloba", Email = "oluwatobiloba@gmail.com", Price = 41000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-1), BikeId=15},
                    new Order{ Id = 30, Firstname = "William", Email = "william@gmail.com", Price = 4100, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-55), BikeId=15},
                    new Order{ Id = 31, Firstname = "Tyrone", Email = "tyrone@gmail.com", Price = 4100, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-2), BikeId=15},
                    new Order{ Id = 32, Firstname = "Karl", Email = "karl@gmail.com", Price = 48000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-1), BikeId=16},
                    new Order{ Id = 33, Firstname = "Joshua", Email = "joshua@gmail.com", Price = 4800, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-2), BikeId=16},
                    new Order{ Id = 34, Firstname = "Joshua", Email = "joshua@gmail.com", Price = 4800, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-2), BikeId=16},
                    new Order{ Id = 35, Firstname = "Gordon", Email = "gordon@gmail.com", Price = 4800, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-220), BikeId=16},
                    new Order{ Id = 36, Firstname = "Orson", Email = "orson@gmail.com", Price = 101000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-12), BikeId=17},
                    new Order{ Id = 37, Firstname = "Tamanna", Email = "tamanna@gmail.com", Price = 10100, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-2), BikeId=17},
                    new Order{ Id = 38, Firstname = "Saul", Email = "saul@gmail.com", Price = 81000, Type = (OrderType)0, Datetime=DateTime.Now.AddDays(-18), BikeId=18},
                    new Order{ Id = 39, Firstname = "Cherie", Email = "cherie@gmail.com", Price = 8100, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-250), BikeId=18},
                    new Order{ Id = 40, Firstname = "William", Email = "william@gmail.com", Price = 7100, Type = (OrderType)1, Datetime=DateTime.Now.AddDays(-17), BikeId=19},
                });
        }
    }
}
