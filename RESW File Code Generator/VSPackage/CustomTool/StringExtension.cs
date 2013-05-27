using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    public static class StringExtension
    {
        public static IntPtr ConvertToIntPtr(this string code, out uint pcbOutput)
        {
            var data = Encoding.Default.GetBytes(code);
            pcbOutput = (uint)data.Length;

            var ptr = Marshal.StringToCoTaskMemAuto(code);
            return ptr;
        }
    }
}