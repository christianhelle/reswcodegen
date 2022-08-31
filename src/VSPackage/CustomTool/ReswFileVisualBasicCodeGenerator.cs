using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Shell;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    [Guid("92DFB543-7138-419B-99D9-90CC77607671")]
    [ComVisible(true)]
    [ProvideObject(typeof(ReswFileVisualBasicCodeGenerator))]
    [CodeGeneratorRegistration(typeof(ReswFileVisualBasicCodeGenerator),
                               "Visual Basic ResW File Code Generator",
                               Guids.ReswFileVisualBasicCodeGenerator,
                               GeneratesDesignTimeSource = true,
                               GeneratorRegKeyName = "ReswFileCodeGenerator")]
    public class ReswFileVisualBasicCodeGenerator : ReswFileCodeGenerator
    {
        public ReswFileVisualBasicCodeGenerator()
            : base(new VBCodeProvider())
        {
        }

        protected override string GeneratorName => "Visual Basic ResW File Code Generator";

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".vb";
            return 0;
        }
    }
}