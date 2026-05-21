using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace MyApiTests
{
    [TestClass]
    public class PostsApiTests
    {
        private readonly string baseUrl = "https://jsonplaceholder.typicode.com";
        #region Positive Test Cases
        [TestMethod]
        public void ValidateSuccessfulStatusCode()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/posts", Method.Get);
            var response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void ValidateResponseReturnsListOfItems()
        {
          var client = new RestClient(baseUrl);
            var request = new RestRequest("/posts", Method.Get);
            var response = client.Execute(request); 
            var posts = JArray.Parse(response.Content!);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void ValidateResponseReturns100Items()
        {
           var client = new RestClient(baseUrl);
            var request = new RestRequest("/posts", Method.Get);
            var response = client.Execute(request); 
            var posts = JArray.Parse(response.Content!); 
            Assert.AreEqual(100, posts.Count);
        }

        [TestMethod]
        public void ValidateEachItemContainsFourProperties()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/posts", Method.Get);
            var response = client.Execute(request); 
            var posts = JArray.Parse(response.Content!); 
            foreach(JObject post in posts)
            {
                Assert.IsTrue(post.ContainsKey("userId"));
                Assert.IsTrue(post.ContainsKey("id"));
                Assert.IsTrue(post.ContainsKey("title"));
                Assert.IsTrue(post.ContainsKey("body"));
            }
            
        }

        [TestMethod]
        public void ValidateUserIdPropertyIsInteger()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/posts", Method.Get);
            var response = client.Execute(request); 
            var posts = JArray.Parse(response.Content!); 
            foreach (JObject post in posts)
            {
                Assert.AreEqual(JTokenType.Integer, post["userId"].Type);
            }
        }

        [TestMethod]
        public void ValidateIdPropertyIsInteger()
        {
          var client = new RestClient(baseUrl);
            var request = new RestRequest("/posts", Method.Get);
            var response = client.Execute(request); 
            var posts = JArray.Parse(response.Content!); 
            foreach (JObject post in posts)
            {
                Assert.AreEqual(JTokenType.Integer, post["id"].Type);
            }  
        }

        [TestMethod]
        public void ValidateTitlePropertyIsString()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/posts", Method.Get);
            var response = client.Execute(request); 
            var posts = JArray.Parse(response.Content!); 
            foreach (JObject post in posts)
            {
                Assert.AreEqual(JTokenType.String, post["title"].Type);
            }  
        }

        [TestMethod]
        public void ValidateBodyPropertyIsString()
        {
             var client = new RestClient(baseUrl);
            var request = new RestRequest("/posts", Method.Get);
            var response = client.Execute(request); 
            var posts = JArray.Parse(response.Content!); 
            foreach (JObject post in posts)
            {
                Assert.AreEqual(JTokenType.String, post["body"].Type);
            }  
        }
        #endregion
        #region  Negative Test Cases
        [TestMethod]
        public void ValidateIncorrectHttpMethodReturns404StatusCode()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/posts", Method.Put);
            var response = client.Execute(request); 
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void ValidateIncorrectHttpMethodReturnsEmptyJsonObject()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/posts", Method.Put);
            var response = client.Execute(request); 
            var body = JObject.Parse(response.Content);
            Assert.AreEqual(0, body.Count);
        }

        [TestMethod]
        public void ValidateIncorrectEndpointReturns404StatusCode()
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("/post", Method.Get);
            var response = client.Execute(request); 
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        #endregion

        #region Legacy Test Cases

        [TestMethod]
        public void ValidateEndpointIsAccessibleThroughHttpProtocol()
        {
            var client = new RestClient("http://jsonplaceholder.typicode.com");
            var request = new RestRequest("/posts", Method.Get);
            var response = client.Execute(request); 
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        #endregion
    }
}