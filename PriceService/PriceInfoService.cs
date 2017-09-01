using Newtonsoft.Json.Linq;
using Service.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PriceService
{
    public class PriceInfoService
    {
        public IList<PortfolioItem> GetPrices(IList<PortfolioItem> items)
        {
            if (items != null)
            {
                StringBuilder tickers = new StringBuilder();
                foreach (var item in items)
                {
                    tickers.Append(item.Symbol + ',');
                }

                string json = GetJsonString(tickers.ToString());

                ParseJson(json, items);

            }
            return items;
        }

        private string GetJsonString(string tickers)
        {
            string json;
            using (var web = new WebClient())
            {
                var url = $"http://finance.google.com/finance/info?client=ig&q=NASDAQ%3A{tickers}";
                json = web.DownloadString(url);
            }

            return json.Replace("//", "");
        }

        private void ParseJson(string json, IList<PortfolioItem> items)
        {
            var v = JArray.Parse(json);

            foreach (var i in v)
            {
                var ticker = i.SelectToken("t").ToString();
                var price = (double)i.SelectToken("l");

                var item = items.Select(l => l).Where(l => l.Symbol.ToUpper() == ticker).FirstOrDefault();
                item.Price = price;
            }
        }
    }
}
