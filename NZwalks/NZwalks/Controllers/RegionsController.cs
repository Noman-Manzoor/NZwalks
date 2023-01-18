using AutoMapper;
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
            var regionsList = _mapper.Map<List<Models.DTOs.Region>>(regions);

            return Ok(regionsList);

        }

        [HttpGet]
        [Route("getRegion{id:guid}")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await _regionRepository.GetRegionById(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = _mapper.Map<Models.DTOs.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        [Route("AddRegion")]
        public async Task<IActionResult> AddRegion(RegionRequestModel regionRequestModel)
        {
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
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            return Ok(await _regionRepository.DeleteRegion(id));
        }

        [HttpPut]
        [Route("UpdateRegion")]
        public async Task<IActionResult> UpdateRegion(Guid id, UpdateRegionRequest updateRegionRequest)
        {
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

    }
}
