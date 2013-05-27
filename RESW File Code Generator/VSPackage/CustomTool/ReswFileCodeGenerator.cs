using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    [ComVisible(true)]
    public abstract class ReswFileCodeGenerator : IVsSingleFileGenerator
    {
        private readonly CodeDomProvider codeDomProvider;

        protected ReswFileCodeGenerator(CodeDomProvider codeDomProvider)
        {
            this.codeDomProvider = codeDomProvider;
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
                var codeGenerator = factory.Create(className, wszDefaultNamespace, bstrInputFileContents, codeDomProvider);
                var code = codeGenerator.GenerateCode();

                rgbOutputFileContents[0] = code.ConvertToIntPtr(out pcbOutput);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Unable to generate code");
                throw;
            }

            return 0;
        }

        #endregion
    }
}