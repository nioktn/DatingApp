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

namespace DatingApp.API.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INoteRepository _repository;
        private readonly IAuthRepository _authRepository;

        public NotesController(IMapper mapper, IUnitOfWork unitOfWork, INoteRepository repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        // GET api/notes/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            var note = await _repository.GetNote(id);
            if (note == null)
                return NotFound($"Note with Id = {id} is not found");

            var noteResource = _mapper.Map<Note, NoteResource>(note);
            return Ok(noteResource);
        }
        // GET ??
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllNotesOfUser(int userId)
        {
            var notes = await _repository.GetAllNotesOfUser(userId);
            if (notes == null)
                return NotFound($"No {_authRepository.GetUserById(userId).Result.Username} notes were found.");

            ICollection<NoteResource> noteResources = null;
            foreach (var note in notes)
            {
                noteResources.Add(_mapper.Map<Note, NoteResource>(note));
            }
            // If you are able to rewrite it using LINQ, please do it.
            
            return Ok(noteResources); // Can we work with the collections?
        }

        // POST api/notes
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] SaveNoteResource noteResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var note = _mapper.Map<SaveNoteResource, Note>(noteResource);
            note.CreatedDate = DateTime.Now;

            await _repository.Add(note);
            await _unitOfWork.CompleteAsync();

            note = await _repository.GetNote(note.Id);

            var result = _mapper.Map<Note, NoteResource>(note);
            return CreatedAtRoute(RouteData, result);
        }

        // PUT api/notes
        [HttpPut]
        public async Task<IActionResult> UpdateNote(int id, SaveNoteResource noteResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var note = await _repository.GetNote(id);
            if (note == null)
                return BadRequest($"Note with Id = {id} is not found");
            
            _mapper.Map<SaveNoteResource, Note>(noteResource, note);
            await _unitOfWork.CompleteAsync();
            
            note = await _repository.GetNote(note.Id);
            var result = _mapper.Map<Note, NoteResource>(note);
            return Ok(result);
        }

        // DELETE api/notes
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _repository.GetNote(id);
            if (note == null)
                return BadRequest($"Note with Id = {id} is not found");

            _repository.Remove(note);
            return Ok(id);
        }
    }
}