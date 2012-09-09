using System.Collections.Generic;

namespace Fabrik.Common.Web.Example.Models
{
    public class AboutView
    {
        public List<string> Locations { get; set; }
        
        public AboutView()
        {
            Locations = new List<string>(new[] { "United Kingdom", "Belgium", "United States" });
        }
    }
}