using IUE7VU_HFT_2022231.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IUE7VU_HFT_2022231.Logic
{
    public interface IPersonLogic
    {
        void Create(Person item);
        void Delete(int id);
        Person Read(int id);
        IQueryable<Person> ReadAll();
        void Update(Person item, int personId);
        IEnumerable<Person> GetPeopleWithMonthMemberships();
        IEnumerable<Person> GetPersonWithMostWorkouts();
        IEnumerable<Person> GetPeopleWithExtremeWorkouts();
    }
}