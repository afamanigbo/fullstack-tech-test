using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using NUnit.Framework;


namespace TestTeacherTabletService
{
    public class TestHttpResponse
    {
        private HttpClient client;
    
        private HttpResponseMessage response;

        [SetUp]
         public void SetUP()
         {
             client = new HttpClient();
             
             client.BaseAddress = new Uri("http://localhost:5000/");
             response = client.GetAsync("api/tabletusage").Result;
         }
  
         [Test]
         public void GetResponseIsSuccess()
         {
             Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
         }

  
         [Test]
        public void GetResponseIsJson()
         {
               client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
             Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
           }
    }
}
