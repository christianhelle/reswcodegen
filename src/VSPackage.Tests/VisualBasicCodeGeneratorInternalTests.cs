using System.Linq;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests;

[TestClass]
public sealed class VisualBasicCodeGeneratorInternalTests : CodeGeneratorTestsBase
{
    #region A static field (to avoid repeating the same work for each test)
    private static readonly StaticData s_staticData = new();
    #endregion

    public VisualBasicCodeGeneratorInternalTests()
        : base(s_staticData, TypeAttributes.NotPublic, new VBCodeProvider())
    {
    }

    [TestMethod]
    public void GeneratedCodeIsFriendClass()
    {
        CompileGeneratedCode();

        StringAssert.Contains(this.Actual, "Partial Friend NotInheritable Class");
        Assert.IsFalse(this.GeneratedType.IsNested);
        Assert.IsTrue(this.GeneratedType.IsNotPublic);
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
            var nameProperty = $"Public Shared ReadOnly Property {name}() As String";
            StringAssert.Contains(this.Actual, nameProperty);

            var propertyInfo = this.GeneratedType.GetProperty(name, BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(propertyInfo);
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

        foreach (var item in resourceItems.Where(item => !item.Name.Contains(".")))
            StringAssert.Contains(this.Actual, $"Localized resource similar to \"{item.Value}\"");
    }

    [TestMethod]
    public void ClassNameEqualsFileNameWithoutExtension()
    {
        StringAssert.Contains(this.Actual, "Class Resources");
    }

    [TestMethod]
    public void ResourceLoaderInitializedWithClassName()
    {
        StringAssert.Contains(this.Actual, "ResourceLoader.GetForCurrentView(currentAssemblyName + \"/Resources\")");
    }
}
