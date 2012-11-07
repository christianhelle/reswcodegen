namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool
{
    public interface ICodeGenerator
    {
        IResourceParser ResourceParser { get; set; }
        string Namespace { get; set; }
        string GenerateCode();
    }
}
