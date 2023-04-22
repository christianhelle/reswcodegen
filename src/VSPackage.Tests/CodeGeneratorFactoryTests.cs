using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Resources.resw")]
    public class CodeGeneratorFactoryTests
    {
        private string reswFileContents;
        private const string CLASS_NAME = "C:\\Test\\Resources\\Strings.resw";

        [TestInitialize]
        public void Initialize()
        {
            reswFileContents = File.ReadAllText("Resources.resw");
        }

        [TestMethod]
        public void CodeGeneratorFactoryReturnsValidInstance()
        {
            var target = new CodeGeneratorFactory();
            var actual = target.Create(CLASS_NAME, "TestApp", reswFileContents);
            Assert.IsNotNull(actual);
        }
    }
}