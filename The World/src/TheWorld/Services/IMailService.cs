using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.services
{
    public interface IMailService
    {
        //create architecture of what each mail service will require
        void SendMail(string to, string from, string subject, string body);

        //Create a service - based on above Interface - folder in Services; DebugMailService class
    }
}
