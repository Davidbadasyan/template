namespace UM.API.AutofacModules;

public class ApplicationModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        #region repos
        builder.RegisterType<UserRepository>()
            .As<IUserRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UserQueryRepository>()
            .As<IUserQueryRepository>()
            .InstancePerLifetimeScope();
        #endregion

        #region services
        builder.RegisterType<CurrentUser>()
            .As<ICurrentUser>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UserQueryService>()
            .As<IUserQueryService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<IntegrationEventService>()
            .As<IIntegrationEventService>()
            .InstancePerLifetimeScope();
        #endregion
    }
}