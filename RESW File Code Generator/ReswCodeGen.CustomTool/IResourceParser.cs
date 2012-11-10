using System.Collections.Generic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool
{
    public interface IResourceParser
    {
        string ReswFileContents { get; set; }
        List<ResourceItem> Parse();
    }
}