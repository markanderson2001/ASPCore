﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//in order to make this an interface to mock it up later - we use refactoring, by coing to the class name (WorlRepository) and hit ctrl+period, to extract an interface
//this will pop a dialog for extract interface and you see it named iW rldRepository (copmmon pattern), then select all members that count
// end up with a iWorldRepository.cs file in root - move to models
////*add to actual repository
//add to startup to allow it to be used


namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, string username, Stop newStop)
        {
            var trip = GetTripByName(tripName, username);

            if (trip != null)
            {
                trip.Stops.Add(newStop);
            }
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All Trips from the Database");

            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName, string username)
        {
            return _context.Trips
              .Include(t => t.Stops)
              .Where(t => t.Name == tripName && t.UserName == username)
              .FirstOrDefault();
        }

        public object GetTripsByUsername(string name)
        {
            return _context.Trips
              .Where(t => t.UserName == name)
              .ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}




//namespace TheWorld.Models
//{

//    public class WorldRepository :IWorldRepository //*add to actual repository

//    {
//        private WorldContext _context;
//        private ILogger<WorldRepository> _logger;

//        //create constructor to create a world context class
//        public WorldRepository(WorldContext context,ILogger<WorldRepository> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        public void AddStop(string tripName, Stop newStop, string username)
//        {
//            var trip = GetUserTripByName(tripName,username);
//            if (trip != null)
//            {
//                trip.Stops.Add(newStop); //FK being set then add as new object;
//                _context.Stops.Add(newStop); //ef ops

//            }
//            //throw new NotImplementedException();
//        }

//        public void AddTrip(Trip trip)
//        {
//            _context.Add(trip); //this pushes it into the context as a new object ...save below
//                                // throw new NotImplementedException();
//        }

//        //to query all the trips -  new method to return collection of all trips (with no parameters)
//        public IEnumerable<Trip> GetAllTrips()

//        {
//            _logger.LogInformation("Getting all trips from the Database");
//            //use context to generate the query (to mock - use refractoring
//            return _context.Trips.ToList();
//        }

//        public Trip GetTripByName(string tripName)
//        {

//            return _context.Trips
//                .Include(t => t.Stops)
//                .Where(t => t.Name == tripName)
//                .FirstOrDefault();//use lamba expression, tofigure which trip to return, add inlcude here in order to get the stops (add ef namespace) to eager load that collection
//                                    // allow to add collection of stops when returned
//            //throw new NotImplementedException();
//        }

//        //essentialy same as getalltrips
//        public IEnumerable<Trip> GetTripsByUsername(string name)
//        {
//            return _context.Trips
//                .Include(t => t.Stops)
//                .Where(t => t.Username == name)
//                .ToList();

//        }

//        public Trip GetUserTripByName(string tripName, string username)
//        {
//            return _context.Trips
//                .Include(t => t.Stops)
//                .Where(t => t.Name == tripName && t.Username == username)
//                .FirstOrDefault();
//        }

//        public async Task<bool> SaveChangesAsync()//put in interface
//        {
//            return (await _context.SaveChangesAsync()) > 0; //return a Boolean  (save changes and savechangesAsync returns an integer with # of rows effected)
//            //throw new NotImplementedException();
//        }
//    }
//}
