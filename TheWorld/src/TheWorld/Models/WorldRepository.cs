using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//in order to make this an interface to mock it up later - we use refactoring, by coing to the class name (WorlRepository) and hit ctrl+perios, to extract an interface
//this will pop a dialog for extract interface and you see it named iW rldRepository (copmmon pattern), then select all members that count
// end up with a iWorldRepository.cs file in root - move to models

namespace TheWorld.Models
{
    
    public class WorldRepository
    {
        private WorldContext _context;

        //create constructor to create a world context class
        public WorldRepository(WorldContext context)
        {
            _context = context;
        }
        //to query all the trips -  new method to return collection of all trips (with no parameters)
        public IEnumerable<Trip> GetAllTrips()
        {
            //use context to generate the query
            return _context.Trips.ToList();
        }
        
    }
}
