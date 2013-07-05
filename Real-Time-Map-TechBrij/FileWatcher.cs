using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Microsoft.AspNet.SignalR;
using Real_Time_Map_TechBrij.Hubs;

namespace Real_Time_Map_TechBrij
{
    public class Location
    {
        public String Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public static class FileWatcher
    {
        private static FileSystemWatcher fWatcher;
        private static String xmlFilePath = HttpContext.Current.Server.MapPath("~/App_Data/location.xml");
        private static List<Location> objLoc = new List<Location>();


        public static IEnumerable<Location> Data
        {
            get
            {
                return objLoc.AsReadOnly(); 
            }
        }

        /// <summary>
        /// One Time Initialization
        /// </summary>
        public static void init()
        {
            Initialize();
            ReadNewLines();
        }


        /// <summary>
        /// To setup FileSystemWatcher
        /// </summary>
        private static void Initialize()
        {
            fWatcher = new FileSystemWatcher();
            fWatcher.Path = Path.GetDirectoryName(xmlFilePath);
            fWatcher.Filter = Path.GetFileName(xmlFilePath);
            fWatcher.Changed += watcher_Changed;
            fWatcher.EnableRaisingEvents = true;
            fWatcher.Error += OnError;
        }

        private static void OnError(object sender, ErrorEventArgs e)
        {
            fWatcher.Dispose();
            Initialize();
        }


        private static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                fWatcher.EnableRaisingEvents = false;
                var newLoc = ReadNewLines();
                //Pass new points to client
                var context = GlobalHost.ConnectionManager.GetHubContext<MapsHub>();
                context.Clients.All.addLocations(newLoc);
            }
            finally
            {
                fWatcher.EnableRaisingEvents = true;
            }

        }

        /// <summary>
        /// To read new lines 
        /// </summary>
        public static IEnumerable<Location> ReadNewLines()
        {
            XDocument xmlDoc;
            using (Stream s = File.OpenRead(xmlFilePath))
            {
                xmlDoc = XDocument.Load(s);
            }
            int total = objLoc.Count();
            var newLoc = (from item in xmlDoc.Descendants("location").Select((x, index) => new { x, index })
                          where item.index >= total
                          select new Location { Name = (String)item.x.Attribute("name"), Lat = (double)item.x.Attribute("lat"), Lng = (double)item.x.Attribute("lng") });
            objLoc.AddRange(newLoc);
            return newLoc;
        }

    }
}