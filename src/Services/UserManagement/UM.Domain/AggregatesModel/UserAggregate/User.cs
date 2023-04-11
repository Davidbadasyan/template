namespace UM.Domain.AggregatesModel.UserAggregate;

public class User : Entity, IAggregateRoot
{
    public string Email { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }

    public UserStatus Status { get; protected set; }
    private int _statusId;

    public UserRole Role { get; protected set; }
    private int _roleId;

    protected User()
    {
    }

    public User(
        string email,
        string firstName,
        string lastName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        _statusId = UserStatus.Approved.Id;
        _roleId = UserRole.User.Id;
    }


    public static async Task<User> CreateUserAsync(
        IUserQueryRepository userQueryRepository,
        string identityResponse,
        string email,
        string firstName,
        string lastName)
    {
        if (await userQueryRepository.UserExistsAsync(email))
            throw new UmDomainException("User already exists");

        if (!string.IsNullOrWhiteSpace(identityResponse))
            throw new UmDomainException(identityResponse);

        var user = new User(email, firstName, lastName);

        return user;
    }

    public void UpdateDetails(
        string firstName,
        string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new UmDomainException("Names must be specified");

        FirstName = firstName;
        LastName = lastName;
    }

    public void Approve()
    {
        //if (_statusId == UserStatus.WaitingForApproval.Id)
        //    throw new UmDomainException("");

        _statusId = UserStatus.Approved.Id;
    }

    public string GetStatus => UserStatus.From(_statusId).Name;
    public string GetRole => UserRole.From(_roleId).Name;
}