namespace UM.API.AutofacModules;

public class MediatorModule : Autofac.Module
{
    public MediatorModule()
    {
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(typeof(RegisterUserCommand).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>));

        builder.RegisterAssemblyTypes(typeof(RegisterUserCommand.RegisterUserCommandHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<>));

        builder.RegisterAssemblyTypes(typeof(TestIntegrationEventHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
    }
}