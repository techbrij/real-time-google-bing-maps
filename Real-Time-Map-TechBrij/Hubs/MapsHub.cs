using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Microsoft.AspNet.SignalR;
using Real_Time_Map_TechBrij;
namespace Real_Time_Map_TechBrij.Hubs
{
    public class MapsHub : Hub
    {
        public IEnumerable<Location> GetLocations() {
            return FileWatcher.Data;
        }            
    }

   

}

