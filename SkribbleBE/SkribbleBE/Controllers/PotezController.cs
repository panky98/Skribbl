﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
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

                IList <Potez> potezi= potezService.GetAllWithIncludes(p => p.Korisnik, p => p.TokIgre, p=>p.TokIgre.RecZaPogadjanje);

                return new JsonResult(potezi);
            }
            catch(Exception e)
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
                Potez potez = this.potezService.GetOneWithIncludes(idPoteza, p => p.Korisnik, p => p.TokIgre, p => p.TokIgre.RecZaPogadjanje);

                return new JsonResult(potez);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("createPotez")]
        public async Task<IActionResult> CreatePotez([FromBody] Potez potez)
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
        public async Task<IActionResult> DeletePotez([FromBody] Potez potez)
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
        public async Task<IActionResult> UpdatePotez([FromBody] Potez potez)
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