using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Activities;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet] //endpoints
        //api controller sends request to mediator and returns list via mediator back to api controller
        public async Task<ActionResult<List<Activity>>> GetActivities() 
        {
            //Mediator becuase it inherites from BaseAPI controller
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        //not returning activity object, Iactionresult returns Okay, bad request etc
        public async Task<IActionResult> CreateActivity(Activity activity) 
        {
            return Ok(await Mediator.Send(new Create.Command {Activity = activity}));
        }
        [HttpPut("{id}")] //endpoint for updating resources

        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Edit.Command {Activity = activity}));
        } 

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command{Id = id}));
        }

    }
}