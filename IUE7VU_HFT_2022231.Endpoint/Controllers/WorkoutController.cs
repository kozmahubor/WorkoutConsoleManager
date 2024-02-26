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
    public class WorkoutController : ControllerBase
    {
        IWorkoutLogic logic;
        public WorkoutController(IWorkoutLogic logic)
        {
            this.logic = logic;
        }


        [HttpPost("/workout/{personId}")]
        public void Create([FromBody]Workout item, [FromRoute]int personId)
        {
            this.logic.Create(item, personId);
        }
        [HttpDelete("/workout/{workoutId}")]
        public void Delete([FromRoute] int workoutId)
        {
            this.logic.Delete(workoutId);
        }
        [HttpGet("/workout/{workoutId}")]
        public Workout Read([FromRoute]int workoutId)
        {
            return this.logic.Read(workoutId);
        }
        [HttpGet("/person/{personId}/workout")]
        public IQueryable<Workout>ReadAll([FromRoute]int personId)
        {
            return this.logic.ReadAll(personId);
        }
        [HttpGet("/workout")]
        public IQueryable<Workout>ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpPut("/workout/{workoutId}")]
        public void Update([FromBody]Workout item, [FromRoute]int workoutId)
        {
            this.logic.Update(item, workoutId);
        }
    }
}
