namespace Orders.API.AutofacModules;

public class MediatorModule : Autofac.Module
{
    public MediatorModule()
    {
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(typeof(CreateOrderCommand).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>));

        builder.RegisterAssemblyTypes(typeof(CreateOrderCommand.CreateOrderCommandHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<>));

        builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
    }
}