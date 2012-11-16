using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Threading;
using Raven.Client;
using Raven.Client.Document;
namespace WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        //session management stuff
        public IDocumentSession RavenSession { get; set; }

       
    }
}
