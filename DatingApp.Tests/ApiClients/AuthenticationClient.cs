using DatingApp.Tests.Model;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Tests.Clients
{
    public class AuthenticationClient : BaseClient
    {
        const string registerEndPoint = "/api/auth/Register";
        const string loginEndPoint = "/api/auth/Login";

        public IRestResponse RegisterUser(UserModel userModel)
        {
            var requestBody = new JsonDeserializer().Serialize(userModel);
            var endPoint = apiCallsClient.GetEndPoint(registerEndPoint);
            var response  = apiCallsClient.SendPostRequest(endPoint, body: requestBody);
            return response;
        }
        
        public IRestResponse LoginUser(UserModel userModel)
        {
            var requestBody = new JsonDeserializer().Serialize(userModel);
            var endPoint = apiCallsClient.GetEndPoint(loginEndPoint);
            var response  = apiCallsClient.SendPostRequest(endPoint, body: requestBody);
            return response;
        }
    }
}
