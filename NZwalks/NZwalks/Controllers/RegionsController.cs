using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZwalks.Data;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;
using NZwalks.Repository;

namespace NZwalks.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class RegionsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRegionRepository _regionRepository;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this._regionRepository = regionRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllRegions")]
        [Authorize]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllAsync();

            // return DTOs without automapper
            //var regionsList = new List<Models.DTOs.Region>();
            //foreach (var region in regions)
            //{
            //    var regionDto = new Models.DTOs.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Code = region.Code,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
            //    };
            //    regionsList.Add(regionDto);
            //}

            // using automapper
            var regionsList = _mapper.Map<List<Models.DTOs.RegionDTO>>(regions);

            return Ok(regionsList);

        }

        [HttpGet]
        [Route("getRegion{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await _regionRepository.GetRegionById(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = _mapper.Map<Models.DTOs.RegionDTO>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        [Route("AddRegion")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRegion(RegionRequestModel regionRequestModel)
        {
            //validating the model

            if (!ValidateAddRegion(regionRequestModel))
            {
                return BadRequest(ModelState);
            };

            // DTO to domain model
            var region = new Models.Domain.Region()
            {
                Id = new Guid(),
                Name = regionRequestModel.Name,
                Area = regionRequestModel.Area,
                Code = regionRequestModel.Code,
                Lat = regionRequestModel.Lat,
                Long = regionRequestModel.Long,
                Population = regionRequestModel.Population
            };


            // send domain model to repository or service layer

            return Ok(await _regionRepository.AddRegion(region));

            //convert domain model to DTO again and return DTO if we want to return object
        }

        [HttpDelete]
        [Route("DeleteRegion{id:guid}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            return Ok(await _regionRepository.DeleteRegion(id));
        }

        [HttpPut]
        [Route("UpdateRegion")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRegion(Guid id, UpdateRegionRequest updateRegionRequest)
        {
            if(!ValidateUpdateRegion(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }
            // convert DTO to domain model
            var region = new Models.Domain.Region()
            {
                Id = id,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Code = updateRegionRequest.Code,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };

            // pass domain model to repository or service layer
            return Ok(await _regionRepository.UpdateRegionAsync(id, region));
        }

        #region private methods
        private bool ValidateAddRegion(RegionRequestModel regionRequestModel)
        {
            if (regionRequestModel == null)
            {
                ModelState.AddModelError(nameof(regionRequestModel), "Add region data is required");
                return false;
            }
            if(string.IsNullOrWhiteSpace(regionRequestModel.Name))
            {
                ModelState.AddModelError(nameof(regionRequestModel.Name), $"{nameof(regionRequestModel.Name)} can't be null or empty");
            }
            if (string.IsNullOrWhiteSpace(regionRequestModel.Code))
            {
                ModelState.AddModelError(nameof(regionRequestModel.Code), $"{nameof(regionRequestModel.Code)} can't be null or empty");
            }
            if (regionRequestModel.Lat <= 0)
            {
                ModelState.AddModelError(nameof(regionRequestModel.Lat), $"{nameof(regionRequestModel.Lat)} can't be less than or equal to zero");
            }
            if (regionRequestModel.Long <= 0)
            {
                ModelState.AddModelError(nameof(regionRequestModel.Long), $"{nameof(regionRequestModel.Long)} can't be less than or equal to zero");
            }
            if (regionRequestModel.Area <= 0)
            {
                ModelState.AddModelError(nameof(regionRequestModel.Area), $"{nameof(regionRequestModel.Area)} can't be less than or equal to zero");
            }
            if (regionRequestModel.Population < 0)
            {
                ModelState.AddModelError(nameof(regionRequestModel.Population), $"{nameof(regionRequestModel.Population)} can't be less than zero");
            }

            if(ModelState.ErrorCount> 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateRegion(UpdateRegionRequest UpdateRegionRequest)
        {
            if (UpdateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest), "Add region data is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(UpdateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Name), $"{nameof(UpdateRegionRequest.Name)} can't be null or empty");
            }
            if (string.IsNullOrWhiteSpace(UpdateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Code), $"{nameof(UpdateRegionRequest.Code)} can't be null or empty");
            }
            if (UpdateRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Lat), $"{nameof(UpdateRegionRequest.Lat)} can't be less than or equal to zero");
            }
            if (UpdateRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Long), $"{nameof(UpdateRegionRequest.Long)} can't be less than or equal to zero");
            }
            if (UpdateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Area), $"{nameof(UpdateRegionRequest.Area)} can't be less than or equal to zero");
            }
            if (UpdateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(UpdateRegionRequest.Population), $"{nameof(UpdateRegionRequest.Population)} can't be less than zero");
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
