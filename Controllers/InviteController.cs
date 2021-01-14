using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Extenstions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class InviteController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IInviteRepository _inviteRepository;
        public InviteController(IUserRepository userRepository, IInviteRepository inviteRepository)
        {
            _inviteRepository = inviteRepository;
            _userRepository = userRepository;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var invitedUser = await _userRepository.GetUserByUsername(username);
            var sourceUser = await _inviteRepository.GetUserWithInvites(sourceUserId);

            if (invitedUser == null) return NotFound();

            if (sourceUser.UserName == username) return BadRequest("You cannot invite yourself");

            var userInvite = await _inviteRepository.GetUserInvite(sourceUserId, invitedUser.Id);
 
            if (userInvite != null) return BadRequest("You already invited this user");

            userInvite = new UserInvite
            {
                SourceUserId = sourceUserId,
                InvitedUserId = invitedUser.Id
            };

            sourceUser.InvitedUsers.Add(userInvite);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to invite user");
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InviteDto>>> GetUserInvites([FromQuery]InviteParams inviteParams)
        {
            inviteParams.UserId = User.GetUserId();
            var users = await _inviteRepository.GetUserInvites(inviteParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }
    }
}