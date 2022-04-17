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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vService;
        private readonly KeepsService _kService;

        public VaultsController(VaultsService vService, KeepsService kService)
        {
            _vService = vService;
            _kService = kService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault found = _vService.GetById(id);
                if (found == null)
                {
                    throw new Exception("No Vault by that Id");
                }
                return found;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.CreatorId = userInfo.Id;
                Vault vault = _vService.Create(vaultData);
                vault.Creator = userInfo;
                return Created($"api/vaults{vault.Id}", vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.Id = id;
                vaultData.CreatorId = userInfo.Id;
                Vault vault = _vService.Update(vaultData);
                return Ok(vault);
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
                _vService.Remove(id, userInfo.Id);
                return Ok("Deleted Vault");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<VKViewModel>>>
        GetKeepsByVaultId(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<VKViewModel> vaultKeep = _kService.GetKeepsByVaultId(id);
                return Ok(vaultKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}