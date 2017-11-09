using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VSPackage_IntegrationTests
{
    [TestClass]
    public class VisualStudioHelperTests
    {
        [TestMethod]
        public void GetVersionTests()
        {
            var actual = VisualStudioHelper.GetVersion();
            Assert.AreNotEqual(VisualStudioVersion.Unknown, actual);
        }
    }
}
