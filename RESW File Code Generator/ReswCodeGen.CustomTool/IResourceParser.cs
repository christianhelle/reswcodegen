using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool
{
    public interface IResourceParser
    {
        string ReswFileContents { get; set; }
        List<ResourceItem> Parse();
    }

    public class ResourceParser : IResourceParser
    {
        public ResourceParser(string reswFileContents)
        {
            ReswFileContents = reswFileContents;
        }

        public string ReswFileContents { get; set; }

        public List<ResourceItem> Parse()
        {
            var doc = XDocument.Parse(ReswFileContents);

            var list = new List<ResourceItem>();

            foreach (var element in doc.Descendants("data"))
            {
                list.Add(new ResourceItem
                {
                    Name = element.Attribute("name").Value,
                    Value = element.Descendants("value").First().Value,
                    Comment = element.Descendants("comment").First().Value
                });
            }

            return list;
        }
    }
}