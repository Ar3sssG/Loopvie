using AutoMapper;

namespace ASBusinessLogic.MapperProfiles
{
    public class PagedListProfile : Profile
    {
        public PagedListProfile()
        {
            //CreateMap<IPagedList<CompanyResponseModel>, PagedResponseModel<CompanyResponseModel>>()
            //    .ForPath(dest => dest.Data, opt => opt.MapFrom(src => src.ToList()))
            //    .ForPath(dest => dest.TotalItemCount, opt => opt.MapFrom(src => src.TotalItemCount))
            //    .ForPath(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
            //    .ForPath(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
            //    .ForPath(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
            //    .ForPath(dest => dest.PageCount, opt => opt.MapFrom(src => src.PageCount));
        }
    }
}
