using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyRegistry.Repository.Abstract
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> Get();

        Task<T> Get(string id);

        Task<T> Update(T obj);

        Task<T> Insert(T obj);

        Task<IEnumerable<T>> Search(string searchTerm);

        void Delete(string id);
    }
}
