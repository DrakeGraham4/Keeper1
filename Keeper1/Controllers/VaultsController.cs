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

        public VaultsController(VaultsService vService)
        {
            _vService = vService;
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
    }
}