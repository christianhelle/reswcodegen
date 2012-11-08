namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool
{
    public class CodeGeneratorFactory
    {
        public ICodeGenerator Create(string defaultNamespace, string inputFileContents)
        {
            return new CodeDomCodeGenerator(new ResourceParser(inputFileContents), defaultNamespace);
        }
    }
}
