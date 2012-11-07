using System.Collections.Generic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool
{
    public interface IResourceParser
    {
        string ReswContent { get; set; }
        List<ResourceItem> Parse();
    }
}