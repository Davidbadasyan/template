namespace Orders.Application.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        #region Domain -> Dto

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
            .ForMember(dest => dest.Number, o => o.MapFrom(src => src.Number))
            .ForMember(dest => dest.Description, o => o.MapFrom(src => src.Description))
            .ForMember(dest => dest.Weight, o => o.MapFrom(src => src.Weight))
            .ForMember(dest => dest.IsDraft, o => o.MapFrom(src => src.IsDraft))
            .ForMember(dest => dest.PaymentMethodId, o => o.MapFrom(src => src.PaymentMethod.Id))
            .ForMember(dest => dest.ShippingMethodId, o => o.MapFrom(src => src.ShippingMethod.Id))
            .ForMember(dest => dest.WeightUnitId, o => o.MapFrom(src => src.WeightUnit.Id));

        CreateMap<Order, OrderResponseDto>()
            .IncludeBase<Order, OrderDto>()
            .ForMember(dest => dest.Status, o => o.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.PaymentMethod, o => o.MapFrom(src => src.PaymentMethod.Name))
            .ForMember(dest => dest.ShippingMethod, o => o.MapFrom(src => src.ShippingMethod.Name))
            .ForMember(dest => dest.WeightUnit, o => o.MapFrom(src => src.WeightUnit.Name))
            .ForMember(dest => dest.Buyer, o => o.Ignore());

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductName, o => o.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.UnitPrice, o => o.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Discount, o => o.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Units, o => o.MapFrom(src => src.Units));

        CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.Street, o => o.MapFrom(src => src.Street))
            .ForMember(dest => dest.City, o => o.MapFrom(src => src.City))
            .ForMember(dest => dest.State, o => o.MapFrom(src => src.State))
            .ForMember(dest => dest.Country, o => o.MapFrom(src => src.Country))
            .ForMember(dest => dest.ZipCode, o => o.MapFrom(src => src.ZipCode));

        CreateMap<ICurrentUser, BuyerDto>()
            .ForMember(dest => dest.Email, o => o.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, o => o.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, o => o.MapFrom(src => src.LastName));

        CreateMap<PaginatedResult<Order>, PaginatedResult<OrderResponseDto>>();

        #endregion

        #region Dto -> Dto
        #endregion
    }
}