using System.Reflection;
using Microsoft.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests;

[TestClass]
public sealed class CSharpCodeGeneratorTests : CodeGeneratorTestsBase
{
    #region A static field (to avoid repeating the same work for each test)
    private static readonly StaticData s_staticData = new();
    #endregion

    public CSharpCodeGeneratorTests()
        : base(s_staticData, TypeAttributes.Public, new CSharpCodeProvider())
    {
    }

    [TestMethod]
    public void GeneratedCodeIsAPublicClass()
    {
        CompileGeneratedCode();

        StringAssert.Contains(this.Actual, "public sealed partial class");
        Assert.IsFalse(this.GeneratedType.IsNested);
        Assert.IsTrue(this.GeneratedType.IsPublic);
        Assert.IsTrue(this.GeneratedType.IsSealed);
        Assert.IsTrue(this.GeneratedType.IsClass);
    }

    [TestMethod]
    public void GeneratedCodeContainsPropertiesDefinedInResources()
    {
        CompileGeneratedCode();

        var resourceItems = this.Target.ResourceParser.Parse();

        foreach (var item in resourceItems)
        {
            var name = item.Name.Replace(".", "_");
            var nameProperty = $"public static string {name}";
            StringAssert.Contains(this.Actual, nameProperty);
            Assert.IsTrue(this.GeneratedType.GetProperty(name, BindingFlags.Public | BindingFlags.Static) != null);

            var propertyInfo = this.GeneratedType.GetProperty(name, BindingFlags.Public | BindingFlags.Static);
            Assert.IsTrue(propertyInfo != null);
            Assert.IsTrue(propertyInfo.PropertyType == typeof(string));
        }
    }

    [TestMethod]
    public void GeneratedCodeReplacesDottedKeysWithForwardSlash()
    {
        var resourceItems = this.Target.ResourceParser.Parse();

        foreach (var item in resourceItems)
        {
            var name = $"GetString(\"{item.Name.Replace(".", "/")}\")";
            StringAssert.Contains(this.Actual, name);
        }
    }

    [TestMethod]
    public void GeneratedCodePropertiesContainsCommentsSimilarToValuesDefinedInResources()
    {
        var resourceItems = this.Target.ResourceParser.Parse();

        foreach (var item in resourceItems)
            StringAssert.Contains(this.Actual, "Localized resource similar to \"" + item.Value + "\"");
    }

    [TestMethod]
    public void ClassNameEqualsFileNameWithoutExtension()
    {
        StringAssert.Contains(this.Actual, "class Resources");
    }

    [TestMethod]
    public void ResourceLoaderInitializedWithClassName()
    {
        StringAssert.Contains(this.Actual, "ResourceLoader.GetForCurrentView(currentAssemblyName + \"/Resources\");");
    }
}
