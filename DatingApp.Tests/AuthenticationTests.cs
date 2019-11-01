using DatingApp.Tests;
using DatingApp.Tests.Clients;
using DatingApp.Tests.Model;
using NUnit.Framework;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Tests
{
    public class AuthenticationTests
    {
        int userId;
        string authToken;
        UserModel userModel = new UserModel
        {
            username = "username",
            password = "password"
        };

        [Test]
        public void RegisterNewUser()
        {
            var authClient = new AuthenticationClient();
            var response = authClient.RegisterUser(userModel);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            JsonHelper.TryParseJson<dynamic>(response.Content, out dynamic result);
            userId = result.id;
        }

        [Test]
        public void LoginNewUser()
        {
            var authClient = new AuthenticationClient();
            var regResponse = authClient.RegisterUser(userModel);
            Assert.AreEqual(HttpStatusCode.Created, regResponse.StatusCode);
            JsonHelper.TryParseJson<dynamic>(regResponse.Content, out dynamic result);
            userId = result.id;

            var logResponse = authClient.LoginUser(userModel);
            Assert.AreEqual(HttpStatusCode.OK, logResponse.StatusCode);
            JsonHelper.TryParseJson<dynamic>(logResponse.Content, out dynamic tokenResult);
            authToken = tokenResult.token.ToString();
            Assert.IsNotEmpty(authToken);
        }

        [TearDown]
        public void CleanUp()
        {
            var usersClient = new UsersClient();
            usersClient.RemoveUser(userId, authToken);
        }
    }
}