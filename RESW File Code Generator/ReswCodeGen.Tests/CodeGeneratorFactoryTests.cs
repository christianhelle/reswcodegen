using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Valid/Resources.resw")]
    public class CodeGeneratorFactoryTests
    {
        private string reswFileContents;
        private const string ClassName = "C:\\Test\\Resources\\Strings.resw";

        [TestInitialize]
        public void Initialize()
        {
            reswFileContents = File.ReadAllText("Resources.resw");
        }

        [TestMethod]
        public void CodeGeneratorFactoryReturnsValidInstance()
        {
            var target = new CodeGeneratorFactory();
            var actual = target.Create(ClassName, "TestApp", reswFileContents);
            Assert.IsNotNull(actual);
        }
    }
}