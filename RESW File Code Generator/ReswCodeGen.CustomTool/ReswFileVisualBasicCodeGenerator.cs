using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.CustomTool
{
    [Guid("704DB723-AB03-4369-A2D7-0C857E96C94C")]
    [ComVisible(true)]
    public class ReswFileVisualBasicCodeGenerator : ReswFileCodeGenerator
    {
        public ReswFileVisualBasicCodeGenerator()
            : base(new VBCodeProvider())
        {
        }

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".vb";
            return 0;
        }
    }
}