using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using nimporteou.Data;

namespace nimporteou.Models.EvenementViewModels
{
    static public class EvenementViewModelFactory
    {
        static public ListeEvenementViewModel CreerListe(ApplicationDbContext db, ApplicationUser user, int count = 25, int start = 0)
        {
            IQueryable<Evenement> evenements;

            evenements = db.Evenements.Include(e => e.Endroit.Ville);

            //Si l'utilisateur n'a pas d'age ou est null, on utilise 0 pour lui permettre d'aller aux évènements qui n'ont pas d'age minimum ou ceux qui ont 0 ans comme age minimum
            evenements = CreerListeBase(evenements, user?.Age ?? 0);

            evenements = evenements.Skip(start);

            evenements = evenements.Take(count);

            return CreerListeViewModel(evenements);
        }

        static public ListeEvenementViewModel CreerListeParticipation(ApplicationDbContext db, ApplicationUser user)
        {
            IQueryable<Evenement> evenements;

            evenements = db.Participations.Include(p => p.Evenement)
                            .Include(p => p.Evenement.Endroit.Ville)
                            .Where(p => p.Participant_id == user.Id)
                            .Where(p => p.Role != Role.Signalement)
                            .Select(p => p.Evenement);

            evenements = CreerListeBase(evenements, user.Age ?? 0);

            return CreerListeViewModel(evenements);
        }

        static public ListeEvenementViewModel CreerListeOrganisateur(ApplicationDbContext db, ApplicationUser user)
        {
            IQueryable<Participation> participations;
            IQueryable<Evenement> evenements;
            List<Evenement> events = new List<Evenement>();
            
            // si je commente ce qui se trouve entre le deux groupes de '//' sa marche pas, car il include pas les adresses des événements
            participations = db.Participations.Where(p => p.Participant_id == user.Id)
                            .Where(p => (p.Role == Role.Createur || p.Role == Role.Organisateur));
            
            foreach (var item in participations)
            {
                events.Add(db.Evenements.Include(e => e.Endroit.Ville).Where(e => e.id == item.Evenement_id).First());
            }
            //
            evenements = db.Participations.Where(p => p.Participant_id == user.Id)
                            .Where(p => (p.Role == Role.Createur || p.Role == Role.Organisateur))
                            .Select(p => p.Evenement).Include(e=>e.Endroit.Ville);

            evenements = CreerListeBase(evenements, user.Age ?? 0);

            return CreerListeViewModel(evenements);
        }

        static public ListeEvenementViewModel CreerListeInvitation(ApplicationDbContext db, ApplicationUser user)
        {
            IQueryable<Participation> participations;
            IQueryable<Evenement> evenements;
            List<Evenement> events = new List<Evenement>();

            participations = db.Participations.Where(p => p.Participant_id == user.Id)
                            .Where(p => p.Role == Role.Inviter);

            foreach (var item in participations)
            {
                events.Add(db.Evenements.Include(e => e.Endroit.Ville).Where(e => e.id == item.Evenement_id).First());
            }
            
            evenements = db.Participations.Where(p => p.Participant_id == user.Id)
                            .Where(p => p.Role == Role.Inviter)
                            .Select(p => p.Evenement).Include(e => e.Endroit.Ville);

            evenements = CreerListeBaseInvitation(evenements, user.Age ?? 0);

            return CreerListeViewModel(evenements);
        }

        static private IQueryable<Evenement> CreerListeBase(IQueryable<Evenement> evs, int age)
        {
            return evs.Where(e => (e.AgeMinimum ?? 0) <= age)
                    .Where(e => e.Public)
                    .Where(e => !e.Annulé)
                    .Where(e => e.DateLimite > DateTime.Now);
        }

        static private IQueryable<Evenement> CreerListeBaseInvitation(IQueryable<Evenement> evs, int age)
        {
            return evs.Where(e => (e.AgeMinimum ?? 0) <= age)
                    .Where(e => !e.Annulé)
                    .Where(e => e.DateLimite > DateTime.Now);
        }

        private static ListeEvenementViewModel CreerListeViewModel(IQueryable<Evenement> evenements)
        {
            return new ListeEvenementViewModel(
                            evenements.AsEnumerable() //Effectue la requête pour permettre d'effectuer le Select en mémoire plûtot que sur le serveur de BD
                                        .Select(e => new BaseEvenementViewModel
                                        {
                                            AdresseComplete = e?.Endroit?.ToString(),
                                            BilletNecessaire = e.BilletNecessaire,
                                            PrixBillet = e.PrixBillet,
                                            CheminPhoto = e.CheminPhoto,
                                            Nom = e.Nom,
                                            Debut = e.Debut,
                                            EvenementID = e.id,
                                            HeureDebut = e.HeureDebut
                                        })
                                        .ToList());
        }
    }
}
