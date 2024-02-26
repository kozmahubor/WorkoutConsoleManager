using IUE7VU_HFT_2022231.Models;
using System.Linq;

namespace IUE7VU_HFT_2022231.Logic
{
    public interface IWorkoutLogic
    {
        void Create(Workout item, int personId);
        void Delete(int id);
        Workout Read(int workoutId);
        IQueryable<Workout> ReadAll(int personId);
        void Update(Workout item, int workoutId);
        IQueryable<Workout> ReadAll();
    }
}