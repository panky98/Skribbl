using System;
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
    public class TokIgreController : ControllerBase
    {
        private readonly TokIgreService tokIgreService;

        public TokIgreController(ProjekatContext projekatContext, TokIgreService tokIgreService)
        {
            this.tokIgreService = tokIgreService;
        }

        [HttpPost]
        [Route("createTokIgre")]
        public async Task<IActionResult> CreateTokIgre([FromBody] TokIgre tokIgre)
        {
            try
            {
                tokIgreService.AddNewTokIgre(tokIgre);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
            
        }


        [HttpGet]
        [Route("getAllTokIgre")]
        public async Task<IActionResult> GetAllTokIgre()
        {
            try
            {
                //return new JsonResult(this.tokIgreService.GetAll());

                IList<TokIgre> tokoviIgre = this.tokIgreService.GetAllWithIncludes(t => t.RecZaPogadjanje, t => t.Potezi, t => t.TokIgrePoKorisniku);
                return new JsonResult(tokoviIgre);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("getOneTokIgre/{idTokaIgre}")]
        public IActionResult GetOnePotez([FromRoute(Name = "idTokaIgre")] int idTokaIgre)
        {
            try
            {
                //return new JsonResult(this.tokIgreService.GetOneTokIgre(idTokaIgre));
                TokIgre tokIgre = this.tokIgreService.GetOneWithIncludes(idTokaIgre, t => t.RecZaPogadjanje);
                return new JsonResult(tokIgre);


            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPut]
        [Route("updateTokIgre")]
        public async Task<IActionResult> UpdateTokIgre([FromBody] TokIgre tokIgre)
        {
            try
            {
                this.tokIgreService.UpdateTokIgre(tokIgre);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpDelete]
        [Route("deleteTokIgre")]
        public async Task<IActionResult> DeleteTokIgre([FromBody] TokIgre tokIgre)
        {
            try
            {
                this.tokIgreService.DeleteTokIgre(tokIgre);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
