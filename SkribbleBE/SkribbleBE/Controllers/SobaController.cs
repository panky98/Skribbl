using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using DataLayer.Services;
using DataLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkribbleBE.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SobaController : ControllerBase
    {
        private readonly SobaService sobaService;

        public SobaController(ProjekatContext projekatContext, SobaService sobaService)
        {
            this.sobaService = sobaService;
        }

        [HttpPost]
        [Route("createSoba")]
        public async Task<IActionResult> CreateSoba([FromBody] SobaDTO s)
        {
                sobaService.AddNewSoba(s);
                return Ok();
        }

        [HttpGet]
        [Route("getAllSoba")]
        public async Task<IActionResult> GetAllSoba()
        {
            try
            {
                return new JsonResult(this.sobaService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPut]
        [Route("updateSoba")]
        public async Task<IActionResult> UpdateSoba([FromBody] SobaDTO s)
        {
            try
            {
                this.sobaService.UpdateSoba(s);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete]
        [Route("deleteSoba")]
        public async Task<IActionResult> DeleteSoba([FromBody] SobaDTO s)
        {
            try
            {
                this.sobaService.DeleteSoba(s);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("getOneSoba/{idSoba}")]
        public async Task<IActionResult> GetOneSoba([FromRoute(Name = "idSoba")] int idSoba)
        {
            try
            {
                return new JsonResult(this.sobaService.getOneSoba(idSoba));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
