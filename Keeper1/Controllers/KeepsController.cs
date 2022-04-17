using System.Threading.Tasks;
using Keeper1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Keeper1.Models;
using CodeWorks.Auth0Provider;
using System;

namespace Keeper1.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _kService;

        public KeepsController(KeepsService kService)
        {
            _kService = kService;
        }

        [HttpGet]
        public ActionResult<Keep> GetAll()
        {
            try
            {

                return Ok(_kService.GetAll());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Keep>> GetById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Keep found = _kService.GetById(id, userInfo);
                if (found == null)
                {
                    throw new Exception("No Keep by that Id");
                }
                return Ok(found);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                keepData.CreatorId = userInfo.Id;
                Keep keep = _kService.Create(keepData);
                keep.Creator = userInfo;
                return Created($"api/keeps/{keep.Id}", keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                keepData.Id = id;
                keepData.CreatorId = userInfo.Id;
                Keep keep = _kService.Update(keepData, userInfo);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Keep found = _kService.GetById(id, userInfo);
                if (userInfo.Id != found.CreatorId)
                {
                    throw new Exception("You cannot remove this Keep");
                }
                _kService.Remove(id);
                return Ok("Keep Removed");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}