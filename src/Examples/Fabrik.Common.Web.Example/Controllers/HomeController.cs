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
            return RedirectToAction("index");
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
            var subscriptionTypes = new SelectList(new[] { "Bronze", "Silver", "Gold" });
            return new HomeView { 
                Message = "Welcome to ASP.NET MVC!", 
                SubscriptionTypes = subscriptionTypes
            };
        }
    }
}
