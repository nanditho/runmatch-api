using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.Dtos;
using API.Extenstions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;

        public ClubController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        [HttpGet("api/clubs")]
        public async Task<IActionResult> GetClubs()
        {
            return Ok( await _clubRepository.GetClubsAsync());
        }

        [HttpGet("api/clubs/{clubId}")]
        public async Task<IActionResult> GetClub([FromRoute] int clubId)
        {
            var club = await _clubRepository.GetClubByIdAsync(clubId);

            if (club == null)
                return NotFound();

            return Ok(club);
        }

        [HttpPost("api/clubs")]
        public async Task<IActionResult> Create([FromBody] CreateClubDto clubRequest)
        {
            var club = new Club 
            { 
                Name = clubRequest.Name,
                City = clubRequest.City,
                State = clubRequest.State,
                Intro = clubRequest.Intro

            };

            await _clubRepository.CreateClubAsync(club);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/api/club/" + club.Name;

            var response = new Club { Name = club.Name };
            return Created(locationUri, response);
        }

        [HttpPut("api/clubs/{clubId}")]
        public async Task<IActionResult> Update([FromRoute]int clubId, [FromBody] Club request)
        {
            var club = new Club
            {
                Id = clubId,
                Name = request.Name
            };

            var updated = await _clubRepository.UpdateClubAsync(club);

            if(updated)
                return Ok();
            
            return NotFound();
        }

        [HttpDelete("api/clubs/{clubId}")]
        public async Task<IActionResult> Delete([FromRoute]int clubId)
        {
            var deleted = await _clubRepository.DeleteClubAsync(clubId);

            if(deleted)
                return NoContent();
            
            return NotFound();
        }
    }
}