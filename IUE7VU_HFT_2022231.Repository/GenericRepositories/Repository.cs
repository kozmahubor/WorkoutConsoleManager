using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE7VU_HFT_2022231.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected IUE7VUDbContext cntxt;
        public Repository(IUE7VUDbContext cntxt)
        {
            this.cntxt = cntxt;
        }
        public void Create(T item)
        {
            cntxt.Set<T>().Add(item);
            cntxt.SaveChanges();
        }
        public void Delete(int id)
        {
            cntxt.Set<T>().Remove(Read(id));
            cntxt.SaveChanges();
        }
        public IQueryable<T> ReadAll()
        {
            return cntxt.Set<T>();
        }
        public abstract T Read(int id);
        public abstract void Update(T item);
    }
}
