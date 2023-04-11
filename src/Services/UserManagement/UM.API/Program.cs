// ========================================= Add services to the container. ============================================ //
var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", reloadOnChange: true, optional: true)
    .AddEnvironmentVariables();


builder.Services.AddRabbitMqEventBus(
    subscriptionClientName: builder.Configuration["EventBus:RabbitMq:SubscriptionClientName"],
    hostName: builder.Configuration["EventBus:RabbitMq:Connection"],
    userName: builder.Configuration["EventBus:UserName"],
    password: builder.Configuration["EventBus:Password"],
    retryCount: !string.IsNullOrEmpty(builder.Configuration["EventBus:RetryCount"]) ? int.Parse(builder.Configuration["EventBus:RetryCount"]) : 5);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


builder.Services.AddAutoMapper(c =>
{
    c.AddProfile(new UserProfile());
});

builder.Services.AddControllers();

builder.Services.AddDbContext<WritableDbContext, UmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(typeof(UmContext).GetTypeInfo().Assembly.GetName().Name);
        }));

builder.Services.AddDbContext<UmQueryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(typeof(UmQueryContext).GetTypeInfo().Assembly.GetName().Name);
        }));

builder.Services.AddDbContext<IntegrationEventLoggerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(typeof(UmContext).GetTypeInfo().Assembly.GetName().Name);
        }));

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IIdentityService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return new IdentityService(httpClientFactory, builder.Configuration["Identity:Url"]);
});

builder.Host.ConfigureContainer<ContainerBuilder>(b => b
    .RegisterModule(new ApplicationModule())
    .RegisterModule(new MediatorModule()));


// ========================================= Configure the HTTP request pipeline ============================================ //
var app = builder.Build();

app.UseEventBus();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseGlobalExceptionHandler();

app.UseRouting();

app.UseAuthorization();

app.UseRequestHeadersToClaims();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.TryMigrateDbContext<UmContext>();
app.TryMigrateDbContext<IntegrationEventLoggerContext>();

app.Run();