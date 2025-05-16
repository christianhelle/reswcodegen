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

        Assert.Contains("Partial Public NotInheritable Class", Actual);
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
            Assert.Contains(nameProperty, Actual);
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
            Assert.Contains(value, Actual);
        }
    }

    [TestMethod]
    public void GeneratedCodePropertiesContainsCommentsSimilarToValuesDefinedInResources()
    {
        var resourceItems = Target.ResourceParser.Parse();

        foreach (var item in resourceItems)
            Assert.Contains("Localized resource similar to \"" + item.Value + "\"", Actual);
    }

    [TestMethod]
    public void ClassNameEqualsFileNameWithoutExtension()
    {
        Assert.Contains("Class Resources", Actual);
    }

    [TestMethod]
    public void ResourceLoaderInitializedWithClassName()
    {
        Assert.Contains("ResourceLoader.GetForCurrentView(currentAssemblyName + \"/Resources\")", Actual);
    }
}
