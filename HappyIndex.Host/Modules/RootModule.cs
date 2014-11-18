using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyPortal.Modules
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get["/"] = _ => View["index.html"];
        }        
    }
}