using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainX.Models;

namespace TrainX.Controllers
{
	public class UsersController : Controller
	{
		private readonly TrainXContext _context;

		public UsersController(TrainXContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var trainXContext = _context.Users.Include(u => u.Sport);
			return View(await trainXContext.ToListAsync());
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Login(string email, string password)
		{
			if (email == "Admin" && password == "Trainx832")
				return RedirectToAction(nameof(Index));

			var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
			if (user is null)
				return NotFound();

			return View("");
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Users == null)
			{
				return NotFound();
			}

			var user = await _context.Users
				.Include(u => u.Sport)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		public IActionResult Create()
		{
			ViewBag.Sports = new SelectList(_context.Sports.ToList(), "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,SportId")] User user)
		{
			if (ModelState.IsValid)
			{
				_context.Add(user);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Id", user.SportId);
			return View(user);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Users == null)
			{
				return NotFound();
			}

			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Id", user.SportId);
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,SportId")] User user)
		{
			if (id != user.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(user);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!UserExists(user.Id))
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
			ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Id", user.SportId);
			return View(user);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Users == null)
			{
				return NotFound();
			}

			var user = await _context.Users
				.Include(u => u.Sport)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Users == null)
			{
				return Problem("Entity set 'TrainXContext.Users'  is null.");
			}
			var user = await _context.Users.FindAsync(id);
			if (user != null)
			{
				_context.Users.Remove(user);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool UserExists(int id)
		{
			return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
