using System;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage
{
    internal static class Guids
    {
        public const string guidVSPackagePkgString = "6a4c1726-440f-4b2d-a2e5-711277da6099";
        public const string guidVSPackageCmdSetString = "fb40dc0b-2f75-404c-ba4e-dc1b90c41941";

        public const string ReswFileCSharpCodeGenerator = "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}";
        public const string ReswFileVisualBasicCodeGenerator = "{164B10B9-B200-11D0-8C61-00A0C91E29D5}";

        public static readonly Guid guidVSPackageCmdSet = new Guid(guidVSPackageCmdSetString);
    };
}
