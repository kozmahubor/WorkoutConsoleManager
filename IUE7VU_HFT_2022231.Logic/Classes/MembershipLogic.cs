using IUE7VU_HFT_2022231.Logic.Classes;
using IUE7VU_HFT_2022231.Models;
using IUE7VU_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IUE7VU_HFT_2022231.Models.Enum;

namespace IUE7VU_HFT_2022231.Logic
{
    public class MembershipLogic : IMembershipLogic
    {
        IRepository<Membership> membershipRepo;
        IRepository<Person> personRepo;
        public MembershipLogic(IRepository<Membership> repo, IRepository<Person> pRepo)
        {
            this.membershipRepo = repo;
            this.personRepo = pRepo;
        }

        public void Create(Membership item, int personId)
        {
            Person person = this.personRepo.Read(personId);
            if (person == null)
            {
                throw new ArgumentException("Person does not exist!");
            }
            if (person.Memberships != null)
            {
                throw new ArgumentException("This person already has a membership ongoing!");
            }
            bool isValidColour = System.Enum.GetValues(typeof(Models.Enum.MembershipTicketColours)).Cast<int>().Contains((int)item.MembershipTicketColour);
            if (!isValidColour)
            {
                throw new ArgumentException("This muscle does not exist");
            }
            item.MembershipDurationBegin = DateTime.Now;
            switch (item.MembershipType)
            {
                case MembershipTypes.Daily:
                    item.MembershipDurationEnd = DateTime.Now.AddDays(1);
                    break;
                case MembershipTypes.Weekly:
                    item.MembershipDurationEnd = DateTime.Now.AddDays(7);
                    break;
                case MembershipTypes.Monthly:
                    item.MembershipDurationEnd = DateTime.Now.AddDays(30);
                    break;
                default:
                    throw new ArgumentException("Membership type was not found!");
            }
            item.PersonId = personId;
            item.MembershipId = IDGeneratorUtil.GenerateID();
            this.membershipRepo.Create(item);
        }
        public void Delete(int id)
        {
            this.membershipRepo.Delete(id);
        }
        public Membership Read(int id)
        {
            var membership = this.membershipRepo.Read(id);
            if (membership == null)
            {
                throw new ArgumentException("Membership was not found!");
            }
            return membership;
        }
        public IQueryable<Membership> ReadAll()
        {
            return this.membershipRepo.ReadAll();
        }
        public void Update(Membership item, int membershipId)
        {
            item.MembershipDurationBegin = DateTime.Now;
            switch (item.MembershipType)
            {
                case MembershipTypes.Daily:
                    item.MembershipDurationEnd = DateTime.Now.AddDays(1);
                    break;
                case MembershipTypes.Weekly:
                    item.MembershipDurationEnd = DateTime.Now.AddDays(7);
                    break;
                case MembershipTypes.Monthly:
                    item.MembershipDurationEnd = DateTime.Now.AddDays(30);
                    break;
                default:
                    throw new ArgumentException("Membership type was not found!");
            }
            item.MembershipId = membershipId;
            this.membershipRepo.Update(item);
        }
    }
}
