using System;
using System.IO;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Resources.resw")]
    public class StringExtensionTests
    {
        private const string FILE_PATH = "Resources.resw";

        [TestMethod]
        public void LineEndingsAreSame()
        {
            var reswFileContents = File.ReadAllText(FILE_PATH);
            var target = new CodeGeneratorFactory().Create(FILE_PATH.Replace(".resw", string.Empty), "TestApp", reswFileContents);
            var expected = target.GenerateCode();

            uint length;
            var ptr = expected.ConvertToIntPtr(out length);
            var actual = Marshal.PtrToStringAnsi(ptr);
            Assert.AreEqual(expected, actual);
        }
    }
}
