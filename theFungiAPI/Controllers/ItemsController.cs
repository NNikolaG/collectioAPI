﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using theFungiApplication;
using theFungiApplication.Commands;
using theFungiApplication.DataTransfer;
using theFungiApplication.Queries;
using theFungiApplication.UseCases;
using theFungiApplication.UseCases.Commands;
using theFungiApplication.UseCases.DataTransfer.Searches;
using theFungiApplication.UseCases.Queries;
using theFungiDataAccess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace theFungiAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public ItemsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<ItemsController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchDto dto, [FromServices] IGetCollectionItemsQuery query)
        {
            var data = _executor.ExecuteQuery(query, dto);
            return Ok(data);
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetSingleCollectionItemQuery command)
        {
            var data = _executor.ExecuteQuery(command, id);
            return Ok(data);
        }

        [Authorize]
        // POST api/<ItemsController>
        [HttpPost]
        public void Post([FromBody] CollectionItemCreateDto dto, [FromServices] ICreateCollectionItemCommand command)
        {
            _executor.ExecuteCommand(command, dto);
        }

        [Authorize]
        // PUT api/<ItemsController>/5
        [HttpPut]
        public IActionResult Put( [FromBody] CollectionItemCreateDto dto, [FromServices] IUpdateCollectionItemCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        [Authorize]
        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCollectionItemCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
