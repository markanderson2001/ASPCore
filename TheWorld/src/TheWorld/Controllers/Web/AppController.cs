//Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller //all controlers need to be public so they can be disovered
                                            //Controller class should be base for every controller in your MVC 6 project
                                            //on error add controler hold down Control period or use helper ...
                                            // .. we going to add it in project.jason

    // here we will specify model for a page or checking authorization
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;
        
        // private WorldContext _context; - have repository pattern instead

        //change controller to support debuging new constructor rather than; new debugMailService()
        //public AppController(IMailService mailService, IConfigurationRoot config, WorldContext context)//instantiates "Appcontroler - pass an implementation of this interface. Add EF WorldContext
        public AppController(IMailService mailService,
            IConfigurationRoot config,
            IWorldRepository repository,
            ILogger<AppController> logger) //namespace of ms.ext.logg than add in class level)//replace the injection of the context with the injection of the iWorldrepository

            //MEMBERS
        {
            _mailService = mailService;
            _config = config;
            // _context = context;
            _repository = repository;
            _logger = logger; //allows us to look and trap errors --so try catch incase something bad happens in query IActionresult
        }

        //METHOD
        public IActionResult Index()        //will return simple HTMl view  (no parameters)
        {
            //  var data = _context.Trips.ToList();//will get list of all the trips as those trip classes thus var data; return a list of trip objects
            //now we can take this data and pass it into the view:
            // No datbase provider has been configured for this DBContext
            try
            {
             //   var data = _repository.GetAllTrips();
                return View(); //remove data as it is hadled by after authentication
                //return View();             //will tell it to find, render that view and return to user
                // no we need the actual view - called a Razor page that represents this view
                // do this by creating a new set of directories - add new file theWorld/Views
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get trips in Index page: {ex.Message}");
                return Redirect("/error"); //or some operation if this fails
                    //point is logging the error, dont neccesary show to end user
                    //better than 500 error, catch them and do something about them

            }
            
        }
             //A place where Authenticated users can go to look at their trips - attribute
             [Authorize]///gate for only authed users
        public IActionResult Trips ()
        {
            //copy what we do in the index page
            var trips = _repository.GetAllTrips();
            return View(trips);
        }

            //NEW METHODS
        //method to allow by default to fo a GET on app/contact
        public IActionResult Contact() //For a contact page & here also return a view as well
        {
           // throw new InvalidOperationException("bad things happen to lazy people"); test Exception
              return View();
        }

        [HttpPost] //if user posts - this method gets instantiated
        public IActionResult Contact(ContactViewModel model)//will return through model binding - name,email,message
        {
            if (model.Email.Contains("aol.com"))
            {
               //ModelState.AddModelError("", "We don't support AOL Email Addresses"); //model error or
                ModelState.AddModelError("EMail", "We don't support AOL Email Addresses"); //property error
            }

            if (ModelState.IsValid)
            {
                    //Test agains model attributes on server

                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From TheWorld", model.Message);

                //clear date from model
                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";

            }
            //_mailService.SendMail("markanderson2001@live.com", model.Email, "From TheWorld", model.Message);
            //probably dont hardcode the tomail, as the answeing person may be different down the line
            //...so let's make it configurable, by using a configuration file - addd config.jason to our proj
            //THE ADDRESS CAN BE FOR EASY OF SCRIPTS ETC SET IN PROPRTIES -- AS WE DID IN DEBUG
            return View();//validated on the client through the model class before being sent to us (through implementing a service)
               
        }
        public IActionResult About() //For an About page & here also return a view as well
        {
            return View();
        }
    }
}

