using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.WinUI.CodeGeneration;

/// <summary>Parses a .resw XML file and returns the list of string resource items.</summary>
internal sealed class ResourceParser
{
    private readonly string reswFileContents;

    public ResourceParser(string reswFileContents)
    {
        this.reswFileContents = reswFileContents;
    }

    /// <summary>
    /// Parses the .resw XML and returns one <see cref="ResourceItem"/> per
    /// <c>&lt;data&gt;</c> element that has a <c>name</c> attribute.
    /// </summary>
    public List<ResourceItem> Parse()
    {
        var doc = XDocument.Parse(reswFileContents);
        var list = new List<ResourceItem>();

        foreach (var element in doc.Descendants("data"))
        {
            if (element.Attributes().All(a => a.Name != "name"))
                continue;

            var item = new ResourceItem();

            var nameAttr = element.Attribute("name");
            if (nameAttr != null)
                item.Name = nameAttr.Value;

            var valueElement = element.Descendants("value").FirstOrDefault();
            if (valueElement != null)
                item.Value = valueElement.Value;

            var commentElement = element.Descendants("comment").FirstOrDefault();
            if (commentElement != null)
                item.Comment = commentElement.Value;

            list.Add(item);
        }

        return list;
    }
}
