using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class SmsController : Controller
    {
        //
        // GET: /Sms/

        public ActionResult Index(string Body)
        {
            // Tell Twilio what to do..
            var xmlString = string.Format(
                "<Response><Message>Hey want to buy this? Click on the link! http://battlehackfinal.cloudapp.net/buy?id={0} </Message></Response>",
                Body);
            return this.Content(xmlString, "text/xml");
        }
    }
}
