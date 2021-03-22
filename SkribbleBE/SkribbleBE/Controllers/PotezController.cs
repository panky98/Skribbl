using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.DTOs;
using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkribbleBE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PotezController : ControllerBase
    {
        private readonly PotezService potezService;

        public PotezController(ProjekatContext projekatContext, PotezService potezService)
        {
            this.potezService = potezService;
        }

        [HttpGet]
        [Route("getAllPotez")]
        public async Task<IActionResult> GetAllPotez()
        {
            try
            {
                //return  new JsonResult(potezService.GetAll());

                IList <PotezDTO> potezi= potezService.GetAllWithIncludes(p => p.Korisnik, p => p.TokIgre, p=>p.TokIgre.RecZaPogadjanje);

                return new JsonResult(potezi);
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("getAllPotezByTokIgre/{idTokIgre}")]
        public async Task<IActionResult> GetAllPotezByTokIgre([FromRoute(Name = "idTokIgre")] int id)
        {
            try
            {
                IList<PotezDTO> potezi = potezService.GetPotezByTokIgre(id);
                return new JsonResult(potezi);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("getOnePotez/{idPoteza}")]
        public IActionResult GetOnePotez([FromRoute(Name = "idPoteza")] int idPoteza)
        {
            try
            {
               // return new JsonResult(this.potezService.GetOnePotez(idPoteza));
                PotezDTO potez = this.potezService.GetOneWithIncludes(idPoteza, p => p.Korisnik, p => p.TokIgre, p => p.TokIgre.RecZaPogadjanje);

                return new JsonResult(potez);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("createPotez")]
        public async Task<IActionResult> CreatePotez([FromBody] PotezDTO potez)
        {
            try
            {
                potezService.AddNewPotez(potez);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
           
        }
        [HttpDelete]
        [Route("deletePotez")]
        public async Task<IActionResult> DeletePotez([FromBody] PotezDTO potez)
        {
            try
            {
                this.potezService.DeletePotez(potez);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("updatePotez")]
        public async Task<IActionResult> UpdatePotez([FromBody] PotezDTO potez)
        {
            try
            {
                this.potezService.UpdatePotez(potez);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
