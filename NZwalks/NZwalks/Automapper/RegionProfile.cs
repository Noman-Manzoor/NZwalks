using AutoMapper;

namespace NZwalks.Automapper
{
    public class RegionProfile: Profile
    {
        public RegionProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTOs.RegionDTO>()
                     .ReverseMap();
        }
    }
}
