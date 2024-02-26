using IUE7VU_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE7VU_HFT_2022231.Repository
{
    public class PersonRepository : Repository<Person>, IRepository<Person>
    {
        public PersonRepository(IUE7VUDbContext cntxt) : base(cntxt)
        {
        }

        public override Person Read(int id)
        {
            return cntxt.Persons.FirstOrDefault(t => t.PersonId == id);
        }

        public override void Update(Person item)
        {
            var old = Read(item.PersonId);
            foreach (var prop in old.GetType().GetProperties())
            {
                try
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
                catch (Exception)
                {

                }
            }
            cntxt.SaveChanges();
        }

    }
}
