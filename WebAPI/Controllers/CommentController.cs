using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
namespace WebAPI.Controllers
{
    public class CommentController : BaseController
    {
        public Comment PostComment(Comment c)
        {
            RavenSession.Store(c);
            RavenSession.SaveChanges();
            return null;
        }
        
    }
}
