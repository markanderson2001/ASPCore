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

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller //all controlers need to be public so they can be disovered
                                            //Controller class should be base for everyh controller in your MVC 6 project
                                            //on error add controler hold down Control period or use helper ...
                                            // .. we going to add it in project.jason

                                            // here we will specify model for a page or checking authorization
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;

        //change controller to support debuging new constructor rather than; new debugMailService()
        public AppController(IMailService mailService, IConfigurationRoot config)//instantiates "Appcontroler - pass an implementation of this interface
        {
            _mailService = mailService;
            _config = config;
        } 

        public IActionResult Index()        //will return simple HTMl view  (no parameters)
        {
            return View();              //will tell it to find, render taht view and return to useri
                                        // no we need the actual view - called a Razor page that represents this view
                                        // do this by creating a new set of directories - add new file theWorld/Views
        }
        //Add new methods 
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

