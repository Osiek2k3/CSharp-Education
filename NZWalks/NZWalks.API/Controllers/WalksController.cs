using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFiltres;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository) {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost("Create")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomianModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomianModel);

            return Ok(mapper.Map<WalkDto>(walkDomianModel));
            
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery]string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool isAscending, [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000)
        {
            var walksDomianModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy,
            isAscending, pageNumber, pageSize);

           // throw new Exception("This is a new Exception");

            return Ok(mapper.Map<List<WalkDto>>(walksDomianModel));
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walksDomianModel = await walkRepository.GetByIdAsync(id);

            if (walksDomianModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walksDomianModel));
        }

        [HttpPut("UpdateWalk/{id}")]
        [ValidateModel]
        public async Task<IActionResult>Update(Guid id, [FromBody]UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomianModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomianModel = await walkRepository.UpdateAsync(id, walkDomianModel);

            if (walkDomianModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomianModel));
        }

        [HttpDelete("DeleteWalk/{id}")]
        public async Task<IActionResult>Delete(Guid id)
        {
            var walkDomianModel = await walkRepository.DeleteAsync(id);

            if (walkDomianModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomianModel));
        }
    }
}
