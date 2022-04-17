using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keeper1.Models;
using Keeper1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper1.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vkService;

        public VaultKeepsController(VaultKeepsService vkService)
        {
            _vkService = vkService;
        }

        [HttpPost]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultKeepData.CreatorId = userInfo.Id;
                VaultKeep vaultKeep = _vkService.Create(vaultKeepData);
                vaultKeep.Creator = userInfo;
                return Created($"api/vaultkeeps/{vaultKeep.Id}", vaultKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _vkService.Remove(id, userInfo);
                return Ok("Deleted vaultkeep");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}