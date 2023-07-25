using Microsoft.AspNetCore.Mvc;
using SwClub.Client.Models;
using SwClub.Client.ModelView;
using System.Diagnostics;

namespace SwClub.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            /*
            var lx = new List<ClubModelView>();
            var x = new ClubModelView()
            {
                Name = "Home",
                Description = "Homde",
            };
            lx.Add(x);
            lx.Add(x);*/
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}