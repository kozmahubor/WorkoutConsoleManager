using IUE7VU_HFT_2022231.Logic.Classes;
using IUE7VU_HFT_2022231.Models;
using IUE7VU_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUE7VU_HFT_2022231.Logic
{
    public class WorkoutLogic : IWorkoutLogic
    {
        IRepository<Workout> workoutRepo;
        IRepository<Person> personRepo;
        public WorkoutLogic(IRepository<Workout> wRepo, IRepository<Person> pRepo)
        {
            this.workoutRepo = wRepo;
            this.personRepo = pRepo;
        }

        public void Create(Workout item, int personId)
        {
            Person person = this.personRepo.Read(personId);
            if (person == null)
            {
                throw new ArgumentException("Person does not exist!");
            }
            
            bool isValidMuscle = System.Enum.GetValues(typeof(Models.Enum.MuscleTypes)).Cast<int>().Contains((int)item.MuscleTypes);
            if (!isValidMuscle)
            {
                throw new ArgumentException("This muscle does not exist");
            }
            item.PersonId = personId;
            item.WorkoutId = IDGeneratorUtil.GenerateID();
            this.workoutRepo.Create(item);
            
        }
        public void Delete(int id)
        {
            this.workoutRepo.Delete(id);
        }
        public Workout Read(int workoutId)
        {
            var workout = this.workoutRepo.Read(workoutId);
            if (workout == null)
            {
                throw new ArgumentException("Workout was not found!");
            }
            return workout;
        }
        public IQueryable<Workout> ReadAll(int personId)
        {
            return this.workoutRepo.ReadAll().Where(w => w.PersonId == personId);
        }
        public IQueryable<Workout> ReadAll()
        {
            return this.workoutRepo.ReadAll();
        }
        public void Update(Workout item, int workoutId)
        {
            item.WorkoutId = workoutId;
            this.workoutRepo.Update(item);
        }
    }
}
