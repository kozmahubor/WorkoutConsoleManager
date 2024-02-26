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
    public class TrainerController : ControllerBase
    {
        ITrainerLogic logic;

        public TrainerController(ITrainerLogic logic, IPersonLogic pLogic)
        {
            this.logic = logic;
        }

        [HttpPost("/trainer")]
        public void Create(Trainer item)
        {
            this.logic.Create(item);
        }
        [HttpDelete("/trainer/{trainerId}")]
        public void Delete([FromRoute] int trainerId)
        {
            this.logic.Delete(trainerId);
        }
        [HttpGet("/trainer/{trainerId}")]
        public Trainer Read([FromRoute] int trainerId)
        {
            return this.logic.Read(trainerId);
        }
        [HttpGet("/trainer")]
        public IQueryable<Trainer> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpPut("/trainer/{trainerId}")]
        public void Update([FromBody] Trainer item, [FromRoute] int trainerId)
        {
            this.logic.Update(item, trainerId);
        }
        [HttpGet("/trainer/popular")]
        public IEnumerable<Trainer> GetPopularTrainers()
        {
            return this.logic.GetPopularTrainers();
        }
        [HttpGet("/trainer/clientswithredmembership")]
        public IEnumerable<Trainer> GetPersonsTrainerWithRedColourMembership()
        {
            return this.logic.GetPersonsTrainerWithRedColourMembership();
        }
    }
}
