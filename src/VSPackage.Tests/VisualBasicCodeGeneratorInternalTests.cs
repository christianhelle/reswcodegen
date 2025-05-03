using System.IO;
using System.Linq;
using System.Reflection;
using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Resources.resw")]
    public class VisualBasicCodeGeneratorInternalTests : CodeGeneratorTestsBase
    {
        public VisualBasicCodeGeneratorInternalTests()
            : base(TypeAttributes.NestedAssembly, new VBCodeProvider())
        {
        }

        [TestMethod]
        public void GeneratedCodeIsFriendClass()
        {
            CompileGeneratedCode();

            Assert.Contains("Partial Friend NotInheritable Class", Actual);
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
                var name = $"GetString(\"{item.Name.Replace(".", "/")}\")";
                Assert.Contains(name, Actual);
            }
        }

        [TestMethod]
        public void GeneratedCodePropertiesContainsCommentsSimilarToValuesDefinedInResources()
        {
            var resourceItems = Target.ResourceParser.Parse();

            foreach (var item in resourceItems.Where(item => !item.Name.Contains(".")))
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
}
