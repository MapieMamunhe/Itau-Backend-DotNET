using Microsoft.OpenApi;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

var logPath = Path.Combine(builder.Environment.ContentRootPath, "Logs");

Directory.CreateDirectory(logPath); 

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(
        Path.Combine(logPath, "log-.txt"),
        rollingInterval: RollingInterval.Day
    )
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    builder.Services.AddOpenTelemetry()
    .WithMetrics(builder =>
    {
        builder.AddPrometheusExporter();

        builder.AddMeter("Microsoft.AspNetCore.Hosting",
                         "Microsoft.AspNetCore.Server.Kestrel");
        builder.AddView("http.server.request.duration",
            new ExplicitBucketHistogramConfiguration
            {
                Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05,
                       0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
            });
    });

    // MEtricas
    var tracingOtlpEndpoint = builder.Configuration["OTLP_ENDPOINT_URL"];
    var otel = builder.Services.AddOpenTelemetry();

    // Configure OpenTelemetry Resources with the application name
    otel.ConfigureResource(resource => resource
        .AddService(serviceName: builder.Environment.ApplicationName));

    // Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
    otel.WithMetrics(metrics => metrics
        // Metrics provider from OpenTelemetry
        .AddAspNetCoreInstrumentation()
        // Metrics provides by ASP.NET Core in .NET 8
        .AddMeter("Microsoft.AspNetCore.Hosting")
        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
        // Metrics provided by System.Net libraries
        .AddMeter("System.Net.Http")
        .AddMeter("System.Net.NameResolution")
        .AddPrometheusExporter());

    // Add Tracing for ASP.NET Core and our custom ActivitySource and export to Jaeger
    otel.WithTracing(tracing =>
    {
        tracing.AddAspNetCoreInstrumentation();
        tracing.AddHttpClientInstrumentation();
        if (tracingOtlpEndpoint != null)
        {
            tracing.AddOtlpExporter(otlpOptions =>
            {
                otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint);
            });
        }
        else
        {
            tracing.AddConsoleExporter();
        }
    });

    // Fim das metricas

    builder.Services.AddSerilog();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "ITAU Backend", Version = "v1" });
    });

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    var app = builder.Build();
    app.MapGet("/", () => Results.Redirect("/swagger"));

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("v1/swagger.json", "ITAU Backend");
        });
    }


    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.MapPrometheusScrapingEndpoint();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}