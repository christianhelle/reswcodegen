using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/ResourcesWithoutValues.resw")]
    [DeploymentItem("Resources/ResourcesWithoutComments.resw")]
    public class ResourceParserErrorHandlingTests
    {
        [TestMethod]
        public void ParseHandlesMissingResourceItemValues()
        {
            var reswFileContents = File.ReadAllText("ResourcesWithoutValues.resw");
            var target = new ResourceParser(reswFileContents);
            var actual = target.Parse();

            Assert.IsNotNull(actual);
            CollectionAssert.AllItemsAreNotNull(actual);
        }

        [TestMethod]
        public void ParseWithMissingResourceItemValuesSetsValueNull()
        {
            var reswFileContents = File.ReadAllText("ResourcesWithoutValues.resw");
            var target = new ResourceParser(reswFileContents);
            var actual = target.Parse();

            Assert.IsNotNull(actual);
            CollectionAssert.AllItemsAreNotNull(actual);

            foreach (var item in actual)
                Assert.IsTrue(string.IsNullOrEmpty(item.Value));
        }

        [TestMethod]
        public void ParseHandlesMissingResourceItemComments()
        {
            var reswFileContents = File.ReadAllText("ResourcesWithoutComments.resw");
            var target = new ResourceParser(reswFileContents);
            var actual = target.Parse();

            Assert.IsNotNull(actual);
            CollectionAssert.AllItemsAreNotNull(actual);
        }

        [TestMethod]
        public void ParseWithMissingResourceItemCommentsSetsCommentsNull()
        {
            var reswFileContents = File.ReadAllText("ResourcesWithoutComments.resw");
            var target = new ResourceParser(reswFileContents);
            var actual = target.Parse();

            Assert.IsNotNull(actual);
            CollectionAssert.AllItemsAreNotNull(actual);

            foreach (var item in actual)
                Assert.IsTrue(string.IsNullOrEmpty(item.Comment));
        }
    }
}