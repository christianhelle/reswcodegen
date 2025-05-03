using System;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Resources.resw")]
    public class StringExtensionTests
    {
        const string TEXT = "test";

        [TestMethod]
        public void ConvertToIntPtrDoesNotReturnZero()
        {
            var ptr = TEXT.ConvertToIntPtr(out uint _);
            Assert.AreNotEqual(IntPtr.Zero, ptr);
        }

        [TestMethod]
        public void ConvertToIntPtrReturnsStringLengthAsParameter()
        {
            TEXT.ConvertToIntPtr(out uint len);
            Assert.AreEqual(TEXT.Length, (int)len);
        }

        [TestMethod]
        public void ConvertToIntPtrConvertsCorrectString()
        {
            var ptr = TEXT.ConvertToIntPtr(out uint len);
            var str = Marshal.PtrToStringAnsi(ptr, (int) len);
            Assert.AreEqual(TEXT, str);
        }
    }
}
