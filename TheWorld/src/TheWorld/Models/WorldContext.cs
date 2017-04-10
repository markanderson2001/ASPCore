using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TheWorld.Models
{
    public class WorldContext : DbContext  //add the package Ms.entityframeworkCore

    {
        public WorldContext()
        {

        }
    //take each type of entities we have and expose them in a property of a specialt type called DbSet
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Stop> Stops { get; set; }
    //this gives us a class that we can execute LINQ queries against, these properties of dbSet are starting points
    //for iQueryable interfaces, adn we use this when we query db directly
    //now reghister in startup class
    }
}
