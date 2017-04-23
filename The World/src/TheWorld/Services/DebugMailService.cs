using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.services
{
    //driven from mail service interface (can also use "Ctrl .") to impliment interface for us
    public class DebugMailService : IMailService

        //starting point for implimentation of the services
    {
        public void SendMail(string to, string from, string subject, string body)
        {
            //we're not actually going to send the mail, throw data in output window - for testing
            // !! Impliment own mailservice - to send mail in
            //throw new NotImplementedException();
            //use $ sign string inripolation syntax
            Debug.WriteLine($"Sending Mail: To {to} FROM {from} Subject {subject} ");
        }
    }
}
