using System;

namespace PortfolioManagerClient.Models
{
    public class PortfolioItemViewModel
    {
        public int ItemId { get; set; }

        public int UserId { get; set; }

        public string Symbol { get; set; }

        public int SharesNumber { get; set; }

        public double Price { get; set; }

        public double Total { get { return Math.Round(Price * SharesNumber, 2); } }
            }
}