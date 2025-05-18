namespace Hospital.API.Extensions;
public static class CorsPolicyServices
{
    private static readonly string PolicyName = "CorsPolicy";

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName, corsBuilder =>
            {
                corsBuilder.WithOrigins("http://localhost:8100").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                corsBuilder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                corsBuilder.WithOrigins("http://localhost").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
        });

        return services;
    }

    public static void UseCorsPolicy(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseCors(PolicyName);
    }
}