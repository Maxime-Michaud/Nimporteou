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
        static public ListeEvenementViewModel CreerListe(ApplicationDbContext db, ApplicationUser user)
        {
            IQueryable<Evenement> evenements;

            evenements = db.Evenements.Include(e => e.Endroit.Ville);
            evenements = CreerListeBase(evenements, user);


            return CreerListeViewModel(evenements);
        }

        static public ListeEvenementViewModel CreerListeParticipation(ApplicationDbContext db, ApplicationUser user)
        {
            IQueryable<Evenement> evenements;

            evenements = db.Participations.Include(p => p.Evenement.Endroit.Ville)
                            .Where(p => p.Participant_id == user.Id)
                            .Where(p => p.Role != Role.Signalement)
                            .Select(p => p.Evenement);

            evenements = CreerListeBase(evenements, user);

            return CreerListeViewModel(evenements);
        }

        static private IQueryable<Evenement> CreerListeBase(IQueryable<Evenement> evs, ApplicationUser user)
        {
            //Si l'utilisateur n'a pas d'age, on utilise 1 pour lui permettre d'aller aux évènements qui n'ont pas d'age minimum ou ceux qui ont 0 ans comme age minimum
            int age = user?.Age ?? 0;

            return evs.Where(e => (e.AgeMinimum ?? 0) <= age)
                        .Where(e => e.Public)
                        .Where(e => !e.Annulé)
                        .Where(e => e.DateLimite > DateTime.Now);
        }

        private static ListeEvenementViewModel CreerListeViewModel(IQueryable<Evenement> evenements)
        {
            return new ListeEvenementViewModel(
                            evenements.AsEnumerable() //Effectue la requête pour permettre d'effectuer le Select en mémoire plûtot que sur le serveur de BD
                                        .Select(e => new BaseEvenementViewModel
                                        {
                                            AdresseComplete = e.Endroit.ToString(),
                                            BilletNecessaire = e.BilletNecessaire,
                                            PrixBillet = e.PrixBillet,
                                            CheminPhoto = e.CheminPhoto,
                                            Nom = e.Nom,
                                            Debut = e.Debut,
                                            EvenementID = e.id
                                        })
                                        .ToList());
        }
    }
}
