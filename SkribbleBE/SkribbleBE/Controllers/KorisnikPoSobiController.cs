using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using DataLayer.Services;
using DataLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkribbleBE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KorisnikPoSobiController : ControllerBase
    {
        private readonly KorisniciPoSobiService korisniciPoSobiService;

        public KorisnikPoSobiController(ProjekatContext projekatContext, KorisniciPoSobiService korisniciPoSobiService)
        {
            this.korisniciPoSobiService = korisniciPoSobiService;
        }


       /* [HttpGet]
        [Route("getAllRecPoKategoriji")]
        public async Task<IActionResult> GetAllRecPoKategoriji()
        {
            try
            {
                return new JsonResult(this.recPoKategorijiService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }*/
        [HttpPut]
        [Route("updateKorisnikPoSobi")]
        public async Task<IActionResult> UpdateRecPoKategoriji([FromBody] KorisnikPoSobiDTO k)
        {
            try
            {
                this.korisniciPoSobiService.UpdateKorisniciPoSobi(k);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete]
        [Route("deleteKorisnikPoSobi")]
        public async Task<IActionResult> DeleteKorisnikPoSobi([FromBody] KorisnikPoSobiDTO k)
        {
            try
            {
                this.korisniciPoSobiService.DeleteKorisniciPoSobi(k);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("getOneKorisnikPoSobi/{idKorisnikPoSobi}")]
        public async Task<IActionResult> GetOneKorisnikPoSobi([FromRoute(Name = "idKorisnikPoSobi")] int idKorisnikPoSobi)
        {
            try
            {
                return new JsonResult(this.korisniciPoSobiService.getOneKorisnikPoSobi(idKorisnikPoSobi));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPost]
        [Route("createByIds/{idKorisnik}/{idSoba}")]
        public async Task<IActionResult> CreateByIds([FromRoute(Name = "idKorisnik")] int idKorisnik, [FromRoute(Name = "idSoba")] int idSoba)
        {
            try
            {
                this.korisniciPoSobiService.CreateByIds(idKorisnik, idSoba);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("getAllUsersByRoomId/{sobaId}")]
        public async Task<IActionResult> GetAllUsersByRoomId([FromRoute(Name = "sobaId")] int sobaId)
        {
            try
            {
                return new JsonResult(this.korisniciPoSobiService.GetAllUsersByRoomId(sobaId));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("getLeaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            try
            {
                return new JsonResult(this.korisniciPoSobiService.GetTopTenUsers());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
