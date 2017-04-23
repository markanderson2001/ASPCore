using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

//This is the class that represents the different types of data that we are storing in the database

namespace TheWorld.Models
{
    //public class WorldContext : DbContext  //add the package Ms.entityframeworkCore //
        public class WorldContext : IdentityDbContext<WorldUser>  ////added identity,
                                                       // takes generic argument that represents the entity that its going to store our user info;
                                                       //like Username and password and email...

    {
        private IConfigurationRoot _config;

        public WorldContext(IConfigurationRoot config, DbContextOptions options)
            :base(options)//from startup - dependency injection layer will do that for us
        {
            _config = config;//store at class level so we can use it
        }
    //take each type of entities we have and expose them in a property of a specialt type called DbSet
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Stop> Stops { get; set; }

        //Onconfiguring gives you this database otions builder, that allows us to say;
            //use SQL Sever and pass it in a connection string
            
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //because we are using onconfigurin we need to add another base class for DbContect
                                                                                       //because base class for dbcontext, accepts something called dbContext options
                                                                                       // and we need to be able to pass these down to that base class
                                                                                       // to do that we need to inject dbcontecxtOptions and pass them to the base class
                                                                                      

        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnection"]);//added package ms.entityframeworkcore.sqlserver
                                                                       // we want to use the configuration data we already set up at top of startup.cs - builder added as singleton
                                                                       //do not want to use hard coding here
                                                                       //__config - store connection string in config file (Config.json) under data
                                                                       //rememebr ConnectionStrings : because it is a subobject

        }
        //this gives us a class that we can execute LINQ queries against, these properties of dbSet are starting points
        //for iQueryable interfaces, adn we use this when we query db directly
        //now register in startup class

    }
}
