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
    [Priority(0)]
    public void GenerateCodeDoesNotReturnNull()
    {
        Assert.IsNotNull(this.Actual);
    }

    [TestMethod]
    [Priority(1)]
    public void GeneratedCodeCompilesCleanly()
    {
        this.CompileGeneratedCode();

        Assert.IsFalse(this.CompilerResults.Errors.HasErrors, string.Join("\n", this.CompilerResults.Output.OfType<string>()));
        Assert.IsFalse(this.CompilerResults.Errors.HasWarnings, string.Join("\n", this.CompilerResults.Output.OfType<string>()));
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
            Assert.IsNull(this.GeneratedType);

            // Invoke compilation.
            var compilerParameters = GetCompilerParameters(this.Target.Provider);
            this.CompilerResults = this.Target.Provider.CompileAssemblyFromDom(
            compilerParameters, this.Target.CodeCompileUnit, GenerateClassesInConflictingNamespaces());

            Assert.IsNotNull(this.CompilerResults.CompiledAssembly);
            this.GeneratedType = this.CompilerResults.CompiledAssembly.GetType("TestApp.Resources");
            Assert.IsNotNull(this.GeneratedType);

            Debug.WriteLineIf(Debugger.IsAttached, $"Compiler returned {this.CompilerResults.NativeCompilerReturnValue}");
            Debug.WriteLineIf(Debugger.IsAttached, $"Output:\n{string.Join("\n", this.CompilerResults.Output.OfType<string>())}");
            Debug.WriteLineIf(Debugger.IsAttached, null);
            Debug.WriteLineIf(Debugger.IsAttached, $"Environment.CurrentDirectory     ={Environment.CurrentDirectory}");
            Debug.WriteLineIf(Debugger.IsAttached, $"CompilerResults.PathToAssembly   ={this.CompilerResults.PathToAssembly}");
            Debug.WriteLineIf(Debugger.IsAttached, $"CompilerResults.TempFiles.TempDir={this.CompilerResults.TempFiles.TempDir}");
            Debug.WriteLineIf(Debugger.IsAttached, $"CompilerResults.TempFiles.Count  ={this.CompilerResults.TempFiles.Count}");
            Debug.WriteLineIf(Debugger.IsAttached, $"CompilerResults.TempFiles        ={string.Join(", ", this.CompilerResults.TempFiles.OfType<string>())}");
            Debug.WriteLineIf(Debugger.IsAttached, $"CompilerResults.Errors.Count     ={this.CompilerResults.Errors.Count}");
            Debug.WriteLineIf(Debugger.IsAttached, $"CompilerResults.CompiledAssembly.Location={this.CompilerResults.CompiledAssembly.Location}");
        }
    }

    private static CodeCompileUnit GenerateClassesInConflictingNamespaces()
    {
        // If one of the following namespaces contain types, they would conflict with the System.*
        // and Windows.* types used in the generated code. This was fixed by prefixing these
        // namespace with Global. (for Visual Basic) or global:: (for C#).
        //
        // This method simply adds these "conflicting" namespaces to a CodeCompileUnit included
        // in the compilation of the generated code. This will ensure that any such global
        // namespace usage is specified with the appropriate global prefix. If such code does get
        // generated again, the compilation will fail, which will cause test failures.
        //
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
            CompilerOptions = string.Join(" ", GetCompilerOptions(provider).Select(
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

    private static IDictionary<string, string> GetCompilerOptions(CodeDomProvider provider)
    {
        var compilerOptions = new Dictionary<string, string>
        {
            { "lib"      , $"\"{Environment.ExpandEnvironmentVariables(@"%windir%\System32\WinMetadata")}\"" },
            { "optimize-", null },
        };

        // The VB compiler has slightly different names for some of its options
        if (provider is VBCodeProvider)
        {
            compilerOptions["libpath"] = compilerOptions["lib"];
            compilerOptions.Remove("lib");
        }

        return compilerOptions;
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
