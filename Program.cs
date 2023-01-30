using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RssApi;
using RssApi.Utils.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidIssuer = AuthOptions.Issuer,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ClockSkew = TimeSpan.Zero
                    };
                });
builder.Services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Default", new()
                {
                    VaryByQueryKeys = new[] {"*"},
                    Duration = 30
                });
            })
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDbContext<RssDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddResponseCaching();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler(app => app.Run(async context =>
{
    context.Response.StatusCode = StatusCodes.Status400BadRequest;
    var exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();

    await context.Response.WriteAsync(exceptionHandler.Error.Message);
}));

app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
