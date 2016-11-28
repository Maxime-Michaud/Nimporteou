using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nimporteou.Data;
using nimporteou.Models.EvenementViewModels;
using Microsoft.AspNetCore.Identity;
using nimporteou.Models;

namespace nimporteou.Controllers
{
    public class EvenementController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        public EvenementController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index(int? id)
        {
            //Get l'eventnement
            if (id != null)
            {
                Evenement ev =  _db.Evenements.Where(e => e.id == id).FirstOrDefault();
                if (ev != null)
                {
                    ConsultationEvenementViewModel cev = new ConsultationEvenementViewModel();

                    cev.EvenementID = ev.id;
                    cev.Nom = ev.Nom;
                    cev.Description = ev.Description;
                    //cev.Public = ev.Public;       <== sa existe pas
                    cev.DateLimite = ev.DateLimite;
                    //cev.VenteBillet               <== sa existe pas
                    cev.PrixBillet = ev.PrixBillet;
                    cev.Debut = ev.Debut;
                    cev.Fin = ev.Fin;
                    cev.Categorie = ev.Categorie.Nom;
                    cev.AdresseComplete = ev.Endroit.ToString();
                    cev.CheminPhoto = ev.CheminPhoto;

                    return View(cev);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Creer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Creer(CreationEvenementViewModel e)
        {
            //todo creer et enregistrer dans la bd
            //Creer l'evenement
            Evenement ev = new Evenement();
            ev.Nom = e.Nom;
            int id = 0;
            return RedirectToAction("Index", id);
        }
    }
}