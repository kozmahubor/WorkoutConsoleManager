using IUE7VU_HFT_2022231.Models;
using System.Linq;

namespace IUE7VU_HFT_2022231.Logic
{
    public interface IMembershipLogic
    {
        void Create(Membership item, int personId);
        void Delete(int id);
        Membership Read(int id);
        IQueryable<Membership> ReadAll();
        void Update(Membership item, int membershipId);
    }
}