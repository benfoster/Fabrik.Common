using System.Collections.Generic;

namespace Fabrik.Common.Web.Example.Models
{
    public class AboutView
    {
        public IEnumerable<string> Locations { get; set; }
        
        public AboutView()
        {
            Locations = new[] { "United Kingdom", "Belgium", "United States" };
        }
    }
}