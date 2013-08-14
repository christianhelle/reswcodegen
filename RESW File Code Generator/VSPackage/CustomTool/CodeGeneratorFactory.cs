using System.CodeDom;
using System.CodeDom.Compiler;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    public class CodeGeneratorFactory
    {
        public ICodeGenerator Create(string className, string defaultNamespace, string inputFileContents, CodeDomProvider codeDomProvider = null, MemberAttributes? classAccessibility = null)
        {
            return new CodeDomCodeGenerator(new ResourceParser(inputFileContents), className, defaultNamespace, codeDomProvider, classAccessibility);
        }
    }
}
