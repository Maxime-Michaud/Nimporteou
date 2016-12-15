using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nimporteou.Data;
using nimporteou.Models;
using nimporteou.Models.EvenementViewModels;

namespace nimporteou.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(EvenementViewModelFactory.CreerListe(_db, await _userManager.GetUserAsync(User), 5));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
