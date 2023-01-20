using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;
using NZwalks.Repository;

namespace NZwalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllWalks")]
        public async Task<IActionResult> GetAllWalks()
        {
            // fetch data using domian models 
            var walks = await _walkRepository.GetAllAsync();

            // converting domain models to DTO's
            var walkDTOlist = _mapper.Map<List<WalkDTO>>(walks);

            // returning DTO 

            return Ok(walkDTOlist);
        }

        [HttpGet]
        [Route("GetWalkById{id:guid}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            //fetch data using domain model
            var walk = await _walkRepository.GetWalkById(id);

            //Convert domain model to DTO
            var walkDTO = _mapper.Map<WalkDTO>(walk);

            //return DTO to application layer
            return Ok(walkDTO);
        }

        [HttpPost]
        [Route("AddWalk")]
        public async Task<IActionResult> AddWalk([FromBody] WalkRequestModel _walkRequestModel)
        {
            //Convert dto to domain model
            var walk = new Walk()
            {
                Id = new Guid(),
                Name = _walkRequestModel.Name,
                Length = _walkRequestModel.Length,
                RegionId = _walkRequestModel.RegionId,
                WalkDifficultyId = _walkRequestModel.WalkDifficultyId
            };

            // send domain model to service layer

            return Ok(await _walkRepository.AddWalk(walk));
        }

        [HttpPut]
        [Route("UpdateWalk")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id,[FromBody] WalkUpdateRequestModel _walkUpdateRequestModel)
        {
            // convert dto to domain object
            var walk = new Walk()
            {
                Length = _walkUpdateRequestModel.Length,
                Name = _walkUpdateRequestModel.Name,
                RegionId = _walkUpdateRequestModel.RegionId,
                WalkDifficultyId = _walkUpdateRequestModel.WalkDifficultyId
            };

            // send omain to service layer
            return Ok(await _walkRepository.UpdateWalkAsync(id, walk));
        }

        [HttpDelete]
        [Route("DeleteWalk")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            return Ok(await _walkRepository.DeleteWalk(id));
        }

    }
}
