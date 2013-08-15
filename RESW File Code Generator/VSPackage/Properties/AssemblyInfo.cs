using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ResW File Code Generator Custom Tool")]
[assembly: AssemblyDescription("A Visual Studio 2012 Custom Tool for generating a strongly typed helper class for accessing localized resources from a .ResW file.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ResW File Code Generator Custom Tool")]
[assembly: AssemblyCopyright("Copyright © Christian Resma Helle 2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]   
[assembly: ComVisible(false)]     
[assembly: CLSCompliant(false)]
[assembly: NeutralResourcesLanguage("en-US")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion("1.1.0.*")]

[assembly: InternalsVisibleTo("VSPackage_IntegrationTests, PublicKey=002400000480000094000000060200000024000052534131000400000100010045bd05a5b24c74b83f069df52e138682c2fb8b1ee65cb9d351fd7e8d622308aeb588b2a2975d75da4fe8a392510528baa26a3317809ec064d3cc852f0df94752ad2228d9f2a048ee2858c1e9d505b05f7fb4ede02f34154f75ea50445741c84f5ab1c814358f5fd6a6f08bb3d94284cff0524d5ca9d888c8648bcbc7a1e0a3c8")]
[assembly: InternalsVisibleTo("VSPackage_UnitTests, PublicKey=002400000480000094000000060200000024000052534131000400000100010045bd05a5b24c74b83f069df52e138682c2fb8b1ee65cb9d351fd7e8d622308aeb588b2a2975d75da4fe8a392510528baa26a3317809ec064d3cc852f0df94752ad2228d9f2a048ee2858c1e9d505b05f7fb4ede02f34154f75ea50445741c84f5ab1c814358f5fd6a6f08bb3d94284cff0524d5ca9d888c8648bcbc7a1e0a3c8")]
