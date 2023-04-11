namespace UM.Domain.AggregatesModel.UserAggregate;

public class UserStatus : Enumeration
{
    public static UserStatus WaitingForApproval = new(1, "Waiting for approval");
    public static UserStatus Approved = new(2, "Approved");
    public static UserStatus Rejected = new(3, "Rejected");
    public static UserStatus Deactivated = new(4, "Deactivated");

    public UserStatus(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<UserStatus> List() =>
        new[] { WaitingForApproval, Approved, Rejected, Deactivated };

    public static UserStatus FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new UmDomainException($"Possible values for UserStatus: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static UserStatus From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new UmDomainException($"Possible values for UserStatus: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}