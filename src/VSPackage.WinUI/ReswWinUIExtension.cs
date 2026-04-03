using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.WinUI;

/// <summary>
/// Extension entry point for the ResW File Code Generator for WinUI 3.
/// This out-of-process extension adds a Solution Explorer context-menu command
/// for .resw files that generates a strongly-typed C# helper class using the
/// WinUI 3 resource API (Microsoft.Windows.ApplicationModel.Resources.ResourceLoader).
/// </summary>
[VisualStudioContribution]
internal class ReswWinUIExtension : Extension
{
    public override ExtensionConfiguration ExtensionConfiguration => new()
    {
        Metadata = new(
            id: "ReswFileCodeGenerator.WinUI.d4e5f789-1234-5678-abcd-90ef12345678",
            version: this.ExtensionAssemblyVersion,
            publisherName: "Christian Resma Helle",
            displayName: "ResW File Code Generator for WinUI 3",
            description:
                "Generates strongly-typed helper classes for .ResW files in WinUI 3 projects. " +
                "Right-click any .resw file in Solution Explorer and select " +
                "'Generate ResW Code (WinUI 3)' to create a C# helper class that uses " +
                "Microsoft.Windows.ApplicationModel.Resources.ResourceLoader."),
    };

    protected override void InitializeServices(IServiceCollection serviceCollection)
    {
        base.InitializeServices(serviceCollection);
    }
}
