using IUE7VU_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE7VU_HFT_2022231.Repository
{
    public class TrainerRepository : Repository<Trainer>, IRepository<Trainer>
    {
        public TrainerRepository(IUE7VUDbContext cntxt) : base(cntxt)
        {
        }
        public override Trainer Read(int id)
        {
            return cntxt.Trainers.FirstOrDefault(t => t.TrainerId == id);
        }
        public override void Update(Trainer item)
        {
            var old = Read(item.TrainerId);
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
