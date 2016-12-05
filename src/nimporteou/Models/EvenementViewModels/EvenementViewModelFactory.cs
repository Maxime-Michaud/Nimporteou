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
            IEnumerable<BaseEvenementViewModel> evenements;

            //Si l'utilisateur n'a pas d'age, on utilise 1 pour lui permettre d'aller aux évènements qui n'ont pas d'age minimum ou ceux qui ont 0 ans comme age minimum
            int age = user?.Age ?? 0;

            evenements = db.Evenements.Include(e => e.Endroit.Ville)
                                      .Where(e => (e.AgeMinimum ?? 0) <= age)
                                      .Where(e => e.Public)
                                      .Where(e => !e.Annulé)
                                      .Where(e => e.DateLimite > DateTime.Now)
                                      .AsEnumerable() //Effectue la requête pour permettre d'effectuer le Select en mémoire plûtot que sur le serveur de BD
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
                                      .ToList();

            return new ListeEvenementViewModel(evenements);
        }
    }
}
