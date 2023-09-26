using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainX.Models;

namespace TrainX.Controllers
{
	public class CoachesController : Controller
	{
		private readonly TrainXContext _context;

		public CoachesController(TrainXContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return _context.Coaches != null ?
						View(await _context.Coaches.ToListAsync()) :
						Problem("Entity set 'TrainXContext.Coaches'  is null.");
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Coaches == null)
			{
				return NotFound();
			}

			var coach = await _context.Coaches
				.FirstOrDefaultAsync(m => m.Id == id);
			if (coach == null)
			{
				return NotFound();
			}

			return View(coach);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Email,Password")] Coach coach)
		{
			if (ModelState.IsValid)
			{
				_context.Add(coach);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(coach);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Coaches == null)
			{
				return NotFound();
			}

			var coach = await _context.Coaches.FindAsync(id);
			if (coach == null)
			{
				return NotFound();
			}
			return View(coach);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password")] Coach coach)
		{
			if (id != coach.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(coach);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CoachExists(coach.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(coach);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Coaches == null)
			{
				return NotFound();
			}

			var coach = await _context.Coaches
				.FirstOrDefaultAsync(m => m.Id == id);
			if (coach == null)
			{
				return NotFound();
			}

			return View(coach);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Coaches == null)
			{
				return Problem("Entity set 'TrainXContext.Coaches'  is null.");
			}
			var coach = await _context.Coaches.FindAsync(id);
			if (coach != null)
			{
				_context.Coaches.Remove(coach);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CoachExists(int id)
		{
			return (_context.Coaches?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
