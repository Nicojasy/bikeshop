using bikeshop.EmailServices.EmailService;
using bikeshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikeshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BikeMarketplaceController : ControllerBase
    {
        private readonly BikeMarketplaceContext db;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<BikeMarketplaceController> _logger;

        public BikeMarketplaceController(ILogger<BikeMarketplaceController> logger, BikeMarketplaceContext context, IEmailSender emailSender)
        {
            db = context;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        /// All bikes
        /// </summary>
        /// <returns>Returns all bikes</returns>
        [HttpGet]
        [Route(""), Route("bikes")]
        public ActionResult<IEnumerable<dtoBike>> GetBikes()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //sql query with all bikes
            var result = db.Bikes.Join(db.Brands, bike => bike.BrandId, brand => brand.Id,
                (bike, brand) => new
                dtoBike
                {
                    Model = bike.Model,
                    Price = bike.Price,
                    Sale = brand.Sale,
                    Brand = brand.Name
                });
            stopwatch.Stop();
            _logger.LogInformation($"GetBikes is completed in {stopwatch.ElapsedMilliseconds} milliseconds."); 
            return Ok(result?.AsEnumerable());
        }

        /// <summary>
        /// All sale bikes
        /// </summary>
        /// <returns>Returns all sale bikes</returns>
        [HttpGet]
        [Route("sale_bikes")]
        public ActionResult<IEnumerable<dtoBike>> GetSaleBikes()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //sql query with all discounted bikes
            var result = db.Bikes.Join(db.Brands, bike => bike.BrandId, brand => brand.Id,
                (bike, brand) => new
                dtoBike
                {
                    Model = bike.Model,
                    Price = bike.Price,
                    Sale = brand.Sale,
                    Brand = brand.Name
                })
                .Where(x => x.Sale != 0);

            stopwatch.Stop();
            _logger.LogInformation($"GetSaleBikes is completed in {stopwatch.ElapsedMilliseconds} milliseconds.");
            return Ok(result);
        }

        /// <summary>
        /// The best bike of the month
        /// </summary>
        /// <returns>Returns best bike of month</returns>
        [HttpGet]
        [Route("best_bike_of_month")]
        public ActionResult<dtoBike> GetBestBikeOfMonth()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            //sql query with best selling brand as name and grand total
            var result_BestBrand = db.Orders.Where(x => x.Datetime > DateTime.Now.AddMonths(-1))
                    .GroupBy(x => x.BikeId, x => x.Price, (BikeId, price) => new
                    {
                        Id = BikeId,
                        sumPrice = price.Sum()
                    })
                    .Join(db.Bikes, order => order.Id, bike => bike.Id,
                    (order, bike) => new
                    {
                        bike.Id,
                        bike.BrandId,
                        order.sumPrice,
                        bike.Model,
                        bike.Price
                    })
                    .GroupBy(x => x.BrandId, x => x.sumPrice, (BrandId, sumPrice) => new
                    {
                        Id = BrandId,
                        sumPrice = sumPrice.Sum()
                    })
                    .Join(db.Brands, brand => brand.Id, sumPricesBrand => sumPricesBrand.Id,
                    (sumPricesBrands, brand) => new
                    {
                        brand.Name,
                        sumPricesBrands.sumPrice
                    })
                    .OrderByDescending(x => x.sumPrice)
                    .FirstOrDefault();

            stopwatch.Stop();
            _logger.LogInformation($"GetBestBikeOfMonth is completed in {stopwatch.ElapsedMilliseconds} milliseconds:\n"
                 + $"key: {result_BestBrand.Name}\nsumPrice: {result_BestBrand.sumPrice}");
            return Ok(result_BestBrand);
        }

        /// <summary>
        /// The best brand of the year
        /// </summary>
        /// <returns>Returns best brand of year</returns>
        [HttpGet]
        [Route("best_brand_of_year")]
        public ActionResult<Brand> GetBestBikeOfWeek()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //sql query with top rented brands as name and total profit
            var result_BestBrand = db.Orders.Where(x => x.Datetime > DateTime.Now.AddYears(-1))
                    .GroupBy(x => x.BikeId, x => x.Price, (BikeId, price) => new
                    {
                        Id = BikeId,
                        sumPrice = price.Sum()
                    })
                    .Join(db.Bikes, order => order.Id, bike => bike.Id,
                    (order, bike) => new
                    {
                        bike.Id,
                        bike.BrandId,
                        order.sumPrice,
                        bike.Model,
                        bike.Price
                    })
                    .GroupBy(x => x.BrandId, x => x.sumPrice, (BrandId, sumPrice) => new
                    {
                        Id = BrandId,
                        sumPrice = sumPrice.Sum()
                    })
                    .Join(db.Brands, brand => brand.Id, sumPricesBrand => sumPricesBrand.Id,
                    (sumPricesBrands, brand) => new
                    {
                        brand.Name,
                        sumPricesBrands.sumPrice
                    })
                    .OrderByDescending(x => x.sumPrice)
                    .FirstOrDefault();

            stopwatch.Stop();
            _logger.LogInformation($"GetBestBikeOfMonth is completed in {stopwatch.ElapsedMilliseconds} milliseconds:\n"
                 + $"key: {result_BestBrand.Name}\nsumPrice: {result_BestBrand.sumPrice}");
            return Ok(result_BestBrand);
        }

        /// <summary>
        /// Sending emails to 10 most rented clients
        /// </summary>
        /// <returns>Returns of clients who received emails</returns>
        [HttpGet]
        [Route("email_sender")]
        public async Task<ActionResult<IEnumerable<string>>> GetTopRentalBikes()
        {
            int top_size = 10;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //sql query with op rented clients as emails and number of orders
            var result_bestClients = db.Orders.Where(x => x.Type == (OrderType)1)
                    .GroupBy(x => x.Email, x => new { x.Id, x.Price}, (Email, order) => new
                    {
                        Email,
                        Score = order.Count(),
                    })
                    .OrderByDescending(x=>x.Score)
                    .Take(top_size);

            //sending emails
            StringBuilder log_emails = new StringBuilder();
            List<string> sentEmails = new List<string>(top_size); 
            foreach (var client in result_bestClients.AsEnumerable())
            {
                string nameOfLastRentedBike = db.Orders.Where(x => x.Email == client.Email)
                                                            .OrderByDescending(x => x.Datetime)
                                                            .Take(1)
                                                            .Join(db.Bikes, brand => brand.BikeId, bike => bike.Id,
                                                                (order, bike) => new
                                                                {
                                                                    bike.Model
                                                                })
                                                            .FirstOrDefault()
                                                            .Model;

                await _emailSender.SendEmailAsync(client.Email,
                                                  "Personal discount at \"bikeshop\"",
                                                  $"Hi fat sausage, buy \"{nameOfLastRentedBike}\" as soon as possible with a personal discount!");
                sentEmails.Add(client.Email);
            }

            stopwatch.Stop();
            _logger.LogInformation($"GetTopRentalBikes is completed in {stopwatch.ElapsedMilliseconds} milliseconds."
                + $"Email send to:\n{log_emails}");
            return Ok(sentEmails.AsEnumerable());
        }
    }
}