using System.Net;

namespace TechLibrary.Exception
{
    public class ErrorOnvalidationException : TechLibraryException
    {
        private readonly List<string> _errorsMessages;
        public ErrorOnvalidationException(List<string> errorsMessages)
        {
            _errorsMessages = errorsMessages;

        }
        public override List<string> GetErrorMessages() => _errorsMessages;

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}
