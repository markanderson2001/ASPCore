using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

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
            try
            {    //Getting results by calling the repository
                var results = _repository.GetAllTrips();

                // return Ok(Mapper.Map<TripViewModel>(results));// Mapper view model to results- but not returning a single trip - it is returing an inumerable trip
                //So lets tell it to reurn a collection
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
                // if (true) return BadRequest("Bad Things Happened");
                //return Ok(new Trip() { Name = "My Trip" });
                //return Ok(_repository.GetAllTrips());
            }
            catch(Exception ex)
            {
                //TODO Logging error
                return BadRequest("Error Occured");
            }
        }

        [HttpPost("")]  //using the attribute based on the verb so its going to match this URi and this verb of POST
                                // to try call this method itself. passing theTripObject (date,Id,Name,Stops)
                                // we have tio tell it where we are getting the data in this post ([FromBody] an attribute we can put directly on the body
                                // . .saying (map) modelBind is correct terms Formbody & Trip to match up names with property of the Json with names of property of the object
        public IActionResult Post([FromBody] TripViewModel theTrip) //add TripViewModel in ViewModels
        {
            if (ModelState.IsValid)
            {
                //SAVE to DATABASE (after added Automapper in project.json
                var newTrip = Mapper.Map<Trip>(theTrip); //unsurprisingly static method called Map -we want a Trip Object and Pass in theTrip
                                                         // thus we return newTrip and pass in source  so we send back a result of what we created (assuming valid)
                                                         //this assumes maps have been created between the two types: from Trip to TripViewModel essentially  ..
                                                         //.. to do that we do it in startup ( can do anywhere ..we'll do it in configure
                return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));//return newtrip - set both ways in startup too (use reversemap)
                //return Created($"api/trips/{theTrip.Name}",newTrip);
                //////conventionally this method hard as we have to copy code over and over again - we'll use AUtoMapper
                ////var newTrip = new Trip()
                ////{
                ////    Name = theTrip.Name,
                ////    DateCreated = theTrip.Created
                ////};

                //return Ok(true);
                //return Created($"api/trips/{theTrip.Name}", theTrip);
            }
            //if invalid we return badrequest;
           //return BadRequest("Bad Data");
            return BadRequest(ModelState); //lets return the actual models state in the request as feedback
                                            // Not suggested in public API
        }
    }
}
