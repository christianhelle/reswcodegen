# ResW File Code Generator

ResW File Code Generator is a Visual Studio extension (.vsix) that provides custom tools for generating strongly typed helper classes from .ResW resource files. The extension generates C# or Visual Basic classes that provide typed access to localized string resources.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

### System Requirements
**CRITICAL**: This project requires Windows to build. The Visual Studio SDK dependencies cannot be resolved on Linux or macOS.

### Bootstrap and Build
- Prerequisites: Windows with Visual Studio (2015, 2017, 2019, or 2022) or MSBuild Tools
- **Build NEVER WORKS on Linux/macOS** - Only attempt builds on Windows environments
- Install .NET Framework 4.7.2 or later
- Install Visual Studio SDK (included with Visual Studio Community/Professional/Enterprise)

**Windows Build Commands:**
```bash
# Using MSBuild directly (GitHub Actions approach)
msbuild ReswCodeGen.sln /property:Configuration=Release /t:Restore
msbuild ReswCodeGen.sln /property:Configuration=Release /t:Rebuild
# Build takes 2-3 minutes. NEVER CANCEL. Set timeout to 10+ minutes.
```

**Alternative Build (Cake Build System):**
```bash
cd src
dotnet tool restore
# Restores Cake build tool - takes 30 seconds
pwsh build.ps1
# Complete build with Cake - takes 3-5 minutes. NEVER CANCEL. Set timeout to 15+ minutes.
```

### Testing
**Unit Tests:**
```bash
# On Windows only
cd src
dotnet test VSPackage.Tests/VSPackage.Tests.csproj --configuration Release
# Test run takes 1-2 minutes. NEVER CANCEL. Set timeout to 10+ minutes.
```

**Alternative Test (MSTest via Cake):**
```bash
cd src
pwsh build.ps1 --target=Run-Unit-Tests
# Takes 2-3 minutes. NEVER CANCEL. Set timeout to 10+ minutes.
```

### Build Output
- VSIX file: `src/VSPackage/bin/Release/ResWFileCodeGenerator.vsix`
- This is the Visual Studio extension package that can be installed

## Validation

**IMPORTANT**: You cannot run the Visual Studio extension on Linux. Validation must be done on Windows with Visual Studio installed.

**Manual Validation Steps (Windows only):**
1. Build the VSIX successfully 
2. Install the generated `.vsix` file in Visual Studio
3. Create a test UWP/WinUI project
4. Add a `.resw` resource file to the project
5. Set the Custom Tool property to "ReswFileCodeGenerator" or "InternalReswFileCodeGenerator"
6. Verify that a strongly-typed code file is generated
7. Test that the generated code compiles and provides IntelliSense

**On non-Windows environments:**
- You can only examine source code and run static analysis
- Build and test commands will fail - this is expected
- Focus on code review, documentation, and architecture changes

## Codebase Navigation

### Key Projects
- **`src/VSPackage/VSPackage.csproj`** - Main Visual Studio extension project
- **`src/VSPackage.Tests/VSPackage.Tests.csproj`** - Unit tests using MSTest framework
- **`ReswCodeGen.sln`** - Main solution file (root level)
- **`src/ReswCodeGen.sln`** - Source-level solution file

### Core Code Locations
- **`src/VSPackage/CustomTool/`** - Core code generation logic
  - `ReswFileCodeGenerator.cs` - Base class for custom tools
  - `ReswFileCSharpCodeGenerator.cs` - C# code generator
  - `ReswFileVisualBasicCodeGenerator.cs` - VB.NET code generator  
  - `ResourceParser.cs` - Parses .resw XML files
  - `CodeGeneratorFactory.cs` - Factory for creating generators
- **`src/VSPackage/source.extension.vsixmanifest`** - Extension manifest defining VS integration
- **`src/VSPackage.Tests/Resources/`** - Test .resw files for validation

### Build Configuration
- **`src/build.cake`** - Cake build script with Clean, Restore, Build, Test tasks
- **`src/build.ps1`** - PowerShell wrapper for Cake build
- **`src/.config/dotnet-tools.json`** - .NET local tools (Cake)
- **`.github/workflows/vsix.yml`** - CI/CD pipeline for building and publishing

### Important Files
- **`global.json`** - MSBuild SDK versions
- **`src/VSPackage/Guids.cs`** - Visual Studio integration GUIDs
- **`src/publish-manifest.json`** - Marketplace publishing configuration

## Common Tasks

### Adding New Code Generation Features
1. Examine existing generators in `src/VSPackage/CustomTool/`
2. Key files to modify:
   - Code generators for language-specific output
   - `ResourceParser.cs` for new .resw parsing features
   - `CodeGeneratorFactory.cs` if adding new generator types
3. Add corresponding tests in `src/VSPackage.Tests/`
4. Always test with real .resw files from `src/VSPackage.Tests/Resources/`

### Testing Code Generation
- Use sample .resw files in `src/VSPackage.Tests/Resources/Resources.resw`
- Test both C# and Visual Basic output
- Verify dotted key handling (e.g., "Test.Key" becomes "Test_Key")
- Check XML comment generation

### Debugging Custom Tools
- The extension registers COM components for Visual Studio integration
- Custom tools are: "ReswFileCodeGenerator" (public) and "InternalReswFileCodeGenerator" (internal/friend)
- Error handling shows MessageBox dialogs and logs to Application Insights

### CI/CD Pipeline
- **`.github/workflows/vsix.yml`** - Builds on every push, publishes to Open VSIX Gallery
- **`.github/workflows/release.yml`** - Release pipeline for Visual Studio Marketplace
- Both use MSBuild on `windows-latest` runners

## Timing Expectations

**NEVER CANCEL** any of these operations:
- **Package restore**: 30-60 seconds
- **Full build**: 3-5 minutes (set timeout: 15+ minutes)
- **Test execution**: 1-3 minutes (set timeout: 10+ minutes)
- **VSIX packaging**: 1-2 minutes (set timeout: 10+ minutes)

## Platform Limitations

**Do not attempt on Linux/macOS:**
- Building the main VSPackage project (requires Visual Studio SDK)
- Running unit tests (depend on VSPackage project)
- Testing the Visual Studio extension functionality
- Using MSBuild commands

**You can do on any platform:**
- Code review and analysis
- Documentation updates  
- Configuration file changes
- Static analysis of source code
- Examining .resw resource file formats

## Troubleshooting

**"Microsoft.VsSDK.targets not found"** - Normal on non-Windows, requires Visual Studio SDK
**"Could not resolve MSBuild"** - Cake build issue on non-Windows, MSBuild not available
**Build succeeds but tests fail** - Check test .resw files are being copied correctly

Always validate that code changes don't break the core code generation functionality by testing with sample .resw files when on Windows.