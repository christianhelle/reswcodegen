using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Resources.resw")]
    public class ClassNameExtractorTests
    {
        private const string ClassName = "Resources.resw";

        [TestMethod]
        public void DoesNotReturnNull()
        {
            var actual = ClassNameExtractor.GetClassName(ClassName);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ReturnsFileNameWithoutExtension()
        {
            var actual = ClassNameExtractor.GetClassName(ClassName);
            Assert.AreEqual("Resources", actual);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ThrowsFileNotFoundException()
        {
            ClassNameExtractor.GetClassName("C:\\Test\\Resources\\Strings.resw");
        }
    }
}
