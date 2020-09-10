using System;
using System.Collections.Generic;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }

///Getters
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Vault>> GetMyVaults()
        {
            try
            {
                var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("you must be logged in");
                }
                return Ok(_vs.Get(user.Value));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            };
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Vault> GetById(int id)
        {
            try
            {
                var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("you must be logged in");
                }
                return Ok(_vs.GetById(user.Value, id));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            };
        }

        [Authorize]
        [HttpGet("{id}/keeps")]
        public ActionResult<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                string getUser = user.Value;
                if (user == null)
                {
                    throw new Exception("you must be logged in");
                }
                return Ok(_vks.GetKeepsByVaultId(id, getUser));
            }
            catch (System.Exception err)
            {

                return BadRequest(err.Message);
            };
        }

///Create, Edit, Delete

        [Authorize]
        [HttpPost]
        public ActionResult<Vault> Post([FromBody] Vault newVault)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                newVault.UserId = userId.Value;
                if (userId == null)
                {
                    throw new Exception("you must be logged in");
                }
                return Ok(_vs.Create(newVault));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]

        public ActionResult<Vault> Edit([FromBody] Vault updatedVault, int id)
        {
            try
            {
                var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("you must be logged in");
                }
                updatedVault.UserId = user.Value;
                updatedVault.Id = id;
                return Ok(_vs.Edit(updatedVault, user.Value));
            }
            catch (System.Exception err)
            {

                return BadRequest(err.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<Vault> Delete(int id)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    throw new Exception("you must be logged in");
                }
                return Ok(_vs.Delete(userId.Value, id));
            }
            catch (System.Exception err)
            {

                return BadRequest(err.Message);
            }
        }



  }

}