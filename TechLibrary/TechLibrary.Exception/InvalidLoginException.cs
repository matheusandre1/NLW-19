using System.Net;

namespace TechLibrary.Exception
{
    public class InvalidLoginException : TechLibraryException
    {
        public override List<string> GetErrorMessages() => ["Email e/ou Senha invalidos"]; 

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
        
    }
}
