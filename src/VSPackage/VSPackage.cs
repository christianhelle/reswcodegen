using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage
{
    [ExcludeFromCodeCoverage]
    [Guid("3E780C6B-4ED6-49B8-9A30-916660330987")]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(UIContextGuids.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    [InstalledProductRegistration(
        "ResW File Code Generator",
        "A Visual Studio Custom Tool for generating a strongly typed helper class for accessing localized resources from a .ResW file.",
        "6a4c1726-440f-4b2d-a2e5-711277da6099")]
    public class VSPackage : AsyncPackage
    {
        protected override System.Threading.Tasks.Task InitializeAsync(
            CancellationToken cancellationToken,
            IProgress<ServiceProgressData> progress)
        {
            AppInsightsClient.Instance.Initialize();
            return base.InitializeAsync(cancellationToken, progress);
        }
    }
}
