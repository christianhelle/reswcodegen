namespace ReswCodeGen.Core
{
    public interface ICodeGenerator
    {
        string Namespace { get; set; }
        string ReswContent { get; set; }
        string GenerateCode();
    }
}
