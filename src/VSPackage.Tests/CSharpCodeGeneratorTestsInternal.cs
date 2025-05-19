using System.Linq;
using System.Reflection;
using Microsoft.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests;

[TestClass]
public sealed class CSharpCodeGeneratorTestsInternal : CodeGeneratorTestsBase
{
    #region A static field (to avoid repeating the same work for each test)
    private static readonly StaticData s_staticData = new();
    #endregion

    public CSharpCodeGeneratorTestsInternal()
        : base(s_staticData, TypeAttributes.NotPublic, new CSharpCodeProvider())
    {
    }

    [TestMethod]
    public void GeneratedCodeIsAnInternalClass()
    {
        CompileGeneratedCode();

        StringAssert.Contains(Actual, "internal sealed partial class");
        Assert.IsFalse(GeneratedType.IsNested);
        Assert.IsTrue(GeneratedType.IsNotPublic);
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
            var nameProperty = $"public static string {name}";
            StringAssert.Contains(Actual, nameProperty);

            var propertyInfo = GeneratedType.GetProperty(name, BindingFlags.Public | BindingFlags.Static);
            Assert.IsTrue(propertyInfo != null);
            Assert.IsTrue(propertyInfo.PropertyType == typeof(string));
        }
    }

    [TestMethod]
    public void GeneratedCodePropertiesContainsCommentsSimilarToValuesDefinedInResources()
    {
        var resourceItems = Target.ResourceParser.Parse();

        foreach (var item in resourceItems.Where(item => !item.Name.Contains(".")))
            StringAssert.Contains(Actual, "Localized resource similar to \"" + item.Value + "\"");
    }

    [TestMethod]
    public void ClassNameEqualsFileNameWithoutExtension()
    {
        StringAssert.Contains(Actual, "class Resources");
    }

    [TestMethod]
    public void ResourceLoaderInitializedWithClassName()
    {
        StringAssert.Contains(Actual, "ResourceLoader.GetForCurrentView(currentAssemblyName + \"/Resources\");");
    }
}
