using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{

    [Route("/api/trips/{tripName}/stops")]
        // a way to interact with stops on their own
    public class StopsController: Controller
    {
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        //constructor
        //because of style of controller we need Iworldrep. bring in Models namespace, and logging info and namespace
        public StopsController(IWorldRepository repository, ILogger<StopsController> logger)//because of style of controller we need Iworldrep. bring in Models namespace
        {
            //assign as local vars and use ra=efacoring to add as members
            _repository = repository;
            _logger = logger;
        }
        //create  1st action - return stops for a specific trip - in the context of a trip
        //the other controller is responsible for trip route, thus base would be api trips , the trip name athen the stops
        ///lets do it at the class level - as we will need it for more than one action (get and post)
        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName); //method - implement ..add to repository (iworld interface and wordrep
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
    }
}
