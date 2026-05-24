using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace Authorization_Api;

[TestClass]
public class AuthenticationTests{
[TestMethod] //positive scenario
public void Get_Current_User_With_Valid_Bearer_Token()
{
    // Arrange
    RestClient client = new RestClient("https://dummyjson.com");

    // Login request
    RestRequest loginRequest =
        new RestRequest("/auth/login", Method.Post);

    loginRequest.AddHeader("Content-Type", "application/json");

    loginRequest.AddJsonBody(new
    {
        username = "emilys",
        password = "emilyspass",
        expiresInMins = 30
    });

    RestResponse loginResponse =
        client.Execute(loginRequest);

    JObject loginJson =
        JObject.Parse(loginResponse.Content!);

    string accessToken =
        loginJson["accessToken"]!.ToString();

    // Protected endpoint request
    RestRequest userRequest =
        new RestRequest("/auth/me", Method.Get);

    userRequest.AddHeader(
        "Authorization",
        $"Bearer {accessToken}");

    // Act
    RestResponse userResponse =
        client.Execute(userRequest);

    // Assert
    Assert.AreEqual(
        HttpStatusCode.OK,
        userResponse.StatusCode);

    JObject userJson =
        JObject.Parse(userResponse.Content!);

    Assert.AreEqual(
        "emilys",
        userJson["username"]?.ToString());
}

[TestMethod] //negative scenario
public void Get_Current_User_With_Invalid_Bearer_Token()
{
    // Arrange - GIVEN (condition)
    RestClient client =
        new RestClient("https://dummyjson.com");

    RestRequest request =
        new RestRequest("/auth/me", Method.Get);

    request.AddHeader(
        "Authorization",
        "Bearer InvalidToken123");

    // Act - WHEN (action)
    RestResponse response =
        client.Execute(request);

    // Assert - THEN(validation)
    Assert.AreEqual(
        HttpStatusCode.Unauthorized,
        response.StatusCode);
}
}
