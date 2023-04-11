namespace UM.Domain.AggregatesModel.UserAggregate;

public class UserRole : Enumeration
{
    public static UserRole SuperAdmin = new(1, nameof(SuperAdmin).ToLowerInvariant());
    public static UserRole User = new(2, nameof(User).ToLowerInvariant());

    public UserRole(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<UserRole> List() =>
        new[] { User, SuperAdmin };

    public static UserRole FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new UmDomainException($"Possible values for UserRole: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static UserRole From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new UmDomainException($"Possible values for UserRole: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}