﻿using System.IO;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests
{
    [TestClass]
    [DeploymentItem("Resources/Resources.resw")]
    public class VisualBasicCodeGeneratorTests
    {
        private const string FilePath = "Resources.resw";
        private string actual;
        private string reswFileContents;
        private ICodeGenerator target;

        [TestInitialize]
        public void Initialize()
        {
            reswFileContents = File.ReadAllText(FilePath);

            target = new CodeGeneratorFactory().Create(FilePath.Replace(".resw", string.Empty), "TestApp", reswFileContents, new VBCodeProvider());
            actual = target.GenerateCode();
        }

        [TestMethod]
        public void GenerateCodeDoesNotReturnNull()
        {
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void GeneratedCodeContainsPropertiesDefinedInResources()
        {
            var resourceItems = target.ResourceParser.Parse();

            foreach (var item in resourceItems.Where(item => !item.Name.Contains(".")))
                Assert.IsTrue(actual.Contains("Public Shared ReadOnly Property " + item.Name + "() As String"));
        }

        [TestMethod]
        public void GeneratedCodePropertiesContainsCommentsSimilarToValuesDefinedInResources()
        {
            var resourceItems = target.ResourceParser.Parse();

            foreach (var item in resourceItems.Where(item => !item.Name.Contains(".")))
                Assert.IsTrue(actual.Contains("Localized resource similar to \"" + item.Value + "\""));
        }

        [TestMethod]
        public void ClassNameEqualsFileNameWithoutExtension()
        {
            Assert.IsTrue(actual.Contains("Class Resources"));
        }

        [TestMethod]
        public void ResourceLoaderInitializedWithClassName()
        {
            Assert.IsTrue(actual.Contains("New ResourceLoader(currentAssemblyName + \"/Resources\")"));
        }
    }
}