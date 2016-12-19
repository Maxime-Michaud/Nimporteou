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

        /// <summary>
        /// Constructeur du controleur
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userManager"></param>
        /// <param name="environment"></param>
        public EvenementController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _db = db;
            _userManager = userManager;
            _environment = environment;
        }

        /// <summary>
        /// Gere la page Index qui affiche les événements public
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async public Task<IActionResult> Index(int? id)
        {
            //Get l'eventnement
            if (id != null)
            {
                return RedirectToAction("Consulter", "evenement", id);
            }

            return View(EvenementViewModelFactory.CreerListe(_db, await _userManager.GetUserAsync(HttpContext.User)));
        }

        /// <summary>
        /// Gere la page MesEvenement qui contient les événements crée par l'utilisateur ou bien des événements
        /// dont il est organisateur.
        /// </summary>
        /// <param name="id">id d'un événement, si null affiche tous les événements,
        /// sinon ouvre la page de consultations de l'événement</param>
        /// <returns></returns>
        async public Task<IActionResult> MesEvenement(int? id)
        {
            //Get l'événement
            if (id != null)
            {
                return RedirectToAction("Consulter", "evenement", id);
            }

            return View(EvenementViewModelFactory.CreerListeOrganisateur(_db, await _userManager.GetUserAsync(HttpContext.User)));
        }

        /// <summary>
        /// TODO pcq -_-
        /// </summary>
        /// <returns></returns>
        [Authorize]
        async public Task<IActionResult> Participation()
        {
            return View(EvenementViewModelFactory.CreerListeParticipation(_db, await _userManager.GetUserAsync(HttpContext.User)));
        }

        /// <summary>
        /// Crée la liste de catégorie
        /// </summary>
        /// <returns></returns>
        public IActionResult Creer()
        {
            List<SelectListItem> lstCat = new List<SelectListItem>();
            foreach (var item in _db.Categories)
            {
                lstCat.Add(new SelectListItem { Text = item.Nom, Value = item.Nom });
            }
            ViewBag.Categories = lstCat;
            return View();
        }

        /// <summary>
        /// Gere la page de création d'événement
        /// </summary>
        /// <param name="e"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost, Authorize]
        public async Task<IActionResult> Creer(CreationEvenementViewModel e, ICollection<IFormFile> files)
        {
            // Vérifie si le model est valide
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var nid = await CreerEvenement(e, files, user);
                return RedirectToAction("Consulter", new { id = nid });
            }

            return RedirectToAction("Creer");
        }

        /// <summary>
        /// Gere la page de consultation d'un événement (la même pour le Créateur/Organisateur et les autres)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Consulter(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //Get l'eventnement
            if (id != null)
            {
                Evenement ev = _db.Evenements.Include(a => a.Categorie).Include(a => a.Endroit.Ville).Where(e => e.id == id).FirstOrDefault();

                if (ev != null)
                {
                    ConsultationEvenementViewModel cev = new ConsultationEvenementViewModel();

                    cev.EvenementID = ev.id;
                    cev.Nom = ev.Nom;
                    cev.Description = ev.Description;
                    cev.DateLimite = ev.DateLimite;
                    cev.PrixBillet = ev.PrixBillet;
                    cev.Debut = ev.Debut;
                    cev.Fin = ev.Fin;
                    cev.Categorie = ev.Categorie.Nom;
                    cev.AdresseComplete = ev.Endroit.ToString();
                    cev.CheminPhoto = ev.CheminPhoto;
                    cev.HeureDebut = ev.HeureDebut;
                    cev.Duree = ev.Duree;

                    //Si le user est le Créateur ou un Organisateur == Peut modifier l'événement
                    if (_db.Participations.Where(a=>a.Evenement_id == id && a.Participant_id == user.Id &&(a.Role == Role.Createur || a.Role == Role.Organisateur)).FirstOrDefault() != null)
                    {
                        cev.peutAdministrer = true;
                        cev.peutInscrire = false;
                    }
                    else if (_db.Participations.Where(a => a.Evenement_id == id && a.Participant_id == user.Id && a.Role == Role.Participant).FirstOrDefault() != null)
                    {
                        cev.peutAdministrer = false;
                        cev.peutInscrire = false;
                    }
                    else
                    {
                        cev.peutAdministrer = false;
                        cev.peutInscrire = true;
                    }

                    return View(cev);
                }
            }
            return View();
        }

        /// <summary>
        /// Créer un événement
        /// </summary>
        /// <param name="e">Le model de la vue de création événement</param>
        /// <param name="files">Le </param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> CreerEvenement(CreationEvenementViewModel e, ICollection<IFormFile> files, ApplicationUser user)
        {
            //Creer l'evenement
            Evenement ev = new Evenement();
            ev.Nom = e.Nom;
            ev.Description = e.Description;
            ev.Public = e.Public;
            ev.DateLimite = e.DateLimite;
            ev.HeureDebut = e.HeureDebut;
            ev.BilletNecessaire = e.BilletNecessaire;
            ev.PrixBillet = e.PrixBillet;
            ev.Debut = e.Debut;
            ev.Fin = e.Fin;
            ev.Categorie = _db.Categories.Where(a => a.Nom == e.Categorie).FirstOrDefault();
            ev.AgeMinimum = e.AgeMinimum;
            if (e.Duree != null)
            {
                ev.Duree = e.Duree;
            }

            Adresse ad = new Adresse();
            if (e.AdresseComplete != null)
            {
                ad.Ad = e.AdresseComplete;
                if (e.Ville != null)
                {
                    string ville = e.Ville;
                    if (_db.Villes.Where(a => a.Nom == ville).FirstOrDefault() != null)
                    {
                        ad.Ville = _db.Villes.Where(a => a.Nom == ville).FirstOrDefault();
                    }
                    else
                    {
                        Ville vi = new Models.Ville();
                        vi.Nom = ville;
                        _db.Villes.Add(vi);
                        _db.SaveChanges();
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
            _db.Evenements.Add(ev);
            _db.SaveChanges();
            //Ajoute le créateur comme participant de role Createur, donc il peux éditer l'événement.
            Participation part = new Participation();
            part.Evenement = _db.Evenements.Include(a=>a.Endroit.Ville).Where(a=>a.id == ev.id).First();
            part.NombreParticipants = 1;
            part.Participant = user;
            part.Role = Role.Createur;

            _db.Participations.Add(part);
            _db.SaveChanges();
            return _db.Evenements.Where(a=>a.id == ev.id).Select(a => a.id).First();
        }

        /// <summary>
        /// Gere l'affichage de la page de modification d'un événement 
        /// </summary>
        /// <param name="id">id de l'événement</param>
        /// <param name="user">l'utilisateur connecter</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Modifier(int? id, ApplicationUser user)
        {
            //Get l'événement
            if (id != null)
            {
                Evenement ev = _db.Evenements.Include(a => a.Categorie).Include(a => a.Endroit.Ville).Where(e => e.id == id).FirstOrDefault();
                if (ev != null)
                {
                    CreationEvenementViewModel cev = new CreationEvenementViewModel();

                    cev.EvenementID = ev.id;
                    cev.Nom = ev.Nom;
                    cev.Description = ev.Description;
                    cev.DateLimite = ev.DateLimite;
                    cev.HeureDebut = ev.HeureDebut;
                    cev.Duree = ev.Duree;
                    cev.Debut = ev.Debut;
                    cev.Fin = ev.Fin;
                    cev.Categorie = ev.Categorie.Nom;
                    cev.AdresseComplete = ev.Endroit.Ad;
                    cev.Ville = ev.Endroit.Ville.Nom;
                    cev.CheminPhoto = ev.CheminPhoto;

                    return View(cev);
                }
            }
            return View();
        }

        /// <summary>
        /// Gere la modification d'un événement 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="e"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost, Authorize]
        public async Task<IActionResult> Modifier(int id, CreationEvenementViewModel e, ICollection<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var nid = await ModifierEvenement(id, e, files, user);
                return RedirectToAction("Consulter", new { id = nid });
            }

            return RedirectToAction("Modifier", id);
        }

        /// <summary>
        /// Méthode pour modifier un événement
        /// </summary>
        /// <param name="id">id de l'événement</param>
        /// <param name="e"></param>
        /// <param name="files">Le fichier a uploader</param>
        /// <param name="user">L'utilisateur connecter</param>
        /// <returns></returns>
        public async Task<int> ModifierEvenement(int id, CreationEvenementViewModel e, ICollection<IFormFile> files, ApplicationUser user)
        {
            //Modifier l'événement
            Evenement ev = _db.Evenements.Include(a => a.Endroit.Ville).Where(a => a.id == id).FirstOrDefault();
            if (ev != null)
            {
                ev.Nom = e.Nom;
                ev.Description = e.Description;
                ev.DateLimite = e.DateLimite;
                ev.HeureDebut = e.HeureDebut;
                if (e.Duree != null)
                {
                    ev.Duree = e.Duree;
                }
                ev.Debut = e.Debut;
                ev.Fin = e.Fin;
                if (ev.Endroit.Ad != e.AdresseComplete || ev.Endroit.Ville.Nom != e.Ville)
                {
                    if (_db.Adresses.Include(a => a.Ville).Where(a => a.ToString() == e.AdresseComplete).FirstOrDefault() == null)
                    {
                        Adresse ad = new Adresse();
                        ad.Ad = e.AdresseComplete;
                        if (ev.Endroit.Ville.Nom != e.Ville)
                        {
                            if (_db.Villes.Where(a=>a.Nom == e.Ville).FirstOrDefault() == null)
                            {
                                Ville vi = new Ville();
                                vi.Nom = e.Ville;
                                ad.Ville = vi;
                            }
                            else
                            {
                                ad.Ville = _db.Villes.Where(a => a.Nom == e.Ville).First();
                            }  
                        }
                        else
                        {
                            ad.Ville = _db.Villes.Where(a => a.Nom == e.Ville).First();
                        }
                        ev.Endroit = ad;
                    }
                    else
                    {
                        ev.Endroit.Ad = _db.Adresses.Where(a => a.Ad == e.AdresseComplete).First().Ad;
                    }
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
                            string CheminPhoto = "/uploads/" + file.FileName;
                            if (ev.CheminPhoto != CheminPhoto)
                            {
                                if (CheminPhoto != null)
                                {
                                    await file.CopyToAsync(fileStream);
                                    ev.CheminPhoto = "/uploads/" + file.FileName;
                                }      
                            }
                        }
                    }
                }
            }
            _db.SaveChanges();
            return _db.Evenements.Where(a => a.id == ev.id).Select(a => a.id).First();
        }

        /// <summary>
        /// Ouvre la page pour l'inscription d'un événement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Inscription(int? id)
        {
            //Get l'eventnement
            if (id != null)
            {
                Evenement ev = _db.Evenements.Include(a => a.Categorie).Include(a => a.Endroit.Ville).Where(e => e.id == id).FirstOrDefault();

                if (ev != null)
                {
                    ConsultationEvenementViewModel cev = new ConsultationEvenementViewModel();

                    cev.EvenementID = ev.id;
                    cev.Nom = ev.Nom;
                    cev.Description = ev.Description;
                    cev.DateLimite = ev.DateLimite;
                    cev.PrixBillet = ev.PrixBillet;
                    cev.Debut = ev.Debut;
                    cev.Fin = ev.Fin;
                    cev.Categorie = ev.Categorie.Nom;
                    cev.AdresseComplete = ev.Endroit.ToString();
                    cev.CheminPhoto = ev.CheminPhoto;
                    cev.HeureDebut = ev.HeureDebut;
                    cev.Duree = ev.Duree;

                    return View(cev);
                }
            }
            return View();
        }

        /// <summary>
        /// Gere l'inscription à un événement
        /// </summary>
        /// <param name="nbr_participant"></param>
        /// <param name="evenement_id"></param>
        /// <returns></returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> InscriptionEvenement(int nbr_participant, int evenement_id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                Inscrire(nbr_participant, evenement_id, user);
                return RedirectToAction("Consulter", new { id = evenement_id });
            }

            return RedirectToAction("Inscription", evenement_id);
        }

        /// <summary>
        /// Méthode qui permet à l'utilisateur de s'inscrire à un événement gratuit en tant que participant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="evenement_id"></param>
        /// <param name="user"></param>
        public void Inscrire(int nbr_participant, int evenement_id, ApplicationUser user)
        {
            if (_db.Participations.Where(a => a.Participant_id == user.Id && a.Evenement_id == evenement_id && a.Role == Role.Participant).FirstOrDefault() == null)
            {
                Participation insciption = new Participation();
                insciption.Evenement_id = evenement_id;
                insciption.Evenement = _db.Evenements.Where(a => a.id == evenement_id).First();
                insciption.Participant_id = user.Id;
                insciption.Participant = user;
                insciption.Role = Role.Participant;
                insciption.NombreParticipants = nbr_participant;
                _db.Participations.Add(insciption);
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Gere le signalement d'un événement
        /// </summary>
        /// <param name="evenement_id">id de l'événement</param>
        /// <returns></returns>
        [HttpGet, Authorize]
        public async Task<IActionResult> Signaler(int evenement_id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                Signalement(evenement_id, user);
                return RedirectToAction("Consulter", new { id = evenement_id });
            }

            return RedirectToAction("Consulter", evenement_id);
        }

        /// <summary>
        /// Méthode qui permet à l'utilisateur de signaler un événement
        /// </summary>
        /// <param name="evenement_id">id de l'événement</param>
        /// <param name="user">l'utilisateur connecter</param>
        public void Signalement(int evenement_id, ApplicationUser user)
        {
            if (_db.Participations.Where(a=>a.Participant_id == user.Id && a.Evenement_id == evenement_id && a.Role == Role.Signalement).FirstOrDefault() == null)
            {
                Participation insciption = new Participation();
                insciption.Evenement_id = evenement_id;
                insciption.Evenement = _db.Evenements.Where(a => a.id == evenement_id).First();
                insciption.Participant_id = user.Id;
                insciption.Participant = user;
                insciption.Role = Role.Signalement;
                insciption.NombreParticipants = 1;
                _db.Participations.Add(insciption);
                _db.SaveChanges();
            }     
        }

        /// <summary>
        /// Ouvre la page pour l'inscription d'un événement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Invitation(int? evenement_id)
        {
            //Get l'eventnement
            if (evenement_id != null)
            {
                Evenement ev = _db.Evenements.Include(a => a.Categorie).Include(a => a.Endroit.Ville).Where(e => e.id == evenement_id).FirstOrDefault();

                if (ev != null)
                {
                    ConsultationEvenementViewModel cev = new ConsultationEvenementViewModel();

                    cev.EvenementID = ev.id;
                    cev.Nom = ev.Nom;
                    cev.Description = ev.Description;
                    cev.DateLimite = ev.DateLimite;
                    cev.PrixBillet = ev.PrixBillet;
                    cev.Debut = ev.Debut;
                    cev.Fin = ev.Fin;
                    cev.Categorie = ev.Categorie.Nom;
                    cev.AdresseComplete = ev.Endroit.ToString();
                    cev.CheminPhoto = ev.CheminPhoto;
                    cev.HeureDebut = ev.HeureDebut;
                    cev.Duree = ev.Duree;

                    return View(cev);
                }
            }
            return View();
        }

        /// <summary>
        /// Gere l'invitation à un événement
        /// </summary>
        /// <param name="evenement_id"></param>
        /// <returns></returns>
        [HttpGet, Authorize]
        public IActionResult Inviter(int evenement_id, string nom)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.Where(a => a.UserName == nom).FirstOrDefault();
                if (user != null)
                {
                    InvitationUtilisateur(evenement_id, user);
                    return RedirectToAction("Consulter", new { id = evenement_id });
                }
            }
            return RedirectToAction("Consulter", evenement_id);
        }

        /// <summary>
        /// Méthode qui permet à l'utilisateur d'inviter un utilisateur à un événement
        /// </summary>
        /// <param name="id"></param>
        /// <param name="evenement_id"></param>
        /// <param name="user"></param>
        public void InvitationUtilisateur(int evenement_id, ApplicationUser user)
        {
            if (_db.Participations.Where(a => a.Participant_id == user.Id && a.Evenement_id == evenement_id && (a.Role == Role.Participant || a.Role == Role.Organisateur || a.Role == Role.Createur || a.Role == Role.Inviter)).FirstOrDefault() == null)
            { 
                Participation insciption = new Participation();
                insciption.Evenement_id = evenement_id;
                insciption.Evenement = _db.Evenements.Where(a => a.id == evenement_id).First();
                insciption.Participant_id = user.Id;
                insciption.Participant = user;
                insciption.Role = Role.Inviter;
                insciption.NombreParticipants = 1;
                _db.Participations.Add(insciption);
                _db.SaveChanges();
            }
        }
    }    
}
