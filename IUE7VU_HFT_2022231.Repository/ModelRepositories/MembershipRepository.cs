using IUE7VU_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE7VU_HFT_2022231.Repository
{
    public class MembershipRepository : Repository<Membership>, IRepository<Membership>
    {
        public MembershipRepository(IUE7VUDbContext cntxt) : base(cntxt)
        {
        }
        public override Membership Read(int id)
        {
            return cntxt.Memberships.FirstOrDefault(t => t.MembershipId == id);
        }

        public override void Update(Membership item)
        {
            var old = Read(item.MembershipId);
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

        //public override void PeopleWithMonthMembership(Membership item)
        //{
        //    var temp = Read(item.MembershipId);
        //    if (temp.MembershipType == Models.Enum.MembershipTypes.Monthly)
        //    {
        //        Person person = new Person()
        //        {
        //            PersonId = temp.PersonId,
        //            PersonName = temp.Person.PersonName,
        //            PersonGender = temp.Person.PersonGender
        //        };
        //    }
        //    cntxt.SaveChanges();
        //}

    }
}
