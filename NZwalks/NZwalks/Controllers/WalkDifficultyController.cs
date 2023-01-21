using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;
using NZwalks.Repository;

namespace NZwalks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            this._mapper = mapper;
        }


        [HttpGet]
        [Route("GetAllWalkDifficulties")]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            // fetch all using donmain models
            var walkDifficulties = await _walkDifficultyRepository.GetAllWalkDifficulties();

            // convert domain models to DTO's
            var walkDifficultiesDTOList = _mapper.Map<List<WalkDifficultyDTO>>(walkDifficulties);

            return Ok(walkDifficultiesDTOList);
        }

        [HttpGet]
        [Route("GetWalkDifficultyById{id:guid}")]
        public async Task<IActionResult> GetWalkDifficultYById(Guid id)
        {
            // fetch all using donmain models
            var walkDifficulty = await _walkDifficultyRepository.GetWalkDifficultyById(id);

            // convert domain models to DTO's
            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        [Route("AddWalkDifficulty")]
        public async Task<IActionResult> AddWalkDifficulty(WalkDifficultyRequestModel _walkDifficultyRequestModel)
        {
            // validating incomin request 
            if(!validateAddWalkDifficulty(_walkDifficultyRequestModel))
            {
                return BadRequest(ModelState);
            }
            // convert DTO to domain model
            var walkDifficulty = new WalkDifficulty()
            {
                Id = new Guid(),
                Code = _walkDifficultyRequestModel.Code
            };
            // send domain model to service layer

            return Ok(await _walkDifficultyRepository.AddWalkDifficulty(walkDifficulty));
        }

        [HttpPut]
        [Route("UpdateWalkDifficulty{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id, [FromBody] WalkDifficultyUpdateRequestModel _walkDifficultyUpdateRequestModel)
        {
            // validating incomin request 
            if (!validateUpdateWalkDifficulty(_walkDifficultyUpdateRequestModel))
            {
                return BadRequest(ModelState);
            }
            //Convert dto to domain
            var walkDifficulty = new WalkDifficulty()
            {
                Code = _walkDifficultyUpdateRequestModel.Code,
            };

            // send domain to service layer
            return Ok(await _walkDifficultyRepository.UpdateWalkDifficulty(id, walkDifficulty));
        }

        [HttpDelete]
        [Route("DeleteWalkDifficulty{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            return Ok(await _walkDifficultyRepository.DeleteWalkDifficulty(id));
        }

        #region
        private bool validateAddWalkDifficulty(WalkDifficultyRequestModel _walkDifficultyRequestModel)
        {
            if (_walkDifficultyRequestModel == null)
            {
                ModelState.AddModelError(nameof(_walkDifficultyRequestModel), $"{nameof(_walkDifficultyRequestModel)} is Invalid");
                return false;
            }
            if (string.IsNullOrWhiteSpace(_walkDifficultyRequestModel.Code))
            {
                ModelState.AddModelError(nameof(_walkDifficultyRequestModel.Code), $"{nameof(_walkDifficultyRequestModel.Code)} can't be empty");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        private bool validateUpdateWalkDifficulty(WalkDifficultyUpdateRequestModel _WalkDifficultyUpdateRequestModel)
        {
            if (_WalkDifficultyUpdateRequestModel == null)
            {
                ModelState.AddModelError(nameof(_WalkDifficultyUpdateRequestModel), $"{nameof(_WalkDifficultyUpdateRequestModel)} is Invalid");
                return false;
            }
            if (string.IsNullOrWhiteSpace(_WalkDifficultyUpdateRequestModel.Code))
            {
                ModelState.AddModelError(nameof(_WalkDifficultyUpdateRequestModel.Code), $"{nameof(_WalkDifficultyUpdateRequestModel.Code)} can't be empty");
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
