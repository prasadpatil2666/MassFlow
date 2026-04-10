using System;
using System.Diagnostics;
using MassTransit.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Azure.Monitor.OpenTelemetry.Exporter;

namespace SharedConfiguration;

public static class TelemetryConfigurationExtensions
{
    public static void AddOpenTelemetry(this IServiceCollection services, string serviceName, IConfiguration configuration)
    {
        var applicationInsightsConnectionString = configuration["ApplicationInsightsConnectionString"];

        services.AddOpenTelemetry()
            .WithMetrics(builder =>
            {
                builder
                    .AddMeter("MassTransit")
                    .SetResourceBuilder(ResourceBuilder.CreateDefault()
                        .AddService(serviceName)
                        .AddTelemetrySdk()
                        .AddEnvironmentVariableDetector())
                    .AddOtlpExporter(o =>
                    {
                        o.Endpoint = new Uri(HostMetadataCache.IsRunningInContainer ? "http://grafana-agent:14317" : "http://localhost:12345");
                        o.Protocol = OtlpExportProtocol.Grpc;
                        // Batching for Metrics is handled via MetricReaderOptions, not here.
                    })
                    .AddAzureMonitorMetricExporter(o =>
                    {
                        o.ConnectionString = applicationInsightsConnectionString;
                    });
            })
            .WithTracing(builder =>
            {
                builder
                    .AddSource("MassTransit")
                    .AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources")
                    .SetResourceBuilder(ResourceBuilder.CreateDefault()
                        .AddService(serviceName)
                        .AddTelemetrySdk()
                        .AddEnvironmentVariableDetector())
                    .AddAspNetCoreInstrumentation()
                     
                    .AddOtlpExporter(o =>
                    {
                        o.Endpoint = new Uri(HostMetadataCache.IsRunningInContainer ? "http://tempo:4317" : "http://localhost:4317");
                        o.Protocol = OtlpExportProtocol.Grpc;
                        o.ExportProcessorType = ExportProcessorType.Batch;
                        o.BatchExportProcessorOptions = new BatchExportProcessorOptions<Activity>
                        {
                            MaxQueueSize = 2048,
                            ScheduledDelayMilliseconds = 5000,
                            ExporterTimeoutMilliseconds = 30000,
                            MaxExportBatchSize = 512,
                        };
                    })
                    .AddAzureMonitorTraceExporter(o =>
                    {
                        o.ConnectionString = applicationInsightsConnectionString;
                    });
            });
    }
}