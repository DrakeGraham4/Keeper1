using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keeper1.Models;
using Keeper1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _pService;
        private readonly VaultsService _vService;
        private readonly KeepsService _kService;


        public ProfilesController(ProfilesService pService, KeepsService kService, VaultsService vService)
        {
            _pService = pService;
            _vService = vService;
            _kService = kService;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> GetUsersProfile(string id)
        {
            try
            {
                Profile profile = _pService.GetUsersProfile(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<Keep>>> GetProfileKeeps(string id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Keep> keep = _kService.GetProfileKeeps(id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<List<Vault>>> GetProfileVaults(string id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Vault> found = _vService.GetProfileVaults(id, userInfo?.Id);
                return Ok(found);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}