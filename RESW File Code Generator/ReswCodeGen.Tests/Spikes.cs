using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReswCodeGen.Tests
{
    [TestClass]
    public class Spikes
    {
        [TestMethod]
        public void MarshalToIntPtrArray()
        {
            const string text = "test";
            var data = Encoding.UTF8.GetBytes(text);
            var pData = Marshal.AllocCoTaskMem(data.Length);
            Marshal.Copy(data, 0, pData, data.Length);

            var pDataArray = new IntPtr[data.Length];
            Marshal.Copy(pData, pDataArray, 0, data.Length);
            Marshal.FreeCoTaskMem(pData);
        }
    }
}