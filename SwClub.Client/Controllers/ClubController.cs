using Client.ConsumeAPI.APIClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SwClub.Client.ModelView;

namespace SwClub.Client.Controllers
{
    public class ClubController : Controller
    {
        private IAPIClientService<ClubModelView> _iAPIClientService;
        private readonly RestClient _RestClient;
        private readonly string _baseurl = "https://localhost:7283/api/";
        // GET: HomeController1

        public ClubController(IAPIClientService<ClubModelView> aPIClientService)
        {
            _iAPIClientService = aPIClientService;
            _RestClient = new RestClient();

        }
        public async Task<IActionResult> Index()
        {

            string _subURL = "club/getAll";
            var clubss = await _iAPIClientService.GetAll(_subURL);

            var lx = new List<ClubModelView>();
            var x = new ClubModelView()
            {
                Name = "Home",
                Description = "Home",
            };
            lx.Add(x);
            lx.Add(x);
            return View(clubss);        
        }

        // GET: HomeController1/Details/5

        public ActionResult Details(int id)
        {
            var cl = new ClubModelView()
            {
                Name = "ad",
                Description = "d"
            };
            return View(cl);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
