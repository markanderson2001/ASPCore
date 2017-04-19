using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.services;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld

{
    public class Startup

    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

      //  public IServiceCollection AddEntityFrameworkStores { get; private set; }

        //Insead of #If Debug we can use the hosted environment
        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)//contentroot is root of project  -to tell where config file is
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();//add environment variables
                                           //in properties were going toadd envorinment with MailSetting __ (two underscores to representthe structure - to replace the colon, cause colons of config file are not allowed in non-windows environments
            _config = builder.Build();//will return IConfigurationRoot - add it to constructor on appcontroller

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            //#if Debug
            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing"))
                services.AddScoped<IMailService, DebugMailService>();
            //#else      
            else
            {
                //!!IMpliment a real mail Service - crete a real mail service class that can actually send mail - for staging servers or smokebuilds
            }

            services.AddDbContext<WorldContext>();

            //add id
            services.AddIdentity<WorldUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;//set password requirements
                config.Cookies.ApplicationCookie.LoginPath = "/auth/login";//look at cookeis object
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents();

            })

            .AddEntityFrameworkStores<WorldContext>();

            services.AddScoped<IWorldRepository, WorldRepository>();

            //add as transient object as it does not have any of its own state
            services.AddTransient<GeoCoordsService>();


            //add to read in sample data
            services.AddTransient<WorldContextSeedData>(); //also add to configure the last theng is to call it (bottom of paghe
            ;


            //add exception and Logging - will add interfaces and servies required for logging
            services.AddLogging();


            services.AddMvc()// we are required to use deppendency injection - here we register all the MVC services
               .AddJsonOptions(config =>
               {
                   config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
               });



        }

        //from contact page - 
        //We can make decision about registering our own service
        //AddTransient MEANS WE ARE GOING TO SUPPLY AN INTERFACE, fulfilled by a cretain class - in our case debugMailServices
        //transient means its going to create an instance of debugmailservices as sson as it needs it
        //services.AddTransient<IMailService, DebugMailService>(); //gets it everytime or getting it from cache

        // or change to Addscoped which will an instance of DebugServices for each set of request
        // we will use this as we want this debug services to be reused, but only n the scope of a single request
        //this is fine for debugging, but for productionisnt what we want, may use #if Debug

        //#endif
        // or services.AddSingleton - create one instance when we need it and pass that instance in over and over again

        //add register; dbcontect - EF  -registerss not only EF but also our specific context. ouContext now injectible in different parts of the project


        //            services.AddScoped<IWorldRepository,MockWorldRepository>();





        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //middleware im going to hand you some code to do something as a request comes in (smallest piece of middleware here
        // previous version of asp.net with global asax - event handlers in different points)

        //We are defining who is handling what and in what order

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            WorldContextSeedData seeder,
            ILoggerFactory factory)// to set up what to do when requests come in
        {


           
            //call a method on Mapper (conver model to save data)
            //takes a lambda - we'll use a multiline lambda (may have few things to initialize to do setting up of these maps
            Mapper.Initialize(config =>
            {
                config.CreateMap<TripViewModel, Trip>().ReverseMap();//with generic arguments to & from - and they will map all the field names - source to destination
                config.CreateMap<StopViewModel, Stop>().ReverseMap(); //reverse for bi-directional mapping
            });
            //order is important as it will hand it to each middleware in its order!! previously relied on global.asax files (asp.net)


            //            app.UseDefaultFiles();// needs to be first (before use static files-then automaticaly look for default files such as index.html, index.htm and others
            //removed after we added Index/cshtml

            //#if DEBUG
            //now that we added Ihosting environment as parameter of Configure, we can code for same
            ///if (env.IsDevelopment()) rather use
            if (env.IsEnvironment("Development"))// knows its development .. inside of project - properties (alt enter) (under debug)
                                                 // here we set if staging or production or testing or QA etc. simple string comparison (is cross platform)


            {
                app.UseDeveloperExceptionPage();//in its own package - now on exception we can see stack, Query, Cookies or Headers
                //add logger a dtell it the minimal level we want to log
                factory.AddDebug(LogLevel.Information);
            }
            else //non development
            {
                factory.AddDebug(LogLevel.Error); //Errors not as verbose as information
            }
            //#endif

            app.UseStaticFiles();//either autocreate or add onto - and see added file in project.jason
            //Tell app to use identity
            app.UseIdentity();
            //BUILD DEFAULT ROUTE
            app.UseMvc(config =>  // a way to tell it this controler - create a map to specify route - patern of a url 
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",   //pattern matching -  can match to specific controller {specifies the pattern} id"?" means optional - then set defaults
                    defaults: new { controller = "App", action = "Index" }//if app not specified - going to use index method on that controller
                   );               //what route belongs to which controller use a lamda to configure mvs
            });

            seeder.EnsureSeedData().Wait();// with wait it becomes a synchronous operation then its going to return
        }
    }
}