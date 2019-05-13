using System;
using System.CodeDom.Compiler;
using System.Reflection;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool
{
    public class CodeGeneratorFactory
    {
        public ICodeGenerator Create(string className, string defaultNamespace, string inputFileContents, CodeDomProvider codeDomProvider = null, TypeAttributes? classAccessibility = null)
        {
            return new CodeDomCodeGenerator(new ResourceParser(inputFileContents), className, defaultNamespace, codeDomProvider, classAccessibility, VisualStudioHelper.GetVersion());
        }
    }

    public static class VisualStudioHelper
    {
        public static VisualStudioVersion GetVersion()
        {
            var dte = Package.GetGlobalService(typeof(SDTE)) as DTE;
            var vsVersion = VisualStudioVersion.VS2012;
            if (dte != null)
            {
                Version dteVersion;
                if (Version.TryParse(dte.Version, out dteVersion) && dteVersion >= new Version(12, 0))
                    vsVersion = VisualStudioVersion.VS2013;
            }
            return vsVersion;
        }
    }
}
