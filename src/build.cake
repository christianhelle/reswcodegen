#tool "nuget:?package=Microsoft.TestPlatform&version=17.14.0"

var target = Argument("target", "Default");
FilePath solutionPath = File("./ReswCodeGen.sln");
bool isRunningOnWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);

Information($"Running on Windows: {isRunningOnWindows}");

Task("Clean")
    .Does(() =>
{
    // Clean directories.
    CleanDirectory("./artifacts");
    CleanDirectories("./**/bin/**");
    CleanDirectories("./packages/");
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    Information("Restoring solution...");
    DotNetRestore(solutionPath.ToString());
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        Information("Building solution...");
        DotNetBuild(solutionPath.ToString(), new DotNetBuildSettings {
            Configuration = "Release",
            NoRestore = true
        });
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetTest(solutionPath.ToString(), new DotNetTestSettings {
        Configuration = "Release",
        NoRestore = true,
        NoBuild = true
    });
});

Task("Post-Build")
    .IsDependentOn("Build")
    .Does(() => {
        EnsureDirectoryExists("./Artifacts");
        if (isRunningOnWindows) {
            Information("Copying VSIX files to Artifacts directory");
            CopyFiles("./VSPackage/bin/Release/**/ReswFileCodeGenerator.vsix", "./Artifacts/");
        } else {
            Warning("Skipping VSIX file copy on non-Windows platform");
        }
    });

Task("Default")
    .IsDependentOn("Post-Build");

RunTarget(target);
