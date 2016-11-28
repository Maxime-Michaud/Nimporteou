using System;

namespace Dispatch_GUTAC.Gestionnaires
{
    abstract public class MapsQueryException : Exception
    {
        public bool Reessayer { get; set; }
    }
}