using System.Web.Mvc;
using Fabrik.Common.Web.Example.Models;

namespace Fabrik.Common.Web.Example.Controllers
{
    public class HomeController : Controller
    {
        [ImportModelStateFromTempData]
        public ActionResult Index()
        {
            var homeView = new HomeViewFactory().CreateView();            
            return View(homeView);
        }

        [HttpPost]
        [ValidateModelState]
        public ActionResult Index(HomeCommand command)
        {
            // if we get here, ModelState is valid
            // save to db etc.
            return RedirectToAction("index")
                .AndAlert(AlertType.Success, "Subscription Received.", "Thank you for subscribing, we now have your most personal details. Mwah ha ha ha haaa!");
        }

        [AutoFormatResult]
        public ActionResult About()
        {
            var locations = new[] { "United Kingdom", "Belgium", "United States" };
            return View(locations);
        }
    }

    public class HomeViewFactory
    {
        public HomeView CreateView()
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
