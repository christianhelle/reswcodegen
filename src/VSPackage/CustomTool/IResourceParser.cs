using System.Collections.Generic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    public interface IResourceParser
    {
        string ReswFileContents { get; }
        List<ResourceItem> Parse();
    }
}
