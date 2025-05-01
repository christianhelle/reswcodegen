using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Shell;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    [Guid("6C6AC14F-9B11-47C1-BC90-DFBFB89B1CB8")]
    [ComVisible(true)]
    [ProvideObject(typeof(ReswFileVisualBasicCodeGeneratorInternal))]
    [CodeGeneratorRegistration(typeof(ReswFileVisualBasicCodeGeneratorInternal),
                               "Visual Basic ResW File Code Generator (Internal class)",
                               Guids.ReswFileVisualBasicCodeGenerator,
                               GeneratesDesignTimeSource = true,
                               GeneratorRegKeyName = "InternalReswFileCodeGenerator")]
    public class ReswFileVisualBasicCodeGeneratorInternal : ReswFileCodeGenerator
    {
        public ReswFileVisualBasicCodeGeneratorInternal()
            : base(new VBCodeProvider(), TypeAttributes.NestedAssembly)
        {
        }

        protected override string GeneratorName => "Visual Basic ResW File Code Generator (Internal class)";

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".vb";
            return 0;
        }
    }
}
