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
    
    public class WorldRepository :IWorldRepository //*add to actual repository

    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        //create constructor to create a world context class
        public WorldRepository(WorldContext context,ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        //to query all the trips -  new method to return collection of all trips (with no parameters)
        public IEnumerable<Trip> GetAllTrips()

        {
            _logger.LogInformation("Getting all trips from the Database");
            //use context to generate the query (to mock - use refractoring
            return _context.Trips.ToList();
        }
        
    }
}
