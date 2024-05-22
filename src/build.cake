#tool "nuget:?package=Microsoft.TestPlatform&version=17.10.0"

var target = Argument("target", "Default");
FilePath solutionPath = File("./ReswCodeGen.sln");

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
        MSBuild(solutionPath, settings =>
            settings.SetPlatformTarget(PlatformTarget.MSIL)
                .SetMSBuildPlatform(MSBuildPlatform.x86)
                .UseToolVersion(MSBuildToolVersion.VS2019)
                .WithTarget("Build")
                .SetConfiguration("Release"));
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Restore")
    .Does(() =>
{
    MSTest("./**/bin/Release/*.Tests.dll");
});

Task("Post-Build")
    .IsDependentOn("Build")
    .Does(() => {
        CopyFileToDirectory("./VSPackage/bin/Release/ReswFileCodeGenerator.vsix", "./Artifacts/");
    });

Task("Default")
	.IsDependentOn("Post-Build");

RunTarget(target);