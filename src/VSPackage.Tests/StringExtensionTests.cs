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
            uint len;
            var ptr = TEXT.ConvertToIntPtr(out len);
            Assert.AreNotEqual(IntPtr.Zero, ptr);
        }

        [TestMethod]
        public void ConvertToIntPtrReturnsStringLengthAsParameter()
        {
            uint len;
            TEXT.ConvertToIntPtr(out len);
            Assert.AreEqual(TEXT.Length, (int)len);
        }

        [TestMethod]
        public void ConvertToIntPtrConvertsCorrectString()
        {
            uint len;
            var ptr = TEXT.ConvertToIntPtr(out len);
            var str = Marshal.PtrToStringAnsi(ptr, (int) len);
            Assert.AreEqual(TEXT, str);
        }
    }
}
