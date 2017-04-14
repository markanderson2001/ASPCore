using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetTripByName(string tripName);//record stops
        //Methods for saving to Database
        void AddTrip(Trip trip);//that takes an actual trip object, this will send Trip to underlying context
                                //..when a number of changes are being made.. be able to save all those in one fell swoop;
        Task<bool> SaveChangesAsync(); //good idea here; make longer lived operations like saving to the dB a synchronous call    
                                             // this will test true or false if saved success
                                             //Add  interface so we get these new methods that we have to implement in WorldRepository
        //void AddStop(string tripName, string username, Stop newStop);

        //Task<bool> SaveChangesAsync();
        //object GetTripsByUsername(string name);

    }
}