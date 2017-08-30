using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll(int id);
        void Delete(int id);
        void Update(T item);
        void Create(T item);
    }
}
