using System.Collections.Generic;
using System.Web.Http;

namespace Fabrik.Common.WebAPI.AtomPubExample.Controllers
{
    public abstract class BlogControllerBase : ApiController
    {
        protected static readonly List<Post> posts = new List<Post>();
    }
}
