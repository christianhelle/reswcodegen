using System;
using System.Runtime.InteropServices;
using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void LineEndingsAreSame()
        {
            uint length;
            var target = string.Format("First line{0}Second line{0}", Environment.NewLine);
            var ptr = target.ConvertToIntPtr(out length);
            var actual = Marshal.PtrToStringAuto(ptr);
            Assert.AreEqual(target, actual);
        }
    }
}
