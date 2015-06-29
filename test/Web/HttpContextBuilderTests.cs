using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using NUnit.Framework;

namespace FakeN.Web.Test
{
   
    [TestFixture]
    public class HttpContextBuilderTests
    {
        private FakeHttpContextBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new FakeHttpContextBuilder();
            FakeHttpContextBuilder.BodySerializer = o => "Hello";
        }

        [Test]
        public void Can_create_a_basic_context()
        {
            var httpContext = builder
                .Post("http://localhost/?A=1")
                .RespondWith(new {Status = "Ok"})
                .Build();


            Assert.That(httpContext.Request.Url.ToString(), Is.EqualTo("http://localhost/?A=1"));

            var reader = new StreamReader(httpContext.Response.OutputStream);
            var text = reader.ReadToEnd();
            Assert.That(text, Is.EqualTo("Hello"));
        }

        [Test]
        public void assigns_session_correctly()
        {
            var httpContext = builder
                .UsingSession(new {UserId = 10})
                .Build();


            Assert.That(httpContext.Session["UserId"], Is.EqualTo(10));
        }

        [Test]
        public void assigns_principal_correctly()
        {
            var httpContext = builder
                .UsePrincipal(new GenericPrincipal(new GenericIdentity("Arne"), new []{"Admin"}))
                .Build();


            Assert.That(httpContext.User.Identity.Name, Is.EqualTo("Arne"));
        }
    }
}
