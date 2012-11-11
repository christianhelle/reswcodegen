using System.CodeDom.Compiler;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool
{
    public class CodeGeneratorFactory
    {
        public ICodeGenerator Create(string defaultNamespace, string inputFileContents, CodeDomProvider codeDomProvider = null)
        {
            return new CodeDomCodeGenerator(new ResourceParser(inputFileContents), defaultNamespace, codeDomProvider);
        }
    }
}
