using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Controllers.Resources;
using DatingApp.API.Core;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.BLL;

namespace DatingApp.API.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INoteRepository _notesRepository;
        private readonly IAuthRepository _authRepository;
        private readonly INotesManager _notesManager;

        public NotesController(IMapper mapper, IUnitOfWork unitOfWork, INoteRepository notesRepository, INotesManager notesManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _notesRepository = notesRepository;
            _notesManager = notesManager;
        }

        // GET api/notes/1
        //[Route("translate")]
        [HttpGet("translate/{id}")]
        public async Task<IActionResult> TranslateNote(int id)
        {
            var note = await _notesRepository.GetNote(id);
            if (note == null)
                return NotFound($"Note with Id = {id} is not found");

            var translatedNote = _notesManager.TranslateNote(note);

            var noteResource = _mapper.Map<Note, NoteResource>(translatedNote);
            return Ok(noteResource);
        }


        // GET api/notes/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            var note = await _notesRepository.GetNote(id);
            if (note == null)
                return NotFound($"Note with Id = {id} is not found");

            var noteResource = _mapper.Map<Note, NoteResource>(note);
            return Ok(noteResource);
        }
        // GET ??
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllNotesOfUser(int userId)
        {
            
            
            // If you are able to rewrite it using LINQ, please do it.
            
            return Ok(); // Can we work with the collections?
        }

        // POST api/notes
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] SaveNoteResource noteResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var note = _mapper.Map<SaveNoteResource, Note>(noteResource);
            note.CreatedDate = DateTime.Now;

            await _notesRepository.Add(note);
            await _unitOfWork.CompleteAsync();

            note = await _notesRepository.GetNote(note.Id);

            var result = _mapper.Map<Note, NoteResource>(note);
            return CreatedAtRoute(RouteData, result);
        }

        // PUT api/notes
        [HttpPut]
        public async Task<IActionResult> UpdateNote(int id, SaveNoteResource noteResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var note = await _notesRepository.GetNote(id);
            if (note == null)
                return BadRequest($"Note with Id = {id} is not found");
            
            _mapper.Map<SaveNoteResource, Note>(noteResource, note);
            _notesRepository.Update(note);
            await _unitOfWork.CompleteAsync();
            
            note = await _notesRepository.GetNote(note.Id);
            var result = _mapper.Map<Note, NoteResource>(note);
            return Ok(result);
        }

        // DELETE api/notes
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _notesRepository.GetNote(id);
            if (note == null)
                return BadRequest($"Note with Id = {id} is not found");

            _notesRepository.Remove(note);
            return Ok(id);
        }
    }
}