using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    [ExcludeFromCodeCoverage]
    public class AppInsightsClient
    {
        private readonly TelemetryClient telemetryClient = null;

        public AppInsightsClient()
        {
            try
            {
                var configuration = TelemetryConfiguration.CreateDefault();
                configuration.ConnectionString =
                    "InstrumentationKey=eba88f76-06da-4ef8-a0fe-162b1a70f871;IngestionEndpoint=https://westeurope-5.in.applicationinsights.azure.com/;LiveEndpoint=https://westeurope.livediagnostics.monitor.azure.com/";

                telemetryClient = new TelemetryClient(configuration);
            }
            catch (Exception ex1)
            {
                Trace.WriteLine(ex1);

                try
                {
                    telemetryClient = new TelemetryClient();
#pragma warning disable CS0618 // Type or member is obsolete
                    telemetryClient.InstrumentationKey = "eba88f76-06da-4ef8-a0fe-162b1a70f871";
#pragma warning restore CS0618 // Type or member is obsolete
                }
                catch (Exception ex2)
                {
                    Trace.WriteLine(ex2);
                    return;
                }
            }

            telemetryClient.Context.Session.Id = Guid.NewGuid().ToString();
            telemetryClient.Context.Device.OperatingSystem = Environment.OSVersion.ToString();
            telemetryClient.Context.Component.Version = GetType().Assembly.GetName().Version.ToString();
        }

        public static AppInsightsClient Instance { get; } = new AppInsightsClient();

        public void TrackFeatureUsage(string featureName, params string[] tags)
        {
            telemetryClient?.TrackEvent(featureName);
            telemetryClient?.Flush();
        }

        public void TrackError(Exception exception)
        {
            telemetryClient?.TrackException(exception);
            telemetryClient?.Flush();
        }
    }
}