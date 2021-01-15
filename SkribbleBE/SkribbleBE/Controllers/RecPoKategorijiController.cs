using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using DataLayer.Services;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkribbleBE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecPoKategorijiController : ControllerBase
    {
        private readonly RecPoKategorijiService recPoKategorijiService;

        public RecPoKategorijiController(ProjekatContext projekatContext, RecPoKategorijiService recPoKategorijiService)
        {
            this.recPoKategorijiService = recPoKategorijiService;
        }

        //[HttpPost]
        //[Route("createRecPoKategoriji")]
        //public async Task<IActionResult> CreateRecPoKategoriji([FromBody] RecPoKategorijiDTO r)
        //{
        //    recPoKategorijiService.AddNewRecPoKategoriji(r);
        //    return Ok();
        //}

        [HttpGet]
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
        }
        [HttpPut]
        [Route("updateRecPoKategoriji")]
        public async Task<IActionResult> UpdateRecPoKategoriji([FromBody] RecPoKategorijiDTO r)
        {
            try
            {
                this.recPoKategorijiService.UpdateRecPoKategoriji(r);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete]
        [Route("deleteRecPoKategoriji")]
        public async Task<IActionResult> DeleteRecPoKategoriji([FromBody] RecPoKategorijiDTO r)
        {
            try
            {
                this.recPoKategorijiService.DeleteRecPoKategoriji(r);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("getOneRecPoKategoriji/{idRecPoKategoriji}")]
        public async Task<IActionResult> GetOneRecPoKategoriji([FromRoute(Name = "idRecPoKategoriji")] int idRecPoKategoriji)
        {
            try
            {
                return new JsonResult(this.recPoKategorijiService.getOneRecPoKategoriji(idRecPoKategoriji));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPost]
        [Route("createByIds/{idKat}/{idRec}")]
        public async Task<IActionResult> CreateByIds([FromRoute(Name = "idKat")] int idKat, [FromRoute(Name = "idRec")] int idRec)
        {
            try
            {
                this.recPoKategorijiService.CreateByIds(idKat, idRec);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("getAllWordsByCategoryId/{katId}")]
        public async Task<IActionResult> GetAllWordsByCategoryId([FromRoute(Name = "katId")] int idKat)
        {
            try
            {
                return new JsonResult(this.recPoKategorijiService.GetAllWordsByCategoryId(idKat));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
