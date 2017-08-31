using System.ComponentModel.DataAnnotations;

namespace PortfolioManagerClient.Models
{
    public class PortfolioItemViewModel
    {
        public int ItemId { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public int SharesNumber { get; set; }
    }
}