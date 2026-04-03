using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.WinUI.CodeGeneration;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.Extensibility.Shell;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.WinUI.Commands;

/// <summary>
/// Solution Explorer context-menu command that generates a WinUI 3-compatible
/// strongly-typed C# helper class from the selected .resw file.
///
/// This command is shown only when a .resw file is selected in Solution Explorer.
/// It works around the limitation that Visual Studio's PRIResource item type
/// (used by WinUI 3 projects) does not support the IVsSingleFileGenerator
/// (Custom Tool) mechanism.
/// </summary>
[VisualStudioContribution]
internal class GenerateReswCodeCommand : Command
{
    public override CommandConfiguration CommandConfiguration => new("Generate ResW Code (WinUI 3)")
    {
        Placements = [CommandPlacement.KnownPlacements.SolutionExplorerContextMenu],
        Icon = new(ImageMoniker.KnownValues.GenerateFile, IconSettings.IconAndText),
        // Only show and enable the command when a .resw file is selected
        VisibleWhen = ActivationConstraint.ClientContext(
            ClientContextKey.Shell.ActiveSelectionFileName, @"\.resw$"),
        EnabledWhen = ActivationConstraint.ClientContext(
            ClientContextKey.Shell.ActiveSelectionFileName, @"\.resw$"),
    };

    /// <inheritdoc />
    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        try
        {
            // GetSelectedPathAsync returns the local paths of all currently selected items
            // in the workspace tree (Solution Explorer). For a context menu command filtered
            // to .resw files, this will contain the right-clicked .resw file's path.
            var selectedPaths = await context.GetSelectedPathAsync(cancellationToken);
            var selectedFilePath = selectedPaths?
                .FirstOrDefault(p => p.EndsWith(".resw", StringComparison.OrdinalIgnoreCase));

            if (selectedFilePath == null)
            {
                await this.Extensibility.Shell().ShowPromptAsync(
                    "No .resw file is currently selected. " +
                    "Please right-click a .resw file in Solution Explorer.",
                    PromptOptions.OK,
                    cancellationToken);
                return;
            }

            // Get the active project's default namespace so the generated class uses it
            var project = await context.GetActiveProjectAsync(
                q => q.With(p => p.DefaultNamespace),
                cancellationToken);

            var className = Path.GetFileNameWithoutExtension(selectedFilePath);
            var namespaceName = project?.DefaultNamespace ?? className;

            // Read the .resw file content
            var fileContents = await File.ReadAllTextAsync(selectedFilePath, cancellationToken);

            // Generate WinUI 3-compatible strongly-typed helper class
            var generator = new WinUICodeGenerator(fileContents, className, namespaceName);
            var generatedCode = generator.GenerateCode();

            // Write the .g.cs file next to the .resw file
            var outputPath = Path.Combine(
                Path.GetDirectoryName(selectedFilePath)!,
                className + ".g.cs");

            await File.WriteAllTextAsync(outputPath, generatedCode, cancellationToken);

            await this.Extensibility.Shell().ShowPromptAsync(
                $"Code generated successfully:\n{outputPath}",
                PromptOptions.OK,
                cancellationToken);
        }
        catch (Exception ex)
        {
            await this.Extensibility.Shell().ShowPromptAsync(
                $"Error generating code: {ex.Message}",
                PromptOptions.OK,
                cancellationToken);
        }
    }
}
