using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using System.IO;

namespace Dispatch_GUTAC.Gestionnaires
{
    /// <summary>
    /// Effectue une requete sur google maps
    /// </summary>
    public class GoogleMapsAdapter : MapsAdapter<GoogleMapsQueryException>
    {
        private const string apiKey = "AIzaSyDitAp3l3CvqF23tFCUGGmQg-uOsb2ridI";
    
        #region Geocoding

        protected override Uri BuildGeocodingUri(string adr)
        {
            var uri = new UriBuilder();
            uri.Host = "maps.googleapis.com";
            uri.Path = "maps/api/geocode/xml";
            uri.Scheme = "https";
            uri.Query += $"address={WebUtility.UrlEncode(adr)}"
                       + $"&key={apiKey}";

            return uri.Uri;
        }

        protected override Tuple<double, double> ParseGeocodingStream(Stream responseStream)
        {
            var xdoc = XDocument.Load(responseStream);

            CheckStatus(xdoc);

            return (from element in xdoc.Descendants()
                    where element.Name == "location"
                    let lat = element.Descendants("lat").First().Value
                    let lng = element.Descendants("lng").First().Value
                    select Tuple.Create(double.Parse(lat), double.Parse(lng)))
                    .DefaultIfEmpty(null)
                    .FirstOrDefault();
        }

        private void CheckStatus(XDocument xdoc)
        {
            var status = xdoc.Descendants("status").Where(x => x.Name == "status").First().Value.ToString();

            switch (status)
            {
                case "OK":
                case "ZERO_RESULTS":
                    break;
                case "OVER_QUERY_LIMIT":
                case "INVALID_REQUEST":
                    throw new GoogleMapsQueryException(status, ExtractErrorMessage(xdoc), false);
                case "REQUEST_DENIED":
                case "UNKNOWN_ERROR":
                default:
                    throw new GoogleMapsQueryException(status, ExtractErrorMessage(xdoc), true);
            }
        }

        /// <summary>
        /// Extrait un message d'erreur de la réponse XML de google API
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        private string ExtractErrorMessage(XDocument xdoc)
        {
            return (from xe in xdoc.Descendants()
                    where xe.Name == "error_message"
                    select xe.Value)
                   .DefaultIfEmpty(null)
                   .Aggregate((s1, s2) => $"{s1}, {s2}");

        }

        #endregion
    }

}
