using AutoMapper;
using DatingApp.API.BLL;
using DatingApp.API.Controllers;
using DatingApp.API.Controllers.Resources;
using DatingApp.API.Core;
using DatingApp.API.Models;
using DatingApp.API.Persistence;
using DatingApp.Tests;
using DatingApp.Tests.Clients;
using DatingApp.Tests.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace Tests
{
    public class AcceptanceTests
    {
        int userId, noteId;
        string authToken;
        UserModel userModel = new UserModel
        {
            username = "username",
            password = "password"
        };
        NoteModel noteModel = new NoteModel
        {
            title = "Sample_title",
            text = "test text test text 123 test"
        };
        AuthenticationClient authClient = new AuthenticationClient();
        NotesClient notesClient = new NotesClient();
        UsersClient usersClient = new UsersClient();

        [Test]
        public void TranslateNote()
        {
            userId = RegisterUser(userModel); // register new user and get user id

            authToken = LoginUser(userModel); // login with new credentials and get token

            noteModel.userId = userId;
            noteId = CreateNote(noteModel); // create new note for created user using auth via token and get note id

            var translatedNote = TranslateNote(noteId); // translate created note

            Assert.AreEqual(noteModel.title + "_translated", translatedNote.title); // assert translated note title
            StringAssert.AreEqualIgnoringCase(noteModel.text, translatedNote.text); // assert translated note text
        }

        private int RegisterUser(UserModel user)
        {

            var regResponse = authClient.RegisterUser(user);
            Assert.AreEqual(HttpStatusCode.Created, regResponse.StatusCode);
            JsonHelper.TryParseJson<dynamic>(regResponse.Content, out dynamic result);
            return result.id;
        }

        private string LoginUser(UserModel user)
        {
            var logResponse = authClient.LoginUser(userModel);
            Assert.AreEqual(HttpStatusCode.OK, logResponse.StatusCode);
            JsonHelper.TryParseJson<dynamic>(logResponse.Content, out dynamic tokenResult);
            var token = tokenResult.token.ToString();
            Assert.IsNotEmpty(token);
            return token;
        }

        private int CreateNote(NoteModel note)
        {
            var createNoteResponse = notesClient.CreateNote(note, authToken);
            Assert.AreEqual(HttpStatusCode.Created, createNoteResponse.StatusCode);
            JsonHelper.TryParseJson<dynamic>(createNoteResponse.Content, out dynamic result);
            return result.id;
        }

        private NoteModel TranslateNote(int noteId)
        {
            var response = notesClient.TranslateNote(noteId, authToken);
            JsonHelper.TryParseJson<NoteModel>(response.Content, out var translatedNoteModel);
            return translatedNoteModel;
        }

        [TearDown]
        public void CleanUp()
        {
            notesClient.RemoveNote(noteId, authToken); // remove created note
            usersClient.RemoveUser(userId, authToken); // remove created user
        }
    }
}