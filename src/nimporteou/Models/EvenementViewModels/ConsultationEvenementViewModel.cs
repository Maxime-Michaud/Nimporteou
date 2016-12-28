using System.Collections.Generic;

namespace nimporteou.Models.EvenementViewModels
{
    public class ConsultationEvenementViewModel : EvenementViewModel
    {
        public bool peutAdministrer;
        public bool peutInscrire;
        public bool annuler;
        public ApplicationUser _user;
        public List<Signalement> signalements;
    }
}