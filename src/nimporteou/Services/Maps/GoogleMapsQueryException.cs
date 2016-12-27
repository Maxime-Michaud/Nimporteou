namespace Dispatch_GUTAC.Gestionnaires
{
    public class GoogleMapsQueryException : MapsQueryException
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }

        public GoogleMapsQueryException(string status, string errorMessage, bool reessayer)
        {
            Status = status;
            ErrorMessage = errorMessage;
            Reessayer = reessayer;
        }
    }
}