using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Keeper1.Models;
using Keeper1.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper1.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly VaultsService _vService;

        public AccountController(AccountService accountService, VaultsService vService)
        {
            _accountService = accountService;
            _vService = vService;
        }


        [HttpGet("vaults")]


        public async Task<ActionResult<List<Vault>>> GetMyVaults()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_vService.GetMyVaults(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }



    }


}