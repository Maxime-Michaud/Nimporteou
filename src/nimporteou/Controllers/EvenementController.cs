using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nimporteou.Data;
using nimporteou.Models.EvenementViewModels;
using Microsoft.AspNetCore.Identity;
using nimporteou.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace nimporteou.Controllers
{
    public class EvenementController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _environment;

        public EvenementController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _db = db;
            _userManager = userManager;
            _environment = environment;
        }

        public IActionResult Index(int? id)
        {
            //Get l'eventnement
            if (id != null)
            {
                return RedirectToAction("Consulter", id);
            }
            return View();
        }

        public IActionResult Creer()
        {
            /*var user = _userManager.GetUserId(HttpContext.User);
            if (user == null)
            {
                return Redirect("Login.aspx");
            }*/

            List<SelectListItem> lstCat = new List<SelectListItem>();
            foreach (var item in _db.Categories)
            {
                lstCat.Add(new SelectListItem { Text = item.Nom, Value = item.Nom });
            }
            ViewBag.Categories = lstCat;
            return View();
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Creer(CreationEvenementViewModel e, ICollection<IFormFile> files)
        {
            //todo creer et enregistrer dans la bd
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var nid = await CreerEvenement(e, files, user);
                return RedirectToAction("Consulter", new { id = nid });
            }

            return RedirectToAction("Creer");
        }

        [HttpGet]
        public IActionResult Consulter(int? id)
        {
            //Get l'eventnement
            if (id != null)
            {
                Evenement ev = _db.Evenements.Include(a => a.Categorie).Include(a => a.Endroit.Ville).Where(e => e.id == id).FirstOrDefault();
                /*if (!ev.Public)
                {

                }*/
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

        public async Task<int> CreerEvenement(CreationEvenementViewModel e, ICollection<IFormFile> files, ApplicationUser user)
        {
            //Creer l'evenement
            Evenement ev = new Evenement();
            ev.Nom = e.Nom;
            ev.Description = e.Description;
            ev.Public = e.Public;
            ev.DateLimite = e.DateLimite;
            ev.BilletNecessaire = e.BilletNecessaire;
            ev.PrixBillet = e.PrixBillet;
            ev.Debut = e.Debut;
            ev.Fin = e.Fin;
            ev.Categorie = _db.Categories.Where(a => a.Nom == e.Categorie).FirstOrDefault();
            ev.AgeMinimum = e.AgeMinimum;
            ev.HeureDebut = e.HeureDebut;
            if (e.Duree != null)
            {
                ev.Duree = e.Duree;
            }

            Adresse ad = new Adresse();
            string adresseComplete = e.AdresseComplete;
            string noCivique;
            string rue;
            string ville;
            if (adresseComplete != null)
            {
                adresseComplete = adresseComplete.Trim();
                adresseComplete = adresseComplete.Replace(',', ' ');
                string[] data = adresseComplete.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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

            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            //todo :  ajouter le dossier du user au path et changer le nom de l'image! verifier si elle existe -> remplace ou renomme?
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    //string nfilename = file.FileName + _db.Users.Where(a => a.Id.ToString() == user).Select(a=> a.UserName);
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        ev.CheminPhoto = "/uploads/" + file.FileName;
                    }
                }
            }
            Participation part = new Participation();
            part.Evenement = ev;
            part.NombreParticipants = 1;
            part.Participant = user;
            part.Role = Role.Createur;

            _db.Participations.Add(part);
            //_db.Evenements.Add(ev);
            _db.SaveChanges();
            return _db.Evenements.Where(a=>a.id == ev.id).Select(a => a.id).First();
        }
    }
}