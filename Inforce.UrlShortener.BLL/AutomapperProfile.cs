using AutoMapper;
using Inforce.UrlShortener.Abstraction.DTOs;
using Inforce.UrlShortener.Entities;

namespace Inforce.UrlShortener.BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            // Mapping for Url <-> UrlDto
            this.CreateMap<Url, UrlDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.OriginalUrl, opt => opt.MapFrom(entity => entity.OriginalUrl))
                .ForMember(dto => dto.ShortUrl, opt => opt.MapFrom(entity => entity.ShortUrl))
                .ForMember(dto => dto.CreatedDate, opt => opt.MapFrom(entity => entity.CreatedDate))
                .ForMember(dto => dto.CreatorId, opt => opt.MapFrom(entity => entity.CreatedBy))
                .ForMember(dto => dto.CreatedBy, opt => opt.MapFrom(entity => entity.User.Login))
                .ReverseMap();

            // Mapping for User <-> UserDto
            this.CreateMap<User, UserDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Login, opt => opt.MapFrom(entity => entity.Login))
                .ForMember(dto => dto.Password, opt => opt.MapFrom(entity => entity.Password))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(entity => entity.Email))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(entity => entity.PhoneNumber))
                .ForMember(dto => dto.RoleName, opt => opt.MapFrom(entity => entity.Role.RoleName))
                .ReverseMap();
        }
    }
}
