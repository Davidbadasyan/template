namespace Common.Application;

public abstract class BaseQuery
{
    protected readonly IMapper Mapper;
    protected readonly ICurrentUser CurrentUser;

    protected BaseQuery(
        IMapper mapper,
        ICurrentUser currentUser)
    {
        Mapper = mapper;
        CurrentUser = currentUser;
    }
}