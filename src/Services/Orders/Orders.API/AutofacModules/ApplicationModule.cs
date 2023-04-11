namespace Orders.API.AutofacModules;

public class ApplicationModule : Autofac.Module
{
    public ApplicationModule()
    {
    }

    protected override void Load(ContainerBuilder builder)
    {
        #region repos
        builder.RegisterType<OrderRepository>()
            .As<IOrderRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<OrderQueryRepository>()
            .As<IOrderQueryRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<LookupQueryRepository>()
            .As<ILookupQueryRepository>()
            .InstancePerLifetimeScope();
        #endregion

        #region services
        builder.RegisterType<CurrentUser>()
            .As<ICurrentUser>()
            .InstancePerLifetimeScope();

        builder.RegisterType<OrderQueryService>()
            .As<IOrderQueryService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<LookupQueryService>()
            .As<ILookupQueryService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<IntegrationEventService>()
            .As<IIntegrationEventService>()
            .InstancePerLifetimeScope();
        #endregion
    }
}