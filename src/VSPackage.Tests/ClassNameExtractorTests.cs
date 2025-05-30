﻿using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests;

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
    public void ThrowsFileNotFoundException()
    {
        Assert.ThrowsExactly<FileNotFoundException>(
            () => ClassNameExtractor.GetClassName(@"C:\Test\Resources\Strings.resw"));
    }
}
