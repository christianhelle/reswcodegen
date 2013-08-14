using System.CodeDom;
using System.Runtime.InteropServices;
using Microsoft.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    [Guid("151F74CA-404D-4188-B994-D7683C32ACF4")]
    [ComVisible(true)]
    public class ReswFileCSharpCodeGeneratorInternal : ReswFileCodeGenerator
    {
        public ReswFileCSharpCodeGeneratorInternal()
            : base(new CSharpCodeProvider(), MemberAttributes.Assembly)
        {
        }

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".cs";
            return 0;
        }
    }
}