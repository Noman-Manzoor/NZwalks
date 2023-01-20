using AutoMapper;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;

namespace NZwalks.Automapper
{
    public class WalkProfile: Profile
    {
        public WalkProfile() 
        {
            CreateMap<Walk, WalkDTO>()
                .ReverseMap();
            CreateMap<WalkDifficulty, WalkDifficultyDTO>()
                .ReverseMap();
        }
    }
}
