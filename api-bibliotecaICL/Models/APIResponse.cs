using System.Net;

namespace APICL.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessage { get; set; }
        public object Resultado { get; set; }

        public string Alertmsg { get; set; }

    }
}
