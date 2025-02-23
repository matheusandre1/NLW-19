using System.Net;

namespace TechLibrary.Exception
{
    public class ConflitcException : TechLibraryException
    {
        public ConflitcException(string message) : base(message) { }

        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Conflict;
    }
}
