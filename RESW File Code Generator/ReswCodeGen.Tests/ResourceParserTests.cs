using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Resources.resw")]
    public class ResourceParserTests
    {
        private string reswFileContents;

        [TestInitialize]
        public void Initialize()
        {
            reswFileContents = File.ReadAllText("Resources.resw");
        }

        [TestMethod]
        public void CanLoadTestReswFileContents()
        {
            Assert.IsNotNull(reswFileContents);
        }

        [TestMethod]
        public void CanTestReswFileContentsLoadToXDocument()
        {
            var doc = XDocument.Parse(reswFileContents);
            Assert.IsNotNull(doc);
        }

        [TestMethod]
        public void CanParseTestReswFileContents()
        {
            var doc = XDocument.Parse(reswFileContents);
            var resourcs = doc.Descendants("data");
            Assert.IsNotNull(resourcs);
            Assert.AreNotEqual(0, resourcs.Count());
        }

        [TestMethod]
        public void CanParseTestReswFileContentsIntoResourceItems()
        {
            var doc = XDocument.Parse(reswFileContents);

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

            CollectionAssert.AllItemsAreNotNull(list);
        }
    }
}