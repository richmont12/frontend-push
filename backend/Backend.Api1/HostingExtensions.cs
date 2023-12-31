using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Serilog;
using SignalRChat.Hubs;

namespace Backend.Api1;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddHttpClient();
        
        builder.Services.AddSignalR();

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.Audience = "dataapi1";
                });
        
        builder.Services.AddAuthorization();
     

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        
        app.UseDeveloperExceptionPage();
        app.UseCors(config => config.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
        
        IdentityModelEventSource.ShowPII = true;   
        // System.Net.ServicePointManager.ServerCertificateValidationCallback += 
        //     (sender, certificate, chain, sslPolicyErrors) => true;
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(x => x.MapControllers().RequireAuthorization());

        app.MapHub<NotificationHub>("/notiHub");

        return app;
    }
}