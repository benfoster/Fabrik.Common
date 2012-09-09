using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fabrik.Common.WebAPI
{
    public class NotFoundException : HttpResponseException
    {
        public NotFoundException()
            : base(new HttpResponseMessage(HttpStatusCode.NotFound))
        {

        }
    }
}
