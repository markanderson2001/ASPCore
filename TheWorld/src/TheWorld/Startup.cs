using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TheWorld
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();// we are required to use deppendency injection - here we register all the MVC services
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //middleware im going to hand you some code to do something as a request comes in (smallest piece of middleware here
        // previous version of asp.net with global asax - event handlers in different points)

        //We are defining who is handling what and in what order

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)// to set up what to do when requests come in
        {
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
            }
//#endif
    
            app.UseStaticFiles();//either autocreate or add onto - and see added file in project.jason

            app.UseMvc(config =>  // a way to tell it this controler - create a map to specify route - patern of a url 
            {
                config.MapRoute(
                    name: "Default",      
                    template: "{controller}/{action}/{id?}",   //pattern matching -  can match to specific controller {specifies the pattern} id"?" means optional - then set defaults
                    defaults: new { controller = "App", action = "Index" }//if app not specified - going to use index method on that controller
                   );               //what route belongs to which controller use a lamda to configure mvs
            });
        }
    }
}





                //loggerFactory.AddConsole();

                //if (env.IsDevelopment())
                //{
                //    app.UseDeveloperExceptionPage();
                //}

                ////app.Run(async (context) =>
                ////{
                ////    //await context.Response.WriteAsync("Hello World!");
                ////    //to return it as a webapage:
                ////    await context.Response.WriteAsync("<html><body><h3>Hello World!</h3></body></html>");
                ////});

            
         


