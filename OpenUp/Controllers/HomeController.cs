using Microsoft.AspNetCore.Mvc;
using OpenUp.DataAccess.Data;
using OpenUp.Models;
using System.Diagnostics;

namespace OpenUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _db;
        public HomeController(AppDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }


        
        public IActionResult Index()
        {
            
            string user = User.Identity.Name;
            IEnumerable<Blog> objBlogList = _db.Blogs.Where(b => b.Author != user);
            if (User.Identity.IsAuthenticated)
            {
                return View(objBlogList);

            }

            return View("~/Views/Account/Login.cshtml");
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