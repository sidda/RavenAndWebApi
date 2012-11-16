using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using WebAPI.Controllers;
using WebAPI.Models;

namespace WebAPI.Tests
{
    [TestFixture]
    public class Class1
    {
        public void ReadDocument()
        {
            Console.WriteLine("{0} is{0} bad guys", "");
        }

        private IDocumentStore _store;
        private IDocumentSession _session;

        [TestFixtureSetUp]
        public void Setup()
        {
            _store = GetEmbeddableDbForTesting();
            _session = _store.OpenSession();
        }
        public static IDocumentStore GetEmbeddableDbForTesting()
        {
            using (var _store = new EmbeddableDocumentStore
                {
                    RunInMemory = true,
                    Conventions = new DocumentConvention { DefaultQueryingConsistency = ConsistencyOptions.QueryYourWrites }
                })
            {
                _store.Initialize();
                return _store;
            }
        }

        [Test]
        public void Add_Numbers_SumTheNumber()
        {
            Post p = new Post();
            p.Id = 1;
            p.PostedOn = DateTime.Now;
            p.Text = "siddaraju";

            PostController pc = new PostController();
            pc.RavenSession = _session;
            Post postedPost = pc.PostBPost(p);
            Assert.IsNull(postedPost, "postedPost is NULL");
        }

        private static Class1 GetClass1()
        {
            return new Class1();
        }
    }
}
