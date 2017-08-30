using PortfolioManagerClient.Models;
using Service.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioManagerClient.Infrastructure
{
    public static class Mapper
    {
        public static PortfolioItem ToPortfolioItem (this PortfolioItemViewModel item)
        {
            return new PortfolioItem()
            {
                ItemId = item.ItemId,
                SharesNumber = item.SharesNumber,
                Symbol = item.Symbol,
                UserId = item.UserId
            };
        }
        public static IList<PortfolioItem> ToPortfolioItemList(this IList<PortfolioItemViewModel> list)
        {
            var newList = new List<PortfolioItem>();
            foreach (var item in list)
                newList.Add(item.ToPortfolioItem());
            return newList;
        }

        public static PortfolioItemViewModel ToPortfolioItemViewModel(this PortfolioItem item)
        {
            return new PortfolioItemViewModel()
            {
                ItemId = item.ItemId,
                SharesNumber = item.SharesNumber,
                Symbol = item.Symbol,
                UserId = item.UserId
            };
        }
        public static IList<PortfolioItemViewModel> ToPortfolioItemViewModelList(this IList<PortfolioItem> list)
        {
            var newList = new List<PortfolioItemViewModel>();
            foreach (var item in list)
                newList.Add(item.ToPortfolioItemViewModel());
            return newList;
        }
    }
}