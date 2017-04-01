using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller //all controlers need to be public so they can be disovered
                                            //Controller class should be base for everyh controller in your MVC 6 project
                                            //on error add controler hold down Control period or use helper ...
                                            // .. we going to add it in project.jason

                                            // here we will specify model for a page or checking authorization
    {
        public IActionResult Index()        //will return simple HTMl view  (no parameters)
        {
            return View();              //will tell it to find, render taht view and return to user
                                        // no we need the actual view - called a Razor page that represents this view
                                        // do this by creating a new set of directories - add new file thewrold/Views
        }
        //Add new methods 
        public IActionResult Contact() //For a contact page & here also return a view as well
        {
           // throw new InvalidOperationException("bad things happen to lazy people"); test Exception

            return View();
        }

        public IActionResult About() //For an About page & here also return a view as well
        {
            return View();
        }
    }
}
