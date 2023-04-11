namespace Identity.API.AutofacModules;

public class ApplicationModule : Autofac.Module
{
    public ApplicationModule()
    {
    }

    protected override void Load(ContainerBuilder builder)
    {
        //builder.RegisterAssemblyTypes(typeof(UserDetailsUpdatedIntegrationEventHandler).GetTypeInfo().Assembly)
        //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
    }
}