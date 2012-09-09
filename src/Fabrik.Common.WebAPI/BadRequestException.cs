using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fabrik.Common.WebAPI
{
    public class BadRequestException : HttpResponseException
    {
        public BadRequestException()
            : base(new HttpResponseMessage(HttpStatusCode.BadRequest))
        {

        }
    }
}
