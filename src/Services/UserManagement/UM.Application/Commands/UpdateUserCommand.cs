namespace UM.Application.Commands;

public class UpdateUserCommand : BaseCommand<UserDto>
{
    public long UserId { get; set; }
    public UserUpdateDto UserUpdate { get; set; }
    public UpdateUserCommand(
        long userId,
        UserUpdateDto userUpdate)
    {
        UserId = userId;
        UserUpdate = userUpdate;
    }

    public class UpdateUserCommandHandler : BaseCommandHandler<UpdateUserCommand>
    {

        private readonly IUserRepository _userRepository;
        private readonly IIntegrationEventService _integrationEventService;
        public UpdateUserCommandHandler(
            IMapper mapper,
            ICurrentUser currentUser,
            IUserRepository userRepository,
            IIntegrationEventService integrationEventService) : base(mapper, currentUser)
        {
            _userRepository = userRepository;
            _integrationEventService = integrationEventService;
        }

        public override async Task<ResponseModel<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                return ResponseModel<UserDto>.Create(ResponseType.Info, message: "User not found");

            user.UpdateDetails(request.UserUpdate.FirstName, request.UserUpdate.LastName);

            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            await _integrationEventService.AddAndSaveEventAsync(new TestIntegrationEvent());

            var userDto = Mapper.Map<UserDto>(user);

            return ResponseModel<UserDto>.Create(ResponseType.Success, userDto);
        }
    }
}