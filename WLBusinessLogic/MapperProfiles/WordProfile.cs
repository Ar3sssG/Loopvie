﻿using AutoMapper;
using WLCommon.Models.Request;
using WLDataLayer.DAL.StoreEntities;

namespace WLBusinessLogic.MapperProfiles
{
    public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<WordRequestModel, Word>()
                .ForPath(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => src.CorrectAnswer))
                .ForPath(dest => dest.WrongVariants, opt => opt.MapFrom(src => src.WrongVariants))
                .ForPath(dest => dest.Text, opt => opt.MapFrom(src => src.Word))
                .ForPath(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Difficulty));
        }
    }
}
