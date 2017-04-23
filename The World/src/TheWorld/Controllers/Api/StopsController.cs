using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    //set troute attruibute
    [Route("/api/trips/{tripName}/stops")]
        // a way to interact with stops on their own
    public class StopsController: Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        //constructor
        //because of style of controller we need Iworldrep. bring in Models namespace, and logging info and namespace
        public StopsController(IWorldRepository repository,
            ILogger<StopsController> logger,
            GeoCoordsService coordsService)
        {
            //assign as local vars and use ra=efacoring to add as members
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }
        //create  1st action - return stops for a specific trip - in the context of a trip
        //the other controller is responsible for trip route, thus base would be api trips , the trip name athen the stops
        ///lets do it at the class level - as we will need it for more than one action (get and post)
        ///

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                //   var trip = _repository.GetserTripByName(tripName, User.Identity.Name); //method - implement ..add to repository (iworld interface and wordrep
                var trip = _repository.GetTripByName(tripName, User.Identity.Name);
                //** code to ensure we dont have duplicate names in future
                //var trip = _repository.GetTripByName(tripName); //method - implement ..add to repository (iworld interface and wordrep
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList())); //add mapper
                //return Ok(trip.Stops.OrderBy(s => s.Order).ToList());// Get in order they were created .
                                                //note we want to return the stop class, but we want to return the viewmodel (like with trips)
            }
            catch (Exception ex)
            //log err
            {
                _logger.LogError($"Failed to get stop: {0}", ex);
            }
            //return never reached;
            return BadRequest("Failed to Get Stops");
        }

        [HttpPost("")]//empty string for route because we inherit it from Controllers attribute
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                //check if VM is valid
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);
                    //Look up the Geo CodesLook up the Longitude and Latitude of new stop
                    var result = await _coordsService.GetCoordsAsync(newStop.Name);
                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }
                    else  
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;

                        //Save to dB
                        //_repository.AddStop(tripName, newStop,User.Identity.Name); //after auth/identity
                        _repository.AddStop(tripName, User.Identity.Name, newStop);
                        //_repository.AddStop(tripName, newStop);//ad addstop to repository  -sowe use refactoring to generate the new mehod (iworldrepos)
                        //added with refaccotring in iworldrep..void AddStop(string tripName, Stop newStop);  // implement in actual repository
                        //worldRepository -add method ef
                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"/api/trips/{tripName}/stops/{newStop.Name}",
                                Mapper.Map<StopViewModel>(newStop));//use mapper to convert back to a stopviewmodel
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new Stop: {0}",ex);
            }
            return BadRequest("Failed to Post Stops");
        }

    }
}
