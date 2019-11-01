using DatingApp.Tests.Clients;
using DatingApp.Tests.Model;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Tests
{
    public class NotesClient : BaseClient
    {
        const string notesEndPoint = "/api/notes";
        const string translateEndPoint = "/api/notes/translate";

        public IRestResponse GetNote(int noteId)
        {
            var endPoint = apiCallsClient.GetEndPoint($"{notesEndPoint}/{noteId}");
            var response = apiCallsClient.SendGetRequest(endPoint);
            return response;
        }

        public IRestResponse CreateNote(NoteModel noteModel, string token)
        {
            var requestBody = new JsonDeserializer().Serialize(noteModel);
            var endPoint = apiCallsClient.GetEndPoint(notesEndPoint);
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Authorization", $"Bearer {token}" }
            };
            var response = apiCallsClient.SendPostRequest(endPoint, headers, body: requestBody);
            return response;
        }
        
        public IRestResponse TranslateNote(int noteId, string token)
        {
            var endPoint = apiCallsClient.GetEndPoint($"{translateEndPoint}/{noteId}");
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Authorization", $"Bearer {token}" }
            };
            var response = apiCallsClient.SendGetRequest(endPoint, headers);
            return response;
        }

        public IRestResponse RemoveNote(int noteId, string token)
        {
            var endPoint = apiCallsClient.GetEndPoint($"{notesEndPoint}/{noteId}");
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
