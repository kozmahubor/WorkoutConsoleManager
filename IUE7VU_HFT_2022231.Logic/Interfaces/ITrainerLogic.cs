using IUE7VU_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;

namespace IUE7VU_HFT_2022231.Logic
{
    public interface ITrainerLogic
    {
        void Create(Trainer item);
        void Delete(int id);
        Trainer Read(int id);
        IQueryable<Trainer> ReadAll();
        void Update(Trainer item, int trainerId);
        IEnumerable<Trainer> GetPopularTrainers();
        IEnumerable<Trainer> GetPersonsTrainerWithRedColourMembership();
    }
}