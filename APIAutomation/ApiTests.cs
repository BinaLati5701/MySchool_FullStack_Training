using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace APIAutomation;

public class Post
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; } = string.Empty;
    public string body { get; set; } = string.Empty;
}


[TestClass]
public class ApiTests
{
    [TestMethod]
    public void GetPostTest()
    {
        // create client
        var client = new RestClient("https://jsonplaceholder.typicode.com");

        // create request
        var request = new RestRequest("/posts/1", Method.Get);

        // send request
        var response = client.Execute(request);

        // print response
        Console.WriteLine(response.Content);

        // deserialize JSON
        var post = JsonConvert.DeserializeObject<Post>(response.Content!);

        // validations
        Assert.IsNotNull(post);
        Assert.AreEqual(1, post.id);
        StringAssert.Contains( post.title, "sunt");
    }
}