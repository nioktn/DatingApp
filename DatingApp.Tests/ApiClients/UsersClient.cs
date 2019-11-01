using DatingApp.Tests.Clients;
using DatingApp.Tests.Model;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Tests
{
    public class UsersClient : BaseClient
    {
        const string usersEndPoint = "/api/users";

        public IRestResponse RemoveUser(int userId, string token)
        {
            var endPoint = apiCallsClient.GetEndPoint($"{usersEndPoint}/{userId}");
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Authorization", $"Bearer {token}" }
            };
            var response = apiCallsClient.SendDeleteRequest(endPoint, headers);
            return response;
        }
    }
}
