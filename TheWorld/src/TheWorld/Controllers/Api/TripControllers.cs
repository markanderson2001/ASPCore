using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Controllers.Api;
using TheWorld.Models;
using TheWorld.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")] //put as base route for entire class
    [Authorize]
    //^^ addidentity to this API
    public class TripControllers : Controller
    {
        
        private ILogger<TripControllers> _logger; //Logger object know what class its being logged from
        private IWorldRepository _repository;

        //add construstor
        public TripControllers(IWorldRepository repository, ILogger<TripControllers> logger)//add generic argument  - tripcontroller as a logger
        {
            //MEMBERS
            _repository = repository;
            _logger = logger;
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
                //var results = _repository.GetAllTrips(); validate user so only get trips by user
                var results = _repository.GetTripsByUsername(this.User.Identity.Name); 
                    //incl name which we use to query trips for that user   
                    //f12 after generation and refator for use



                // return Ok(Mapper.Map<TripViewModel>(results));// Mapper view model to results- but not returning a single trip - it is returing an inumerable trip
                //So lets tell it to reurn a collection
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
                // if (true) return BadRequest("Bad Things Happened");
                //return Ok(new Trip() { Name = "My Trip" });
                //return Ok(_repository.GetAllTrips());
            }
            catch(Exception ex)
            {
                //Logging error
                _logger.LogError($"Failed to get All Trips: {ex}");//choose level and add exception inside of log

                return BadRequest("Error Occured" + ex);
            }
        }

        [HttpPost("")]  //using the attribute based on the verb so its going to match this URi and this verb of POST
                                // to try call this method itself. passing theTripObject (date,Id,Name,Stops)
                                // we have tio tell it where we are getting the data in this post ([FromBody] an attribute we can put directly on the body
                                // . .saying (map) modelBind is correct terms Formbody & Trip to match up names with property of the Json with names of property of the object



        public async Task<IActionResult> Post([FromBody] TripViewModel theTrip) //add TripViewModel in ViewModels // added async
        {
            if (ModelState.IsValid)
            {
                //SAVE to DATABASE (after added Automapper in project.json
                var newTrip = Mapper.Map<Trip>(theTrip); //unsurprisingly static method called Map -we want a Trip Object and Pass in theTrip
                                                         // thus we return newTrip and pass in source  so we send back a result of what we created (assuming valid)
                                                         //this assumes maps have been created between the two types: from Trip to TripViewModel essentially  ..
                                                         //.. to do that we do it in startup ( can do anywhere ..we'll do it in configure

                //add for user ony
                newTrip.Username = User.Identity.Name;

                //Add trip to Database
                _repository.AddTrip(newTrip); //Support for "addtrip" in reposirory 

                if (await _repository.SaveChangesAsync())
                //if succeeded save
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));//return newtrip - set both ways in startup too (use reversemap)
                }
                //if Not saved
                //else
                //{
                //    return BadRequest("Failed to Save Changes to dB");
                //    //return BadRequest(ModelState); //lets return the actual models state in the request as feedback
                //    // Not suggested in public API
                //}

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
            return BadRequest("Failed to Save Changes to dB");
           //return BadRequest("Bad Data");

        }
    }
}
