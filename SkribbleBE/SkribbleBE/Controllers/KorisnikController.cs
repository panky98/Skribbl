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

    [Route("[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly KorisnikService korisnikService;

        public KorisnikController(ProjekatContext projekatContext, KorisnikService korisnikService)
        {
            this.korisnikService = korisnikService;
        }

        [HttpPost]
        [Route("createKorisnik")]
        public async Task<IActionResult> CreateKorisnik([FromBody] KorisnikDTO k)
        {
            bool usernameTaken = korisnikService.findKorisnik(k.Username);
            if (usernameTaken == false)
            {
                korisnikService.AddNewKorisnik(k);
                return Ok();
            }
            return BadRequest("Korisnicko ime vec postoji!");
        }
        [Authorize]
        [HttpGet]
        [Route("getAllKorisnik")]
        public async Task<IActionResult> GetAllKorisnik()
        {
            try
            {
                return new JsonResult(this.korisnikService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [Authorize]
        [HttpPut]
        [Route("updateKorisnik")]
        public async Task<IActionResult> UpdateKorisnik([FromBody] KorisnikDTO k)
        {
            try
            {
                this.korisnikService.UpdateKorisnik(k);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        /*[Authorize]
        [HttpDelete]
        [Route("deleteKorisnik")]
        public async Task<IActionResult> DeleteKorisnik([FromBody] KorisnikDTO k)
        {
            try
            {
                this.korisnikService.DeleteKorisnik(k);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }*/
        [Authorize]
        [HttpGet]
        [Route("getOneKorisnik/{idKorisnik}")]
        public async Task<IActionResult> GetOneKorisnik([FromRoute(Name = "idKorisnik")] int idKorisnik)
        {
            try
            {
                return new JsonResult(this.korisnikService.getOneKorisnik(idKorisnik));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
