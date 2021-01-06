using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkribbleBE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecController : ControllerBase
    {
        private readonly RecService recService;

        public RecController(ProjekatContext projekatContext,RecService recService)
        {
            this.recService = recService;
        }

        [HttpPost]
        [Route("createRec")]
        public async Task<IActionResult> CreateRec([FromBody] Rec r)
        {
            recService.AddNewRec(r);
            return Ok();
        }

        [HttpGet]
        [Route("getAllRec")]
        public async Task<IActionResult> GetAllRec()
        {
            try
            {
                return new JsonResult(this.recService.GetAll());
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPut]
        [Route("updateRec")]
        public async Task<IActionResult> UpdateRec([FromBody]Rec r)
        {
            try
            {
                this.recService.UpdateRec(r);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete]
        [Route("deleteRec")]
        public async Task<IActionResult> DeleteRec([FromBody] Rec r)
        {
            try
            {
                this.recService.DeleteRec(r);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("getOneRec/{idRec}")]
        public async Task<IActionResult> GetOneRec([FromRoute(Name ="idRec")]int idRec)
        {
            try
            {
                return new JsonResult(this.recService.getOneRec(idRec));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

    }
}
