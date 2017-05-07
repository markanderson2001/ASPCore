using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;


namespace TheWorld
{
    public class Program
    {
        public static void Main(string[] args) /*Starting point of all Code */
        {
            var host = new WebHostBuilder() /* builds a web host that starts to listen to requests*/ /*Allows to specify what is important eg: usekestrel ..*/
                .UseKestrel()       /*name of webserver under asp.net core*/
                .UseContentRoot(Directory.GetCurrentDirectory()) /* where content is for project*/
                .UseIISIntegration()/*add some support here mostly for IIS - like specialized headers and windows authentication*/
                .UseStartup<Startup>()/* very important - go ahead and use a class called startup and instantiate it when you start the webhost -  to setup my webserver*/
                .Build();

            host.Run();
        }
    }
}
