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
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository; // our users repository

        public UsersController(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        // DELETE api/users
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
                return BadRequest($"User with Id = {id} is not found");

            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }
    }
}