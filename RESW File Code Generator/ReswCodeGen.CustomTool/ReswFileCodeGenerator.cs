using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell.Interop;
using ReswCodeGen.Core;

namespace ReswCodeGen.CustomTool
{
    [Guid("98983F6D-BC77-46AC-BA5A-8D9E8763F0D2")]
    [ComVisible(true)]
    public class ReswFileCodeGenerator : IVsSingleFileGenerator
    {
        #region IVsSingleFileGenerator Members

        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".cs";
            return 0;
        }

        public int Generate(string wszInputFilePath,
                            string bstrInputFileContents,
                            string wszDefaultNamespace,
                            IntPtr[] rgbOutputFileContents,
                            out uint pcbOutput,
                            IVsGeneratorProgress pGenerateProgress)
        {
            try
            {
                var factory = new CodeGeneratorFactory();
                var codeGenerator = factory.Create(wszDefaultNamespace, bstrInputFileContents);
                var code = codeGenerator.GenerateCode();

                var data = Encoding.UTF8.GetBytes(code);
                
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(data.Length);
                Marshal.Copy(data, 0, rgbOutputFileContents[0], data.Length);

                pcbOutput = (uint)data.Length;
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