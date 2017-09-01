using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.Entities
{
    public class PortfolioItem
    {
        public int ItemId { get; set; }

        public int UserId { get; set; }

        public string Symbol { get; set; }

        public int SharesNumber { get; set; }

        public double Price { get; set; }
    }
}
