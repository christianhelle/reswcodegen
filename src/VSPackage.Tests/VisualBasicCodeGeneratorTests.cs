using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests;

[TestClass]
public sealed class VisualBasicCodeGeneratorTests : CodeGeneratorTestsBase
{
    #region A static field (to avoid repeating the same work for each test)
    private static readonly StaticData s_staticData = new();
    #endregion

    public VisualBasicCodeGeneratorTests()
        : base(s_staticData, TypeAttributes.Public, new VBCodeProvider())
    {
    }

    [TestMethod]
    public void GeneratedCodeIsPublicClass()
    {
        CompileGeneratedCode();

        StringAssert.Contains(Actual, "Partial Public NotInheritable Class");
        Assert.IsFalse(GeneratedType.IsNested);
        Assert.IsTrue(GeneratedType.IsPublic);
        Assert.IsTrue(GeneratedType.IsSealed);
        Assert.IsTrue(GeneratedType.IsClass);
    }

    [TestMethod]
    public void GeneratedCodeContainsPropertiesDefinedInResources()
    {
        CompileGeneratedCode();

        var resourceItems = Target.ResourceParser.Parse();

        foreach (var item in resourceItems)
        {
            var name = item.Name.Replace(".", "_");
            var nameProperty = $"Public Shared ReadOnly Property {name}() As String";
            StringAssert.Contains(Actual, nameProperty);
            Assert.IsNotNull(GeneratedType.GetProperty(name, BindingFlags.Public | BindingFlags.Static));

            var propertyInfo = GeneratedType.GetProperty(name, BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(propertyInfo);
            Assert.IsTrue(propertyInfo.PropertyType == typeof(string));
        }
    }

    [TestMethod]
    public void GeneratedCodeReplacesDottedKeysWithForwardSlash()
    {
        var resourceItems = Target.ResourceParser.Parse();

        foreach (var item in resourceItems)
        {
            var value = $"GetString(\"{item.Name.Replace(".", "/")}\")";
            StringAssert.Contains(Actual, value);
        }
    }

    [TestMethod]
    public void GeneratedCodePropertiesContainsCommentsSimilarToValuesDefinedInResources()
    {
        var resourceItems = Target.ResourceParser.Parse();

        foreach (var item in resourceItems)
            StringAssert.Contains(Actual, "Localized resource similar to \"" + item.Value + "\"");
    }

    [TestMethod]
    public void ClassNameEqualsFileNameWithoutExtension()
    {
        StringAssert.Contains(Actual, "Class Resources");
    }

    [TestMethod]
    public void ResourceLoaderInitializedWithClassName()
    {
        StringAssert.Contains(Actual, "ResourceLoader.GetForCurrentView(currentAssemblyName + \"/Resources\")");
    }
}
