using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrainX.Models;

namespace TrainX.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly TrainXContext _context;

        public HomeController(TrainXContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sport= _context.Sports.ToList();
            return View(sport);
        }

        public IActionResult Program()
        {
            var sport = _context.Sports.ToList();
            return View(sport);
        }

        public IActionResult Classes()
        {
            return View();
        }

        public IActionResult Contact()
        {
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