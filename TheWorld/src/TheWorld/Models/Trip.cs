using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class Trip
    {
        //id         
        public int Id { get; set; }
        //name of trip
        public string Name { get; set; }
        //date created
        public DateTime DateCreated { get; set; }
        //user of trip
        public string Username { get; set; }

        //create a member that will hold each stop on a trip  -Icollection allow to add and remove (wereas ienumerable is read only
        public ICollection<Stop> Stops { get; set; } //ensure not on root but in models folder if not drag there

    }
}

