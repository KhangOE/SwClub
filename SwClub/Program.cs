using SwClub.Repositories.UoW;
using SwClub.Common.Constants;
using SwClub.DataAccess.Contexts;
using SwClub.Entities.Models;
using SwClub.Repositories.Interfaces;
using SwClub.Services.IServices;
using SwClub.Services.Services;
using SwClub.Web.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using SwClub.Common.Constants;
using SwClub.Entities.Models;
using SwClub.Repositories.Interfaces;
using SwClub.Repositories.UoW;
using SwClub.Services.IServices;
using SwClub.Services.Services;
using iZOTA.Ticket.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateBootstrapLogger();

builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext());

// Add services to the container.
IServiceCollection services = builder.Services;

ConfigureServices(services);

var app = builder.Build();
if (Environment.GetEnvironmentVariable(GlobalConstant.Env.AUTO_MIGRATE) == "True")
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<SwClubDbContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment()
    || Environment.GetEnvironmentVariable(GlobalConstant.Env.SWAGGER_ENABLED) == "True")
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "iZOTA Ticket API V1");
    });
}

if (Environment.GetEnvironmentVariable(GlobalConstant.Env.HTTPS_REDIRECT) == "True")
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
});

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

void ConfigureServices(IServiceCollection services)
{
    // Get connection string and setup MySQL
    var connectionString = Environment.GetEnvironmentVariable(GlobalConstant.Env.DATABASE_URL);
    if (string.IsNullOrEmpty(connectionString))
    {
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    }

    services.AddDbContext<SwClubDbContext>(options =>
       // options.UseNpgsql(connectionString));
       options.UseSqlServer(connectionString));

    // Setup Identity
    services.AddIdentity<User, Role>()
        .AddEntityFrameworkStores<SwClubDbContext>()
        .AddDefaultTokenProviders();

    // Adding Authentication
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }) // Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = false,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1212121212asasasasasasasqwqwqwqwqwqwqw")),
        };
    });

    services.AddLocalization(options => options.ResourcesPath = GlobalConstant.FolderFile.ResourceFolder);

    services.AddControllers();
    // Register the Swagger generator, defining 1 or more Swagger documents
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "SwClub", Version = "v1" });

        /*
        // Set the comments path for the Swagger JSON and UI.
        var webXmlPath = Path.Combine(AppContext.BaseDirectory, "iZOTA.Ticket.Web.xml");
        c.IncludeXmlComments(webXmlPath);

        var commonXmlPath = Path.Combine(AppContext.BaseDirectory, "iZOTA.Ticket.Common.xml");
        c.SchemaFilter<EnumTypeSchemaFilter>(commonXmlPath);
        */
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new string[] { }
                    },
                });

        c.OperationFilter<SwaggerHeaderFilter>();
    });
   // services.AddSwaggerGenNewtonsoftSupport();

    services.AddAutoMapper(typeof(Program));

    // Config FormOptions
    services.Configure<FormOptions>(options =>
    {
        options.ValueLengthLimit = int.MaxValue;
        options.MultipartBodyLengthLimit = int.MaxValue;
        options.MemoryBufferThreshold = int.MaxValue;
    });

    // API versioning
    services.AddApiVersioning(config =>
    {
        config.DefaultApiVersion = new ApiVersion(1, 0);
        config.AssumeDefaultVersionWhenUnspecified = true;
        config.ReportApiVersions = true;
        config.ApiVersionReader = new UrlSegmentApiVersionReader();
    });

    services.AddDirectoryBrowser();

    // CORS
    services.AddCors();

    services.AddHttpClient();

    // Setup Dependency
    ConfigureDependency(services);
}

void ConfigureDependency(IServiceCollection services)
{
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    // Add scoped services
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IClubService, ClubService>();

    // Add singleton services
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
}