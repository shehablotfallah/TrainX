using Microsoft.AspNetCore.Mvc;
using TrainX.Models;

namespace TrainX.Controllers
{
	public class VideoController : Controller
	{
		private readonly TrainXContext _context;

		public VideoController(TrainXContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var video = _context.Videos.ToList();
			return View(video);
		}
	}
}
