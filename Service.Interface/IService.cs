using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IService<T>
    {
        IList<T> GetItems(int id);
        void DeleteItem(int id);
        void UpdateItem(T item);
        void CreateItem(T item);
    }
}
