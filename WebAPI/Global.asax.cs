﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Raven.Client;
using Raven;
using Raven.Client.Document;
using System.Reflection;
using Raven.Client.Indexes;
using WebAPI.Controllers;
namespace WebAPI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        public static DocumentStore Store;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //RavenDB stuff
            Store = new DocumentStore { ConnectionStringName = "RavenDB" };
            Store.Initialize();

            var config = GlobalConfiguration.Configuration;

            config.Filters.Add(new RavenDbApiAttribute(Store));

           // IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);
        }
    }
}