using System.Runtime.InteropServices;
using Microsoft.CSharp;
using Microsoft.VisualStudio.Shell;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    [Guid("98983F6D-BC77-46AC-BA5A-8D9E8763F0D2")]
    [ComVisible(true)]
    [ProvideObject(typeof(ReswFileCSharpCodeGenerator))]
    [CodeGeneratorRegistration(typeof(ReswFileCSharpCodeGenerator),
                               "C# ResW File Code Generator",
                               Guids.ReswFileCSharpCodeGenerator,
                               GeneratesDesignTimeSource = true,
                               GeneratorRegKeyName = "ReswFileCodeGenerator")]
    public class ReswFileCSharpCodeGenerator : ReswFileCodeGenerator
    {
        public ReswFileCSharpCodeGenerator()
            : base(new CSharpCodeProvider())
        {
            AppInsightsClient.Instance.TrackFeatureUsage("C# ResW File Code Generator");
        }

        public override int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".cs";
            return 0;
        }
    }
}