using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vk;
        public VaultKeepsController(VaultKeepsService vk)
        {
            _vk = vk;
        }


        [HttpGet("{id}")]
        public ActionResult<IEnumerable<VaultKeepViewModel>> Get(int id)
        {
            try
            {
                var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("you must be logged in");
                }
                return Ok(_vk.GetById(user.Value, id));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpPost]
        public ActionResult<VaultKeep> Create([FromBody] VaultKeep newVaultKeep)
        {
            try
            {
                var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("you must be logged in");
                }
                newVaultKeep.UserId = user.Value;
                return Ok(_vk.Create(newVaultKeep));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

       

        [HttpDelete("{id}")]
        public ActionResult<VaultKeep> Delete(int id)
        {
            try
            {
                var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("you must be logged in");
                }
                return Ok(_vk.Delete(user.Value, id));
            }
            catch (System.Exception err)
            {

                return BadRequest(err.Message);
            }
        }

    }
}