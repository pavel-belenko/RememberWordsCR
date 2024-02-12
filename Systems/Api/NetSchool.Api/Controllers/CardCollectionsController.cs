﻿using Microsoft.AspNetCore.Mvc;
using NetSchool.Common.Exceptions;
using NetSchool.Services.CardCollections;
using NetSchool.Services.CardCollections.CardCollections;
using NetSchool.Services.Logger;

namespace NetSchool.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class CardCollectionsController : ControllerBase
    {
        private readonly IAppLogger _logger;
        private readonly ICartCollectionService _cartCollectionService;

        public CardCollectionsController(IAppLogger logger, ICartCollectionService cardCollectionService)
        {
            _logger = logger;
            _cartCollectionService = cardCollectionService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<CardCollectionModel>> GetAll()
        {
            var result = await _cartCollectionService.GetAll();

            return result;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var result = await _cartCollectionService.Get(id);
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<CardCollectionModel> Create([FromBody] CreateModel request)
        {
            var result = await _cartCollectionService.Create(request);

            return result;
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateModel request)
        {
            try
            {
                await _cartCollectionService.Update(id, request);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _cartCollectionService.Delete(id);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
