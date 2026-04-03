namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.WinUI.CodeGeneration;

/// <summary>Represents a single string resource entry parsed from a .resw file.</summary>
internal sealed class ResourceItem
{
    /// <summary>The resource key name (may contain dots, e.g. "Button.Text").</summary>
    public string? Name { get; set; }

    /// <summary>The localized string value.</summary>
    public string? Value { get; set; }

    /// <summary>An optional developer comment associated with the resource.</summary>
    public string? Comment { get; set; }
}
