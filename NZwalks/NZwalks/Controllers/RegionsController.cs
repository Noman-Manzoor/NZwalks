using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.Data;
using NZwalks.Models.Domain;
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
            this._mapper= mapper;
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


    }
}
