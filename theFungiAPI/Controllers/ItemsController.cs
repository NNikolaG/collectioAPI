﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using theFungiApplication;
using theFungiApplication.Commands;
using theFungiApplication.DataTransfer;
using theFungiApplication.Queries;
using theFungiApplication.UseCases;
using theFungiApplication.UseCases.Commands;
using theFungiApplication.UseCases.DataTransfer;
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
        public static IEnumerable<string> AllowedExtensions =>
            new List<string> { ".jpg", ".png", ".jpeg" };

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
        public IActionResult Post([FromForm] CreateCollectionItemWithImage dto, [FromServices] ICreateCollectionItemCommand command)
        {
            if (dto.ImageFile != null)
            {
                var guid = Guid.NewGuid().ToString();

                var extension = Path.GetExtension(dto.ImageFile.FileName);

                if (!AllowedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Unsupported file type.");
                }

                var fileName = guid + extension;

                var filePath = Path.Combine("wwwroot", "images", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                dto.ImageFile.CopyTo(stream);


                dto.Image = fileName;
            }
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);

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
