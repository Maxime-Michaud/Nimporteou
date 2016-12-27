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
            ViewData["Message"] = "Cette appliquation permet aux gens de voir et d'organiser les événements plus facilement";

            return View();
        }

        public IActionResult Support()
        {
            ViewData["Message"] = "Vous avez des difficultés à utiliser notre site? Laissé le nous savoir et nous corrigerons le tir!";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "N'hésitez pas à nous contacter si vous avez des question!";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
