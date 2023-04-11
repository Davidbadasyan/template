namespace Common.Application;
public abstract class BaseCommand : IRequest<ResponseModel>
{
    public abstract class BaseCommandHandler<TRequest> : IRequestHandler<TRequest, ResponseModel>
        where TRequest : BaseCommand
    {
        protected readonly IMapper Mapper;
        protected readonly ICurrentUser CurrentUser;
        protected BaseCommandHandler(
            IMapper mapper,
            ICurrentUser currentUser)
        {
            Mapper = mapper;
            CurrentUser = currentUser;
        }

        public abstract Task<ResponseModel> Handle(TRequest request, CancellationToken cancellationToken);
    }
}

public abstract class BaseCommand<TResponse> : IRequest<ResponseModel<TResponse>> where TResponse : IResponseDto
{
    public abstract class BaseCommandHandler<TRequest> : IRequestHandler<TRequest, ResponseModel<TResponse>>
        where TRequest : BaseCommand<TResponse>
    {
        protected readonly IMapper Mapper;
        protected readonly ICurrentUser CurrentUser;
        protected BaseCommandHandler(
            IMapper mapper,
            ICurrentUser currentUser)
        {
            Mapper = mapper;
            CurrentUser = currentUser;
        }

        public abstract Task<ResponseModel<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

    }
}