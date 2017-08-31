using Service.Interface;
using Service.Interface.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Services.Services
{
    public class StorageService: IService<PortfolioItem>
    {
        private string filePath;
        private PortfolioItemsService portfolioItemsService;
        private UsersService usersService;

        public StorageService(string path)
        {
            filePath = path;
            portfolioItemsService = new PortfolioItemsService();
            usersService = new UsersService();
            var userId = usersService.GetOrCreateUser();
            InitializeFile(userId);
        }

        public void CreateItem(PortfolioItem item)
        {
            string newJson;

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                List<PortfolioItem> items = JsonConvert.DeserializeObject<List<PortfolioItem>>(json);
                items.Add(item);
                newJson = JsonConvert.SerializeObject(items);
            }

            File.WriteAllText(filePath, newJson);

            portfolioItemsService.CreateItem(item);
        }

        public void DeleteItem(int id)
        {
            PortfolioItem item = null;
            string newJson;

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                List<PortfolioItem> items = JsonConvert.DeserializeObject<List<PortfolioItem>>(json);
                foreach (var i in items)
                {
                    if (i.ItemId == id)
                    {
                        item = i;
                        break;
                    }
                }
                if (item != null)
                {
                    items.Remove(item);
                    newJson = JsonConvert.SerializeObject(items);
                }
                else
                {
                    throw new ArgumentException($"There is no such item with id = {id}");
                }
            }

            File.WriteAllText(filePath, newJson);

            portfolioItemsService.DeleteItem(item.ItemId);
        }

        public IList<PortfolioItem> GetItems(int id)
        {
            IList<PortfolioItem> items;

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<PortfolioItem>>(json);
                items = items.Select(i => i).Where(i => i.UserId == id).ToList();
            }

            return items;
        }

        public void UpdateItem(PortfolioItem item)
        {
            string newJson;

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                List<PortfolioItem> items = JsonConvert.DeserializeObject<List<PortfolioItem>>(json);
                var itemToUpdate = items.Select(i => i).Where(i => i.ItemId == item.ItemId).FirstOrDefault();
                items.Remove(itemToUpdate);
                items.Add(item);
                newJson = JsonConvert.SerializeObject(items);
            }

            File.WriteAllText(filePath, newJson);

            portfolioItemsService.UpdateItem(item);
        }

        private void InitializeFile(int userId)
        {
            var items = portfolioItemsService.GetItems(userId);
            string json = JsonConvert.SerializeObject(items);
            File.WriteAllText(filePath, json);
        }
    }
}
