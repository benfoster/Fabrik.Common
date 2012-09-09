using System.Web.Mvc;
using Fabrik.Common.Web.Example.Models;

namespace Fabrik.Common.Web.Example.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewFactory viewFactory;
        
        public HomeController(IViewFactory viewFactory)
        {
            this.viewFactory = viewFactory;
        }
        
        [HttpGet, ImportModelStateFromTempData]
        public ActionResult Index()
        {
            return View(viewFactory.CreateView<HomeView>());
        }

        [HttpPost, ValidateModelState]
        public ActionResult Index(HomeCommand command)
        {
            // if we get here, ModelState is valid
            // save to db etc.
            return new AjaxAwareRedirectResult(Url.Action("index"))
                .AndAlert(this, AlertType.Success, "Subscription Received.", "Thank you for subscribing, we now have your most personal details. Mwah ha ha ha haaa!");
        }

        [HttpGet, AutoFormatResult]
        public ActionResult About()
        {
            return View(viewFactory.CreateView<AboutView>());
        }

        [HttpGet, AutoFormatResult]
        public ActionResult List(ListParameters parameters)
        {
            return View(viewFactory.CreateView<ListParameters, ListView>(parameters));
        }

        [HttpGet, NullModelCheck("Product not found."), AutoFormatResult]
        public ActionResult Details(int id)
        {
            return View(viewFactory.CreateView<int, ProductDetailsView>(id));
        }

        public ActionResult NotFound()
        {
            return new HttpStatusCodeViewResult(System.Net.HttpStatusCode.NotFound, "Not Found");
        }
    }
}
