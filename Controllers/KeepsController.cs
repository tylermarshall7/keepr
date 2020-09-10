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
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;
        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Keep>> Get()
        {
            try
            {
                return Ok(_ks.Get());
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            };
        }

        [Authorize]
        [HttpGet("user")]
        public ActionResult<IEnumerable<Keep>> GetMyKeeps()
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("You must be logged in to create a keep");
                }
                return Ok(_ks.Get(user.Value));

            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        
        [HttpGet("{id}")]
        public ActionResult<Keep> GetById(int id)
        {
            try
            {
                return Ok(_ks.GetById(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            };
        }



//////////////////////////////////////////////////////////////////

        [Authorize]
        [HttpPost]
        public ActionResult<Keep> Post([FromBody] Keep newKeep)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                newKeep.UserId = userId.Value;
                return Ok(_ks.Create(newKeep));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<Keep> Edit(int id, [FromBody] Keep updatedKeep)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("You must be logged in to edit a keep");
                }
                updatedKeep.UserId = user.Value;
                updatedKeep.Id = id;
                return Ok(_ks.Edit(updatedKeep));

            }
            catch (Exception err)
            {
                
                return BadRequest(err.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user == null)
                {
                    throw new Exception("You must be logged in to delete a keep");
                }
                return Ok(_ks.Delete(user.Value, id));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}