using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Sets;

namespace DataLayer.Repos
{
    public interface IBaseRepo<T> where T : BaseEntity, new()
    {
        T New();
        bool Delete(T record);
        bool Update(T record);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
    }

    public class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity, new()
    {
        public T New()
        {
            return Set<T>.New();
        }

        public bool Delete(T record)
        {
            return Set<T>.Delete(record);
        }

        public bool Update(T record)
        {
            return Set<T>.Update(record.Id, record);
        }

        public T GetById(Guid id)
        {
            return Set<T>.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<T> GetAll() => Set<T>.All();
    }
}
