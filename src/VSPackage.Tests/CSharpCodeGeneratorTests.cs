using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Resources.resw")]
    public class CSharpCodeGeneratorTests
    {
        private string reswFileContents;
        private const string FILE_PATH = "Resources.resw";
        private string actual;
        private ICodeGenerator target;

        [TestInitialize]
        public void Initialize()
        {
            reswFileContents = File.ReadAllText(FILE_PATH);

            target = new CodeGeneratorFactory().Create(FILE_PATH.Replace(".resw", string.Empty), "TestApp", reswFileContents);
            actual = target.GenerateCode();
        }

        [TestMethod]
        public void GenerateCodeDoesNotReturnNull()
        {
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void GeneratedCodeIsAPublicClass()
        {
            Assert.IsTrue(actual.Contains("public sealed partial class"));
        }

        [TestMethod]
        public void GeneratedCodeContainsPropertiesDefinedInResources()
        {
            var resourceItems = target.ResourceParser.Parse();

            foreach (var item in resourceItems)
            {
                var value = $"public static string {item.Name.Replace(".", "_")}";
                Assert.IsTrue(actual.Contains(value));
            }
        }

        [TestMethod]
        public void GeneratedCodeReplacesDottedKeysWithForwardSlash()
        {
            var resourceItems = target.ResourceParser.Parse();

            foreach (var item in resourceItems)
            {
                var value = $"GetString(\"{item.Name.Replace(".", "/")}\")";
                Assert.IsTrue(actual.Contains(value));
            }
        }

        [TestMethod]
        public void GeneratedCodePropertiesContainsCommentsSimilarToValuesDefinedInResources()
        {
            var resourceItems = target.ResourceParser.Parse();

            foreach (var item in resourceItems)
                Assert.IsTrue(actual.Contains("Localized resource similar to \"" + item.Value + "\""));
        }

        [TestMethod]
        public void ClassNameEqualsFileNameWithoutExtension()
        {
            Assert.IsTrue(actual.Contains("class Resources"));
        }

        [TestMethod]
        public void ResourceLoaderInitializedWithClassName()
        {
            Assert.IsTrue(actual.Contains("ResourceLoader.GetForCurrentView(currentAssemblyName + \"/Resources\");"));
        }

        [TestMethod]
        public void ContainsProjectUrl()
        {
            Assert.IsTrue(actual.Contains("http://bit.ly/reswcodegen"));
        }
    }
}