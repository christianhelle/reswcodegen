using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Resources.resw")]
    public class CodeGeneratorFactoryTests
    {
        private string reswFileContents;

        [TestInitialize]
        public void Initialize()
        {
            reswFileContents = File.ReadAllText("Resources.resw");
        }

        [TestMethod]
        public void CodeGeneratorFactoryReturnsValidInstance()
        {
            var target = new CodeGeneratorFactory();
            var actual = target.Create("TestApp", reswFileContents);
            Assert.IsNotNull(actual);
        }
    }
}