using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
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
    public class KategorijaController : ControllerBase
    {
        private readonly KategorijaService kategorijaService;

        public KategorijaController(ProjekatContext projekatContext, KategorijaService kategorijaService)
        {
            this.kategorijaService = kategorijaService;
        }

        [HttpPost]
        [Route("createKategorija")]
        public async Task<IActionResult> CreateKategorija([FromBody] Kategorija r)
        {
            kategorijaService.AddNewKategorija(r);
            return Ok();
        }

        [HttpGet]
        [Route("getAllKategorija")]
        public async Task<IActionResult> GetAllKategorija()
        {
            try
            {
                return new JsonResult(this.kategorijaService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPut]
        [Route("updateKategorija")]
        public async Task<IActionResult> UpdateKategorija([FromBody] Kategorija r)
        {
            try
            {
                this.kategorijaService.UpdateKategorija(r);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete]
        [Route("deleteKategorija")]
        public async Task<IActionResult> DeleteKategorija([FromBody] Kategorija r)
        {
            try
            {
                this.kategorijaService.DeleteKategorija(r);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("getOneKategorija/{idKategorija}")]
        public async Task<IActionResult> GetOneKategorija([FromRoute(Name = "idKategorija")] int idKategorija)
        {
            try
            {
                return new JsonResult(this.kategorijaService.getOneKategorija(idKategorija));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
