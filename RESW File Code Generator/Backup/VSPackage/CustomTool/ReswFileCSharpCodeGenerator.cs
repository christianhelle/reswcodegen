using System.Runtime.InteropServices;
using Microsoft.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    [Guid("98983F6D-BC77-46AC-BA5A-8D9E8763F0D2")]
    [ComVisible(true)]
    public class ReswFileCSharpCodeGenerator : ReswFileCodeGenerator
    {
        public ReswFileCSharpCodeGenerator()
            : base(new CSharpCodeProvider())
        {
        }

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".cs";
            return 0;
        }
    }
}