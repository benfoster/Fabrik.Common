using System.Web.Mvc;
using Fabrik.Common.Web.Example.Models;

namespace Fabrik.Common.Web.Example.Application
{
    public class HomeViewBuilder : IViewBuilder<HomeView>
    {
        public HomeView Build()
        {
            var sources = new[] { "Google", "TV", "Radio", "A friend", "Crazy bloke down the pub" };

            return new HomeView
            {
                Message = "Welcome to ASP.NET MVC!",
                SubscriptionType = SubscriptionType.SilverSubscription, // default to silver subscription
                SubscriptionSourcesList = new SelectList(sources),
                SubscriptionSources = new[] { "Google" } // default to Google selection
            };
        }
    }
}