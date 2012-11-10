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
        public void ConstructorSetsReswFileContents()
        {
            var target = new ResourceParser(reswFileContents);
            Assert.IsNotNull(target.ReswFileContents);
        }

        [TestMethod]
        public void ParseReturnsResourceItems()
        {
            var target = new ResourceParser(reswFileContents);
            var actual = target.Parse();
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ParseReturnedResourceItemsContainsName()
        {
            var target = new ResourceParser(reswFileContents);
            var actual = target.Parse();

            foreach (var item in actual)
                Assert.IsNotNull(item.Name);
        }

        [TestMethod]
        public void ParseReturnedResourceItemsContainsValue()
        {
            var target = new ResourceParser(reswFileContents);
            var actual = target.Parse();

            foreach (var item in actual)
                Assert.IsNotNull(item.Value);
        }

        [TestMethod]
        public void ParseReturnedResourceItemsContainsComment()
        {
            var target = new ResourceParser(reswFileContents);
            var actual = target.Parse();

            foreach (var item in actual)
                Assert.IsNotNull(item.Comment);
        }
    }
}