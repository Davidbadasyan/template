namespace UM.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        #region Domain -> Dto
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, o => o.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, o => o.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, o => o.MapFrom(src => src.Email))
            .ForMember(dest => dest.ApprovalStatus, o => o.MapFrom(src => src.GetStatus))
            .ForMember(dest => dest.Role, o => o.MapFrom(src => src.GetRole));

        CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.Country, o => o.MapFrom(src => src.Country))
            .ForMember(dest => dest.State, o => o.MapFrom(src => src.State))
            .ForMember(dest => dest.City, o => o.MapFrom(src => src.City))
            .ForMember(dest => dest.ZipCode, o => o.MapFrom(src => src.ZipCode))
            .ForMember(dest => dest.Street, o => o.MapFrom(src => src.Street));
        #endregion

        #region Dto -> Dto
        CreateMap<UserRegistrationDto, IdentityUserCreationDto>()
            .ForMember(dest => dest.FirstName, o => o.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, o => o.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, o => o.MapFrom(src => src.Email))
            .ForMember(dest => dest.Status, o => o.MapFrom(src => UserStatus.Approved.Name))
            .ForMember(dest => dest.Role, o => o.MapFrom(src => UserRole.User.Name));
        #endregion
    }
}