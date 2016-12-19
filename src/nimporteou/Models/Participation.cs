using System.ComponentModel.DataAnnotations.Schema;

namespace nimporteou.Models
{
    /// <summary>
    /// Classe d'association qui sert a faire le pont entre les utilisateurs, les évènements, ainsi que les billets, si
    /// nécéssaire pour l'évènement
    /// </summary>
    public class Participation
    {
        public int Participant_id { get; set; }

        public int Evenement_id { get; set; }
        /// <summary>
        /// La personne qui participe
        /// </summary>
        [ForeignKey("Participant_id")]
        public ApplicationUser Participant { get; set; }

        /// <summary>
        /// Le role de l'utilisateur dans l'evenement
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// L'évènement auquel le participant participe
        /// </summary>
        [ForeignKey("Evenement_id")]
        public Evenement Evenement { get; set; }

        /// <summary>
        /// Les informations du billet, si il y a lieu
        /// </summary>
        public Billet Billet { get; set; }

        /// <summary>
        /// Un signalement pour l'évènement. Si présent, le role devrais être mis a Signalement
        /// </summary>
        public Signalement Signalement { get; set; }

        /// <summary>
        /// Si un utilisateur s'inscrit avec des amis ou autres
        /// </summary>
        public int NombreParticipants { get; set; }
    }

    public enum Role
    {
        Signalement = -1,
        Invité = 0,
        Participant,
        Organisateur,
        Createur
    };
}