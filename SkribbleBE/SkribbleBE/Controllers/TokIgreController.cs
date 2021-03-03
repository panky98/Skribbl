using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.DTOs;
using DataLayer.Models;
using DataLayer.Services;
using DataLayer.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkribbleBE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokIgreController : ControllerBase
    {
        private readonly TokIgreService tokIgreService;
        private readonly CentralCoordinator centralCoordinator;

        public TokIgreController(ProjekatContext projekatContext, TokIgreService tokIgreService,CentralCoordinator centralCoordinator)
        {
            this.tokIgreService = tokIgreService;
            this.centralCoordinator = centralCoordinator;
        }

        [HttpPost]
        [Route("createTokIgre")]
        public async Task<IActionResult> CreateTokIgre([FromBody] TokIgreDTO tokIgre)
        {
            try
            {
                int id=tokIgreService.AddNewTokIgre(tokIgre);
                return Ok(id);
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPost]
        [Route("startTokIgre/{groupName}")]
        public async Task StartTokIgre([FromRoute(Name ="groupName")] string groupName)
        {
            this.centralCoordinator.GroupName = groupName;
            await this.centralCoordinator.StartAsync(System.Threading.CancellationToken.None);
        }


        [HttpGet]
        [Route("getAllTokIgre")]
        public async Task<IActionResult> GetAllTokIgre()
        {
            try
            {
                //return new JsonResult(this.tokIgreService.GetAll());

                IList<TokIgreDTO> tokoviIgre = this.tokIgreService.GetAllWithIncludes(t => t.RecZaPogadjanje, t => t.Potezi, t => t.TokIgrePoKorisniku, t=>t.Soba);
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
                TokIgreDTO tokIgre = this.tokIgreService.GetOneWithIncludes(idTokaIgre, t => t.RecZaPogadjanje, t => t.Potezi, t => t.TokIgrePoKorisniku, t => t.Soba);
                return new JsonResult(tokIgre);


            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPut]
        [Route("updateTokIgre")]
        public async Task<IActionResult> UpdateTokIgre([FromBody] TokIgreDTO tokIgre)
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
        public async Task<IActionResult> DeleteTokIgre([FromBody] TokIgreDTO tokIgre)
        {
            try
            {
                this.tokIgreService.DeleteTokIgreAsync(tokIgre);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
