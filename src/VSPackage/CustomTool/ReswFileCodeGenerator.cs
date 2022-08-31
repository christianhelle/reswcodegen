using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    [ComVisible(true)]
    public abstract class ReswFileCodeGenerator : IVsSingleFileGenerator
    {
        private readonly CodeDomProvider codeDomProvider;
        private readonly TypeAttributes? classAccessibility;

        protected abstract string GeneratorName { get; }

        protected ReswFileCodeGenerator(CodeDomProvider codeDomProvider, TypeAttributes? classAccessibility = null)
        {
            this.codeDomProvider = codeDomProvider;
            this.classAccessibility = classAccessibility;
        }

        #region IVsSingleFileGenerator Members

        public abstract int DefaultExtension(out string pbstrDefaultExtension);

        public virtual int Generate(string wszInputFilePath,
                                    string bstrInputFileContents,
                                    string wszDefaultNamespace,
                                    IntPtr[] rgbOutputFileContents,
                                    out uint pcbOutput,
                                    IVsGeneratorProgress pGenerateProgress)
        {
            try
            {
                var className = ClassNameExtractor.GetClassName(wszInputFilePath);
                var factory = new CodeGeneratorFactory();
                var codeGenerator = factory.Create(className, wszDefaultNamespace, bstrInputFileContents, codeDomProvider, classAccessibility);
                var code = codeGenerator.GenerateCode();

                rgbOutputFileContents[0] = code.ConvertToIntPtr(out pcbOutput);
            }
            catch (Exception e)
            {
                AppInsightsClient.Instance.TrackError(e);
                MessageBox.Show(e.Message, "Unable to generate code");
                throw;
            }
            finally
            {
                AppInsightsClient.Instance.TrackFeatureUsage(GeneratorName);
            }

            return 0;
        }

        #endregion
    }
}