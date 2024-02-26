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
    public class MembershipController : ControllerBase
    {
        IMembershipLogic logic;

        public MembershipController(IMembershipLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost("/membership/{personId}")]
        public void Create([FromBody] Membership item, [FromRoute] int personId)
        {
            this.logic.Create(item, personId);
        }
        [HttpDelete("/membership/{membershipId}")]
        public void Delete([FromRoute] int membershipId)
        {
            this.logic.Delete(membershipId);
        }
        [HttpGet("/membership/{membershipId}")]
        public Membership Read([FromRoute] int membershipId)
        {
            return this.logic.Read(membershipId);
        }
        [HttpGet("/membership")]
        public IQueryable<Membership> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpPut("/membership/{membershipId}")]
        public void Update([FromBody] Membership item, [FromRoute] int membershipId)
        {
            this.logic.Update(item, membershipId);
        }
    }
}
