using AutoMapper;
using DatingApp.API.BLL;
using DatingApp.API.Controllers;
using DatingApp.API.Controllers.Resources;
using DatingApp.API.Core;
using DatingApp.API.Models;
using DatingApp.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    public class NotesControllerTests
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INoteRepository _notesRepository; // our notes repository
        private readonly IAuthRepository _authRepository;
        private readonly INotesManager _notesManager; // our notes BLL


        public void Setup()
        { 
        }

        //[Test]
        public async Task UpdateNoteTest()
        {
            var mapper = new Mock<IMapper>().Object;
            var unitOfWork = new Mock<IUnitOfWork>().Object;
            var noteRepository = new Mock<INoteRepository>();
            var noteManager = new Mock<INotesManager>().Object;
            var authRepository = new Mock<IAuthRepository>().Object;
            var id = 2;

            noteRepository.Setup(x => x.GetNote(id)).Returns(default(Task<Note>));

            var notesController = new NotesController(mapper, unitOfWork, noteRepository.Object, noteManager);

            var result = await notesController.UpdateNote(2, new SaveNoteResource());
            //var badRequestResult = result.Result as BadRequestObjectResult;

            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
            //Assert.AreEqual(new BadRequestObjectResult($"Note with Id = {id} is not found"), result);
        }
    }
}