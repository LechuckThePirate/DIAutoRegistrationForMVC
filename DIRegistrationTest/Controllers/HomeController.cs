using System.Web.Mvc;
using DependencyInjection.ServiceLibrary.Classes;
using DIRegistrationTest.Services;
using DITest.Contracts.Interfaces;

namespace DIRegistrationTest.Controllers
{
    public class HomeController : Controller
    {

        private IExternalService _myService;
        private ILocalService _anotherService;

        public HomeController(IExternalService myService, ILocalService anotherService)
        {
            _myService = myService;
            _anotherService = anotherService;
        }

        public ActionResult Index()
        {
            _myService.TerrificMethodOne();
            ViewBag.myServiceResult = _myService.SayHello();
            ViewBag.anotherServiceResult = _anotherService.GuessWhat();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}