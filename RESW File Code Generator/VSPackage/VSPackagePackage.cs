using ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage.CustomTool;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Design.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidVSPackagePkgString)]
    [DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\11.0")]
    [ProvideObject(typeof(ReswFileCSharpCodeGenerator))]
    [ProvideObject(typeof(ReswFileVisualBasicCodeGenerator))]
    [ProvideGeneratorAttribute(typeof(ReswFileCSharpCodeGenerator), "ReswFileCodeGenerator", "ResW File Code Generator for C#", "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", true)] // csharp
    [ProvideGeneratorAttribute(typeof(ReswFileVisualBasicCodeGenerator), "ReswFileCodeGenerator", "ResW File Code Generator for VB", "{164B10B9-B200-11D0-8C61-00A0C91E29D5}", true)] // visual basic
    [ProvideGeneratorAttribute(typeof(ReswFileCSharpCodeGeneratorInternal), "ReswFileCodeGeneratorInternal", "ResW File Code Generator for C#", "{151F74CA-404D-4188-B994-D7683C32ACF4}", true)] // csharp
    [ProvideGeneratorAttribute(typeof(ReswFileVisualBasicCodeGeneratorInternal), "ReswFileCodeGeneratorInternal", "ResW File Code Generator for VB", "{6C6AC14F-9B11-47C1-BC90-DFBFB89B1CB8}", true)] // visual basic
    public sealed class VSPackagePackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public VSPackagePackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

        }
        #endregion

    }
}
