using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using nimporteou.Models;

namespace Dispatch_GUTAC.Gestionnaires
{
    /// <summary>
    /// Effectue une requete à un service de geolocalisation et l'utilise pour afficher une carte et calculer des distances
    /// </summary>
    public abstract class MapsAdapter<T> where T : MapsQueryException
    {

        #region Interface public
        /// <summary>
        /// Obtiens la latitude at la longitude d'une adresse
        /// </summary>
        /// <param name="adr"></param>
        /// <returns></returns>
        public Task<Tuple<double, double>> GeoCode(string adr)
        {
            return GeoCode(adr);
        }

        private Task<HttpResponseMessage> MakeGeocodingRequest(string adr)
        {
            var client = new HttpClient();

            return client.GetAsync(BuildGeocodingUri(adr));
        }

        #endregion


        #region Membres abstraits et virtuels

        protected virtual int EssaisMaximum { get { return 3; } }

        protected abstract Uri BuildGeocodingUri(string adr);

        protected abstract Tuple<double, double> ParseGeocodingStream(Stream responseStream);

        #endregion

        public IOrderedEnumerable<Adresse> ChoisirOrdreDestinations(Adresse depart, Adresse destination, params Adresse[] adressesIntermediaires)
        {
            throw new NotImplementedException();
        }
    }
}
