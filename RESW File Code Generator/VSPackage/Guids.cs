// Guids.cs
// MUST match guids.h

using System;

namespace ChristianHelle.DeveloperTools.CodeGenerators.Resw.VSPackage
{
    static class GuidList
    {
        public const string guidVSPackagePkgString = "6a4c1726-440f-4b2d-a2e5-711277da6099";
        public const string guidVSPackageCmdSetString = "fb40dc0b-2f75-404c-ba4e-dc1b90c41941";

        public static readonly Guid guidVSPackageCmdSet = new Guid(guidVSPackageCmdSetString);
    };
}