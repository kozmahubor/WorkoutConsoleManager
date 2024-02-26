using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IUE7VU_HFT_2022231.Logic.Classes;
using IUE7VU_HFT_2022231.Models;
using IUE7VU_HFT_2022231.Repository;
using static IUE7VU_HFT_2022231.Models.Enum;

namespace IUE7VU_HFT_2022231.Logic
{
    public class PersonLogic : IPersonLogic
    {
        IRepository<Person> personRepo;
        IRepository<Workout> workoutRepo;
        public PersonLogic(IRepository<Person> personRepo, IRepository<Workout> workoutRepo)
        {
            this.personRepo = personRepo;
            this.workoutRepo = workoutRepo;
        }
        public PersonLogic(IRepository<Person> personRepo) 
        { 
            this.personRepo = personRepo;
        }
        public void Create(Person item)
        {
            if (item.PersonName.Length < 3)
            {
                throw new ArgumentException("Name was too short!");
            }
            item.PersonId = IDGeneratorUtil.GenerateID();
            this.personRepo.Create(item);
        }
        public void Delete(int id)
        {
            this.personRepo.Delete(id);
        }
        public Person Read(int id)
        {
            var person = this.personRepo.Read(id);
            if (person == null)
            {
                throw new ArgumentException("Person does not exist!");
            }
            
            return person;
        }
        public IQueryable<Person> ReadAll()
        {
            if (this.personRepo.ReadAll().Any(p => p.Trainer == null))
            {
                throw new ArgumentException("Person does not have a trainer");
            }
            return this.personRepo.ReadAll();
        }
        public void Update(Person item, int personId)
        {
            item.PersonId = personId;
            this.personRepo.Update(item);
        }
        //-------------------------------------------------------------
        public IEnumerable<Person> GetPeopleWithMonthMemberships()
        {
            return this.personRepo.ReadAll()
                .Where(p => p.Memberships.MembershipType == MembershipTypes.Monthly);
        }
        public IEnumerable<Person> GetPersonWithMostWorkouts()
        {
            var persons = this.personRepo.ReadAll();
            int max = persons.Max(p => p.Workouts.Count());
            
            return persons.Where(p => p.Workouts.Count() == max);
        }

        public IEnumerable<Person> GetPeopleWithExtremeWorkouts()
        {
            return this.personRepo.ReadAll()
                .Where(p => p.Workouts.Any(w => w.WorkoutDifficulty == WorkoutDifficulty.Extreme));
        }
    }
}
