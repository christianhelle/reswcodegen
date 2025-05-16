using System.CodeDom;
using System.CodeDom.Compiler;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    public abstract class CodeGenerator : ICodeGenerator
    {
        protected CodeGenerator(IResourceParser resourceParser, string defaultNamespace)
        {
            ResourceParser = resourceParser;
            Namespace = defaultNamespace;
        }

        public IResourceParser ResourceParser { get; private set; }

        public string Namespace { get; private set; }

        public abstract string GenerateCode();

        public abstract CodeCompileUnit CodeCompileUnit { get; }

        public abstract CodeDomProvider Provider { get; }
    }
}
