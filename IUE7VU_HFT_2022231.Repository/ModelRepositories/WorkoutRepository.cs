using IUE7VU_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE7VU_HFT_2022231.Repository
{
    public class WorkoutRepository : Repository<Workout>, IRepository<Workout>
    {
        public WorkoutRepository(IUE7VUDbContext cntxt) : base(cntxt)
        {
        }
        public override Workout Read(int id)
        {
            return cntxt.Workouts.FirstOrDefault(t => t.WorkoutId == id);
        }

        public override void Update(Workout item)
        {
            var old = Read(item.WorkoutId);
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
