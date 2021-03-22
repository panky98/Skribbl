using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.DTOs;
using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkribbleBE.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TokIgrePoKorisnikuController : ControllerBase
    {
        private readonly TokIgrePoKorisnikuService tokIgrePoKorisnikuService;

        public TokIgrePoKorisnikuController(ProjekatContext projekatContext, TokIgrePoKorisnikuService tokIgrePoKorisnikuService)
        {
            this.tokIgrePoKorisnikuService = tokIgrePoKorisnikuService;
        }

        [HttpPost]
        [Route("createTokIgrePoKorisniku")]
        public async Task<IActionResult> CreateTokIgrePoKorisniku([FromBody] TokIgrePoKorisnikuDTO tokIgrePoKorisniku)
        {
            try
            {
                if(tokIgrePoKorisniku.Korisnik==-1)
                {
                    IEnumerable<Claim> claims = User.Claims;
                    Claim idClaim = Enumerable.ElementAt<Claim>(claims, 2);
                    tokIgrePoKorisniku.Korisnik=Convert.ToInt32(idClaim.Value);
                }
                tokIgrePoKorisnikuService.AddNewTokIgrePoKorisniku(tokIgrePoKorisniku);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("getAllTokIgrePoKorisniku")]
        public async Task<IActionResult> GetAllTokIgrePoKorisniku()
        {
            try
            {
                //return new JsonResult(this.tokIgreService.GetAll());

                IList<TokIgrePoKorisnikuDTO> tokoviIgre = this.tokIgrePoKorisnikuService.GetAllWithIncludes(t=>t.Korisnik, t=>t.TokIgre, t=>t.TokIgre.RecZaPogadjanje);
                return new JsonResult(tokoviIgre);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("getOneTokIgrePoKorisniku/{idTokIgrePoKorisniku}")]
        public IActionResult GetOneTokIgrePoKorisniku([FromRoute(Name = "idTokIgrePoKorisniku")] int idTokIgrePoKorisniku)
        {
            try
            {

                TokIgrePoKorisnikuDTO tokIgrePoKorisniku = this.tokIgrePoKorisnikuService.GetOneWithIncludes(idTokIgrePoKorisniku, t => t.Korisnik, t => t.TokIgre, t=>t.TokIgre.RecZaPogadjanje);
                return new JsonResult(tokIgrePoKorisniku);


            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("updateTokIgrePoKorisniku")]
        public async Task<IActionResult> UpdateTokIgrePoKorisniku([FromBody] TokIgrePoKorisnikuDTO tokIgrePoKorisniku)
        {
            try
            {
                this.tokIgrePoKorisnikuService.UpdateTokIgrePoKorisniku(tokIgrePoKorisniku);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete]
        [Route("deleteTokIgrePoKorisniku")]
        public async Task<IActionResult> DeleteTokIgrePoKorisniku([FromBody] TokIgrePoKorisnikuDTO tokIgrePoKorisniku)
        {
            try
            {
                this.tokIgrePoKorisnikuService.DeleteTokIgrePoKorisniku(tokIgrePoKorisniku);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("getTokIgrePoKorisnikuZaKorisnika/{idKorisnik}")]
        public IActionResult GetTokIgrePoKorisnikuZaKorisnika([FromRoute(Name = "idKorisnik")] int idKorisnik)
        {
            try
            {

                IList<TokIgrePoKorisnikuDTO> tokoviIgre = this.tokIgrePoKorisnikuService.GetTokIgreByKorisnik(idKorisnik);
                return new JsonResult(tokoviIgre);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
