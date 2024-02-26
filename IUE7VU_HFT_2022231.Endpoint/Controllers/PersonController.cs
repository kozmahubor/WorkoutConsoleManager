using IUE7VU_HFT_2022231.Logic;
using IUE7VU_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUE7VU_HFT_2022231.Endpoint.Controllers
{
    [ApiController]
    public class PersonController : ControllerBase
    {
        IPersonLogic logic;

        public PersonController(IPersonLogic logic)
        {
            this.logic = logic;
        }
        [HttpPost("/person")]
        public void Create([FromBody] Person item)
        {
            this.logic.Create(item);
        }
        [HttpDelete("/person/{personId}")]
        public void Delete([FromRoute] int personId)
        {
            this.logic.Delete(personId);
        }

        [HttpGet("/person/{personId}")]
        public Person Read([FromRoute] int personId)
        {
            return this.logic.Read(personId);
        }

        [HttpGet("/person")]
        public IQueryable<Person> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpPut("/person/{personId}")]
        public void Update([FromBody] Person item, [FromRoute] int personId)
        {
            this.logic.Update(item, personId);
        }

        [HttpGet("/person/monthmemberships")]
        public IEnumerable<Person> GetPeopleWithMonthMemberships()
        {
            return this.logic.GetPeopleWithMonthMemberships();
        }
        [HttpGet("/person/mostworkouts")]
        public IEnumerable<Person> GetPersonWithMostWorkouts()
        {
            return this.logic.GetPersonWithMostWorkouts();
        }
        [HttpGet("/person/extremeworkouts")]
        public IEnumerable<Person> GetPeopleWithExtremeWorkouts()
        {
            return this.logic.GetPeopleWithExtremeWorkouts();
        }
    }
}
