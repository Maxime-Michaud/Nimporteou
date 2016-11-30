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
            if (ModelState.IsValid)
            {
                //Creer l'evenement
                Evenement ev = new Evenement();
                ev.Nom = e.Description;
                ev.Description = e.Nom;
                ev.Public = e.Public;
                ev.DateLimite = e.DateLimite;
                ev.BilletNecessaire = e.BilletNecessaire;
                ev.PrixBillet = e.PrixBillet;
                ev.Debut = e.Debut;
                ev.Fin = e.Fin;

                Categorie cat = new Categorie();
                cat.Nom = e.Categorie;
                if (_db.Categories.Where(a => a.Nom == cat.Nom).FirstOrDefault() == null)
                {
                    _db.Categories.Add(cat);
                    ev.Categorie = cat;
                }
                else
                {
                    ev.Categorie = _db.Categories.Where(a => a.Nom == cat.Nom).FirstOrDefault();
                }
                ev.AgeMinimum = e.AgeMinimum;
                Adresse ad = new Adresse();

                string adresseComplete = e.AdresseComplete;
                string noCivique;
                string rue;
                string ville;
                if (adresseComplete != null)
                {
                    adresseComplete = adresseComplete.Trim();
                    adresseComplete = adresseComplete.Replace(',',' ');
                    string []data = adresseComplete.Split(' ');
                    noCivique = data[0].ToString();
                    rue = data[1].ToString();
                    ville = data[2].ToString();

                    int x = 0;
                    if (Int32.TryParse(noCivique, out x))
                    {
                        ad.NumeroCivique = x;
                    }
                    if (rue != null)
                    {
                        ad.Rue = rue;
                    }
                    if (ville != null)
                    {
                        if (_db.Villes.Where(a => a.Nom == ville).FirstOrDefault() != null)
                        {
                            ad.Ville = _db.Villes.Where(a => a.Nom == ville).FirstOrDefault();
                        }
                        else
                        {
                            Ville vi = new Models.Ville();
                            vi.Nom = ville;
                            _db.Villes.Add(vi);
                            ad.Ville = vi;
                        }
                    }
                }
                if (ad != null)
                {
                    ev.Endroit = ad;
                }
                ev.CheminPhoto = e.CheminPhoto;

                _db.Evenements.Add(ev);
                _db.SaveChanges();
                return RedirectToAction("Index", ev.id);
            }

            return View();
        }
    }
}