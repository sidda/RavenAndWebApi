using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Raven.Client;
using System.Web.Http.Controllers;

namespace WebAPI.Controllers
{
    public class RavenDbApiAttribute : ActionFilterAttribute
    {
        readonly IDocumentStore documentStore;

        public RavenDbApiAttribute(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");
            this.documentStore = documentStore;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as BaseController;
            if (controller == null)
                return;

            controller.RavenSession = documentStore.OpenSession();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var controller = actionExecutedContext.ActionContext.ControllerContext.Controller as BaseController;
            if (controller == null)
                return;

            controller.RavenSession.Dispose();
        }
    }
}