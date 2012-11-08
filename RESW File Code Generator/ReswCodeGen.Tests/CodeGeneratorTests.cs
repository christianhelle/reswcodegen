using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Resources.resw")]
    public class CodeGeneratorTests
    {
        private string reswFileContents;

        [TestInitialize]
        public void Initialize()
        {
            reswFileContents = File.ReadAllText("Resources.resw");
        }

        [TestMethod]
        public void GenerateCodeDoesNotReturnNull()
        {
            var target = new CodeGeneratorFactory().Create("TestApp", reswFileContents);
            var actual = target.GenerateCode();
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void GeneratedCodeContainsPropertiesDefinedInResources()
        {
            var target = new CodeGeneratorFactory().Create("TestApp", reswFileContents);
            var resourceItems = target.ResourceParser.Parse();
            var actual = target.GenerateCode();

            foreach (var item in resourceItems.Where(item => !item.Name.Contains(".")))
                Assert.IsTrue(actual.Contains("public static string " + item.Name));
        }

        [TestMethod]
        public void GeneratedCodePropertiesContainsCommentsSimilarToValuesDefinedInResources()
        {
            var target = new CodeGeneratorFactory().Create("TestApp", reswFileContents);
            var resourceItems = target.ResourceParser.Parse();
            var actual = target.GenerateCode();

            foreach (var item in resourceItems.Where(item => !item.Name.Contains(".")))
                Assert.IsTrue(actual.Contains("Localized resource similar to \"" + item.Value + "\""));
        }
    }
}