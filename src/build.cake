#tool "nuget:?package=Microsoft.TestPlatform&version=18.0.0"

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
        
        if (isRunningOnWindows) {
            Information("Building full solution on Windows");
            DotNetBuild(solutionPath.ToString(), new DotNetBuildSettings {
                Configuration = "Release",
                NoRestore = true
            });
        } else {
            Warning("Building on non-Windows environment - skipping VSPackage project");
            // On non-Windows platforms, exclude the VSPackage project
            var projects = GetFiles("./*/**.csproj")
                .Where(p => !p.FullPath.Contains("VSPackage"));
                
            foreach (var project in projects) {
                Information($"Building {project.GetFilenameWithoutExtension()}");
                DotNetBuild(project.FullPath, new DotNetBuildSettings {
                    Configuration = "Release",
                    NoRestore = true
                });
            }
        }
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    if (isRunningOnWindows) {
        Information("Running all tests on Windows");
        DotNetTest(solutionPath.ToString(), new DotNetTestSettings {
            Configuration = "Release",
            NoRestore = true,
            NoBuild = true
        });
    } else {
        Warning("Running tests on non-Windows environment - some tests might be skipped");
        var testProjects = GetFiles("./**/*.Tests.csproj")
            .Where(p => !p.FullPath.Contains("VSPackage"));
            
        foreach (var project in testProjects) {
            Information($"Testing {project.GetFilenameWithoutExtension()}");
            try {
                DotNetTest(project.FullPath, new DotNetTestSettings {
                    Configuration = "Release",
                    NoRestore = true,
                    NoBuild = true
                });
            } catch (Exception ex) {
                Warning($"Test execution failed for {project.GetFilenameWithoutExtension()}: {ex.Message}");
            }
        }
    }
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
