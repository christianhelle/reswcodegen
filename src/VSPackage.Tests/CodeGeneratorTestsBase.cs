using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool.Tests;
using ICodeGenerator = VSPackage.CustomTool.ICodeGenerator;

[DeploymentItem("Resources/Resources.resw")]
public abstract class CodeGeneratorTestsBase
{
    protected const string FILE_PATH = "Resources.resw";

    private readonly StaticData staticData;

    #region Instance Properties that access the derived class's static data

    protected TypeAttributes? ClassAccessibility
    {
        get => staticData.ClassAccessibility;
        set => staticData.ClassAccessibility = value;
    }

    protected CodeDomProvider Provider
    {
        get => staticData.Provider;
        set => staticData.Provider = value;
    }

    protected string ReswFileContents
    {
        get => staticData.ReswFileContents;
        set => staticData.ReswFileContents = value;
    }

    protected string Actual
    {
        get => staticData.Actual;
        set => staticData.Actual = value;
    }

    protected ICodeGenerator Target
    {
        get => staticData.Target;
        set => staticData.Target = value;
    }

    protected CompilerResults CompilerResults
    {
        get => staticData.CompilerResults;
        set => staticData.CompilerResults = value;
    }

    protected Type GeneratedType
    {
        get => staticData.GeneratedType;
        set => staticData.GeneratedType = value;
    }

    #endregion

    protected CodeGeneratorTestsBase(StaticData staticData, TypeAttributes? classAccessibility = null, CodeDomProvider provider = null)
    {
        lock (staticData)
        {
            this.staticData = staticData;

            this.ClassAccessibility ??= classAccessibility;
            this.Provider ??= provider;

            this.ReswFileContents ??= File.ReadAllText(FILE_PATH);

            this.Target ??= new CodeGeneratorFactory().Create(FILE_PATH.Replace(".resw", string.Empty), "TestApp", this.ReswFileContents, Provider, classAccessibility);
            this.Provider = this.Target.Provider;
            this.Actual ??= this.Target.GenerateCode();
        }
    }

    [TestMethod]
    public void GenerateCodeDoesNotReturnNull()
    {
        Assert.IsNotNull(this.Actual);
    }

    [TestMethod]
    public void GeneratedCodeCompilesCleanly()
    {
        this.CompileGeneratedCode();

        Assert.IsFalse(this.CompilerResults.Errors.HasErrors, string.Join("\n", this.CompilerResults.Output.OfType<string>()));
        Assert.IsFalse(this.CompilerResults.Errors.HasWarnings, string.Join("\n", this.CompilerResults.Output.OfType<string>()));
        Assert.IsNotNull(this.GeneratedType);
    }

    [TestMethod]
    public void ContainsProjectUrl()
    {
        StringAssert.Contains(this.Actual, "http://bit.ly/reswcodegen");
    }

    protected void CompileGeneratedCode()
    {
        lock (this.staticData)
        {
            if (this.CompilerResults is not null)
            {
                Assert.IsNotNull(this.GeneratedType);
                return;
            }

            Assert.IsNotNull(this.Target);
            Assert.IsNotNull(this.Target.Provider);

            // Invoke compilation.
            var compilerParameters = GetCompilerParameters(this.Target.Provider);
            this.CompilerResults = this.Target.Provider.CompileAssemblyFromDom(
                compilerParameters, this.Target.CodeCompileUnit, GenerateClassesInConflictingNamespaces());

            Debug.WriteLine($"Compiler returned {this.CompilerResults.NativeCompilerReturnValue}");
            Debug.WriteLine($"Output:\n{string.Join("\n", this.CompilerResults.Output.OfType<string>())}");
            Debug.WriteLine(null);
            Debug.WriteLine($"Environment.CurrentDirectory     ={Environment.CurrentDirectory}");
            Debug.WriteLine($"CompilerResults.PathToAssembly   ={this.CompilerResults.PathToAssembly}");
            Debug.WriteLine($"CompilerResults.TempFiles.TempDir={this.CompilerResults.TempFiles.TempDir}");
            Debug.WriteLine($"CompilerResults.TempFiles.Count  ={this.CompilerResults.TempFiles.Count}");
            Debug.WriteLine($"CompilerResults.TempFiles        ={string.Join(", ", this.CompilerResults.TempFiles.OfType<string>())}");
            Debug.WriteLine($"CompilerResults.Errors.Count     ={this.CompilerResults.Errors.Count}");
            Debug.WriteLine($"CompilerResults.CompiledAssembly.Location={this.CompilerResults.CompiledAssembly.Location}");

            this.GeneratedType = this.CompilerResults.CompiledAssembly.GetType("TestApp.Resources");
        }
    }

    private static CodeCompileUnit GenerateClassesInConflictingNamespaces()
    {
        return new CodeCompileUnit
        {
            Namespaces =
            {
                new CodeNamespace("TestApp.Windows.Library") { Types = { new CodeTypeDeclaration("Class1") { IsClass = true, TypeAttributes = TypeAttributes.Sealed | TypeAttributes.Public, } } },
                new CodeNamespace("TestApp.System.Library") { Types = { new CodeTypeDeclaration("Class2") { IsClass = true, TypeAttributes = TypeAttributes.Sealed | TypeAttributes.Public, } } }
            }
        };
    }

    private CompilerParameters GetCompilerParameters(CodeDomProvider provider)
    {
        var compilerOptions = new Dictionary<string, string>
        {
            { "lib"      , $"\"{Environment.ExpandEnvironmentVariables(@"%windir%\System32\WinMetadata")}\"" },
            { "optimize-", null },
        };

        // The VB compiler has slightly different names for some of its options
        if (provider.GetType() == typeof(VBCodeProvider))
        {
            compilerOptions["libpath"] = compilerOptions["lib"];
            compilerOptions.Remove("lib");
        }

        var compilerParameters = new CompilerParameters
        {
            // Generate a class library instead of an executable.
            GenerateExecutable = false,

            // Set the assembly file name to generate.
            // OutputAssembly = $"GeneratedResources.{this.GetType().Name}.dll",

            // Save the assembly as a non-file.
            GenerateInMemory = true,

            // Set the level at which the compiler
            // should start displaying warnings.
            WarningLevel = 4,

            // Set whether to treat all warnings as errors.
            TreatWarningsAsErrors = true,

            // Set a temporary files collection.
            // The TempFileCollection stores the temporary files
            // generated during a build in the current directory,
            // and does not delete them after compilation.
            TempFiles = new TempFileCollection(".", true),

            // Set additional library paths for finding referenced assemblies
            CompilerOptions = string.Join(" ", compilerOptions.Select(
                kv => string.IsNullOrWhiteSpace(kv.Value) ? $"-{kv.Key}" : $"-{kv.Key}:{kv.Value}")),

            // Assemblies referenced by the generated code
            ReferencedAssemblies =
            {
                "System.dll",
                "System.Runtime.dll",
                "Windows.ApplicationModel.winmd",
                "Windows.Foundation.winmd",
                "Windows.UI.winmd",
            },
        };

        return compilerParameters;
    }

    protected sealed class StaticData
    {
        public TypeAttributes? ClassAccessibility { get; set; }
        public CodeDomProvider Provider { get; set; }
        public string ReswFileContents { get; set; }
        public string Actual { get; set; }
        public ICodeGenerator Target { get; set; }
        public CompilerResults CompilerResults { get; set; }
        public Type GeneratedType { get; set; }
    }
}
