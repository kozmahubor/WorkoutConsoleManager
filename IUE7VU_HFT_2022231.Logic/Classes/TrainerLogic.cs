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
    public class TrainerLogic : ITrainerLogic
    {
        IRepository<Trainer> repo;
        IRepository<Person> personRepo;

        public TrainerLogic(IRepository<Trainer> repo, IRepository<Person> personRepo)
        {
            this.repo = repo;
            this.personRepo = personRepo;
        }
        public TrainerLogic(IRepository<Trainer> repo) 
        {
            this.repo = repo;
        }

        public void Create(Trainer item)
        {
            if (item.TrainerName.Length < 3)
            {
                throw new ArgumentException("Name was too short!");
            }
            item.TrainerId = IDGeneratorUtil.GenerateID();
            this.repo.Create(item);
        }
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }
        public Trainer Read(int id)
        {
            var trainer = this.repo.Read(id);
            if (trainer == null)
            {
                throw new ArgumentException("Trainer does not exist!");
            }
            return trainer;
        }
        public IQueryable<Trainer> ReadAll()
        {
            return this.repo.ReadAll();
        }
        public void Update(Trainer item, int trainerId)
        {
            item.TrainerId = trainerId;
            this.repo.Update(item);
        }
        public IEnumerable<Trainer> GetPopularTrainers()
        {
            return this.repo.ReadAll().Where(t => t.Clients.Count > 1);
        }
        public IEnumerable<Trainer> GetPersonsTrainerWithRedColourMembership()
        {
            return this.repo.ReadAll().Where(t => t.Clients
            .Any(p => p.Memberships.MembershipTicketColour == MembershipTicketColours.Red));
        }
    }
}
