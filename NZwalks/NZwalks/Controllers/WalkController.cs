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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalkController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
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
            // validating incoming request 
            if(!(await validateAddWalk(_walkRequestModel)))
            {
                return BadRequest(ModelState);
            }
            
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
            // validating incomin request
            if(!(await validateUpdateWalk(_walkUpdateRequestModel)))
            {
                return BadRequest(ModelState);
            }

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

        #region private methods
        private async Task<bool> validateAddWalk(WalkRequestModel _walkRequestModel)
        {
            if(_walkRequestModel == null)
            {
                ModelState.AddModelError(nameof(_walkRequestModel), $"{nameof(_walkRequestModel)} is Invalid");
                return false;
            }
            if(string.IsNullOrWhiteSpace(_walkRequestModel.Name))
            {
                ModelState.AddModelError(nameof(_walkRequestModel.Name), $"{nameof(_walkRequestModel.Name)} is empty");
            }
            if (_walkRequestModel.Length <= 0)
            {
                ModelState.AddModelError(nameof(_walkRequestModel.Length), $"{nameof(_walkRequestModel.Length)} can't be less than or equal to zero");
            }
            var region = await regionRepository.GetRegionById( _walkRequestModel.RegionId );
            if(region == null)
            {
                ModelState.AddModelError(nameof(_walkRequestModel.RegionId), $"{nameof(_walkRequestModel.RegionId)} is Invalid");
            }
            var walkDifficulty = await walkDifficultyRepository.GetWalkDifficultyById(_walkRequestModel.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(_walkRequestModel.WalkDifficultyId), $"{nameof(_walkRequestModel.WalkDifficultyId)} is Invalid");
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> validateUpdateWalk(WalkUpdateRequestModel _WalkUpdateRequestModel)
        {
            if (_WalkUpdateRequestModel == null)
            {
                ModelState.AddModelError(nameof(_WalkUpdateRequestModel), $"{nameof(_WalkUpdateRequestModel)} is Invalid");
                return false;
            }
            if (string.IsNullOrWhiteSpace(_WalkUpdateRequestModel.Name))
            {
                ModelState.AddModelError(nameof(_WalkUpdateRequestModel.Name), $"{nameof(_WalkUpdateRequestModel.Name)} is empty");
            }
            if (_WalkUpdateRequestModel.Length <= 0)
            {
                ModelState.AddModelError(nameof(_WalkUpdateRequestModel.Length), $"{nameof(_WalkUpdateRequestModel.Length)} can't be less than or equal to zero");
            }
            var region =await regionRepository.GetRegionById(_WalkUpdateRequestModel.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(_WalkUpdateRequestModel.RegionId), $"{nameof(_WalkUpdateRequestModel.RegionId)} is Invalid");
            }
            var walkDifficulty = await walkDifficultyRepository.GetWalkDifficultyById(_WalkUpdateRequestModel.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(_WalkUpdateRequestModel.WalkDifficultyId), $"{nameof(_WalkUpdateRequestModel.WalkDifficultyId)} is Invalid");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
