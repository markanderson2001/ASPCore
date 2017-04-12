using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")] //put as base route for entire class
    public class TripControllers : Controller
    {
        private IWorldRepository _repository;

        //add construstor
        public TripControllers(IWorldRepository repository)
        {
            _repository = repository;
        }
        //Drive it from Controller (as we did when we built our controllers that are going to return views)
        // this case we are going to return data as an API with MVC6
        //earlier versions of ASP.net had 2 worlds; APi Controllers and View controllers
        //in MV6 everything's a controller,Api and views, change on what you return
        [HttpGet("")]     // .. now they are "" which will be same as ROUTe, if put "/foo" it amends the route not replace
                                    //use postman chrome addon
                                    // when we make a request to the server, that matches this URL, this method will be called
                                    //akin to what we did in startup when we built our default route there
                                        
        public IActionResult Get()    //call Get there and say return Json and crete a new trip object
                                   //and set one of its values so we can get this data returning
                                   //and the name is going to be My Trip
        {
            // if (true) return BadRequest("Bad Things Happened");
            //return Ok(new Trip() { Name = "My Trip" });
            return Ok(_repository.GetAllTrips());
        }

        [HttpPost("")]  //using the attribute based on the verb so its going to match this URi and this verb of POST
                                // to try call this method itself. passing theTripObject (date,Id,Name,Stops)
                                // we have tio tell it where we are getting the data in this post ([FromBody] an attribute we can put directly on the body
                                // . .saying (map) modelBind is correct terms Formbody & Trip to match up names with property of the Json with names of property of the object
        public IActionResult Post([FromBody] Trip theTrip)
        {
            return Ok(true);

        }
    }
}
