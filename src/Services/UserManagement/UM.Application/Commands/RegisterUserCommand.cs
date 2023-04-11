namespace UM.Application.Commands;

public class RegisterUserCommand : BaseCommand<UserDto>
{
    public UserRegistrationDto UserRegistration { get; set; }

    public RegisterUserCommand(UserRegistrationDto userRegistration)
    {
        UserRegistration = userRegistration;
    }

    public class RegisterUserCommandHandler : BaseCommandHandler<RegisterUserCommand>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(
            IMapper mapper,
            ICurrentUser currentUser,
            IIdentityService identityService,
            IUserQueryRepository userQueryRepository,
            IUserRepository userRepository) : base(mapper, currentUser)
        {
            _identityService = identityService;
            _userQueryRepository = userQueryRepository;
            _userRepository = userRepository;
        }

        public override async Task<ResponseModel<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var identityUserCreation = Mapper.Map<IdentityUserCreationDto>(request.UserRegistration);

            var identityResponse = await _identityService.CreateUserAsync(identityUserCreation);

            var user = await User.CreateUserAsync(
                _userQueryRepository,
                identityResponse,
                request.UserRegistration.Email,
                request.UserRegistration.FirstName,
                request.UserRegistration.LastName);

            await _userRepository.AddAsync(user);
            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return ResponseModel<UserDto>.Create(ResponseType.Success, Mapper.Map<UserDto>(user));
        }


    }
}