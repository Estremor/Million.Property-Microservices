using AutoMapper;
using Properties.Application.Commands;
using Properties.Application.Dto;
using Properties.Domain.Entities;
namespace Properties.API.Automapper;

public sealed class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region Owner
        CreateMap<Owner, CreateOwnerCommand>()
            .ForMember(x => x.Photo, src => src.MapFrom(d => d.Photo == null ? string.Empty : Convert.ToBase64String(d.Photo)));

        CreateMap<CreateOwnerCommand, Owner>()
            .ForMember(x => x.Photo, src => src.MapFrom(d => string.IsNullOrWhiteSpace(d.Photo) ? null : Convert.FromBase64String(d.Photo)));
        #endregion

        #region Property
        // crate
        CreateMap<Property, CreatePropertyCommand>()
            .ForMember(x => x.OwnerDocument, src => src.Ignore())
            .ForMember(x => x.PropertyImages, src => src.Ignore());

        CreateMap<CreatePropertyCommand, Property>()
            .ForMember(x => x.IdOwner, src => src.Ignore());
        // read
        CreateMap<PropertyReadDto, Property>();
        CreateMap<Property, PropertyReadDto>();

        // update 
        CreateMap<UpdatePropertyCommand, Property>();
        CreateMap<UpdatePropertyCommand, PropertyTrace>().ForMember(x => x.Value, src => src.MapFrom(d => d.Value == null || d.Value <= 0 ? d.Price : d.Value));

        #endregion

        #region PropertyImage
        CreateMap<PropertyImage, Image>()
            .ForMember(x => x.File, src => src.MapFrom(d => d.File == null ? string.Empty : Convert.ToBase64String(d.File)))
        .ForMember(x => x.Enabled, src => src.MapFrom(d => d.Enabled));

        CreateMap<Image, PropertyImage>()
            .ForMember(x => x.File, src => src.MapFrom(d => string.IsNullOrWhiteSpace(d.File) ? null : Convert.FromBase64String(d.File)))
            .ForMember(x => x.Enabled, src => src.MapFrom(d => d.Enabled));
        #endregion

        #region Image
        CreateMap<CreateImageCommand, PropertyImage>()
            .ForMember(x => x.File, src => src.MapFrom(d => string.IsNullOrWhiteSpace(d.File) ? null : Convert.FromBase64String(d.File)));

        CreateMap<PropertyImage, CreateImageCommand>()
            .ForMember(x => x.File, src => src.MapFrom(d => d.File == null ? string.Empty : Convert.ToBase64String(d.File)));
        #endregion
    }
}
