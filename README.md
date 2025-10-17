[![VSIX](https://github.com/christianhelle/reswcodegen/actions/workflows/vsix.yml/badge.svg)](https://github.com/christianhelle/reswcodegen/actions/workflows/vsix.yml)
[![Version](https://vsmarketplacebadges.dev/version/ChristianResmaHelle.ResWFileCodeGenerator.svg)](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ResWFileCodeGenerator) 
[![Installs](https://vsmarketplacebadges.dev/downloads-short/ChristianResmaHelle.ResWFileCodeGenerator.svg)](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ResWFileCodeGenerator) 
[![Rating](https://vsmarketplacebadges.dev/rating-star/ChristianResmaHelle.ResWFileCodeGenerator.svg)](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ResWFileCodeGenerator)
[![buymeacoffee](https://img.shields.io/badge/buy%20me%20a%20coffee-donate-yellow.svg)](https://www.buymeacoffee.com/christianhelle)

# ResW File Code Generator
A Visual Studio Custom Tool for generating a strongly typed helper class for accessing localized resources from a .ResW file.

#### Download from **[Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ResWFileCodeGenerator)**

## Introduction

.ResW files are XML-based resource files used in Universal Windows Platform (UWP) and Windows UI (WinUI) applications to store localized strings and other resources. The ResW File Code Generator extension provides strongly-typed access to these resources, eliminating the need for string-based resource lookups and reducing the risk of runtime errors due to typos in resource keys.

## Installation

1. Open Visual Studio.
2. Go to Extensions > Manage Extensions.
3. Search for "ResW File Code Generator" in the Visual Studio Marketplace.
4. Click Download, then close Visual Studio to start the installation.
5. Follow the VSIX installer prompts and restart Visual Studio when prompted.

Alternatively, download and install the extension directly from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ResWFileCodeGenerator).

## Features

- Define custom namespace for the generated file
- Auto-updating of generated code file when changes are made to the .ResW Resource file
- XML documentation style comments like "Localized resource similar to '[the value]'"
- Supports Visual Studio 2015, 2017, 2019, and 2022
- Supports dotted keys - Replaces **`.`** with **`_`** (e.g. `Something.Awesome` = `Something_Awesome`)

## Getting Started

1. Create a new UWP or WinUI project in Visual Studio.
2. Add a new .ResW resource file to your project (right-click project > Add > New Item > Resources File (.resw)).
3. Add some string resources to the .resw file.
4. In the Solution Explorer, right-click the .resw file and select Properties.
5. In the Properties window, set the "Custom Tool" property to "ReswFileCodeGenerator" or "InternalReswFileCodeGenerator".
6. Save the .resw file. A corresponding .cs or .vb file should be generated automatically.
7. Use the generated class to access your resources in a type-safe manner.

## Configuration

### Custom Namespace

By default, the generated class uses the project's default namespace. To specify a custom namespace:

1. Right-click the .resw file in Solution Explorer.
2. Select Properties.
3. In the "Custom Tool Namespace" property, enter your desired namespace.

### Custom Tool Selection

- Use "ReswFileCodeGenerator" for a public class.
- Use "InternalReswFileCodeGenerator" for an internal (C#) or friend (VB) class.

## Custom Tools

- ReswFileCodeGenerator - Generates a public class
- InternalReswFileCodeGenerator - Generates an internal (C#) / friend (VB) class

## Supported Languages

- C#
- Visual Basic

## Supported Scenarios

- Universal Windows Platform (UWP) applications
- Windows UI Library (WinUI) 2 and 3 applications
- .NET 5+ WinUI applications
- C# and Visual Basic projects

**Limitations:**
- Only works with .resw files, not .resx files
- Requires Visual Studio with the extension installed
- Generated code is read-only; do not modify it manually

## How It Works

The ResW File Code Generator is a Visual Studio Custom Tool that automatically generates C# or Visual Basic code when you save a .resw file. The tool:

1. Parses the XML structure of the .resw file
2. Extracts resource keys and values
3. Generates a strongly-typed class with properties for each resource
4. Handles dotted keys by replacing dots with underscores
5. Adds XML documentation comments with the resource values

The generated code uses the Windows.ApplicationModel.Resources.ResourceLoader to load resources at runtime.

## Screenshots

![ReswFileCodeGenerator Custom Tool](https://github.com/christianhelle/reswcodegen/raw/master/images/reswfilecodegenerator-customtool.png)

![InternalReswFileCodeGenerator Custom Tool](https://github.com/christianhelle/reswcodegen/raw/master/images/internalreswfilecodegenerator-customtool.png)

## Example C# Usage

```csharp
string test1, test2, test3, test4;

void LoadLocalizedStrings()
{
    test1 = CSharpUwpApp.Properties.Resources.Test1;
    test2 = CSharpUwpApp.Properties.Resources.Test2;
    test3 = CSharpUwpApp.Properties.Resources.Test3;
    test4 = CSharpUwpApp.Properties.Resources.Test_With_Dotted_Keys;
}
```

## Example VB Usage

```vbnet
Dim test1, test2, test3, test4

Private Sub LoadLocalizedStrings()
    test1 = VisualBasicUwpApp.Properties.Resources.Test1
    test2 = VisualBasicUwpApp.Properties.Resources.Test2
    test3 = VisualBasicUwpApp.Properties.Resources.Test3
    test4 = VisualBasicUwpApp.Properties.Resources.Test_With_Dotted_Keys
End Sub
```

## Example Generated C# Code

```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by ResW File Code Generator (http://bit.ly/reswcodegen)
//     ResW File Code Generator was written by Christian Resma Helle
//     and is under GNU General Public License version 2 (GPLv2)
//
//     This code contains a helper class exposing property representations
//     of the string resources defined in the specified .ResW file
//
//     Code generation environment:
//         Time and Date: 4/29/2025 11:49:45 PM (UTC-08:00) Pacific Time (US & Canada)
//         Computer Name: my-pc.my-domain.net
//         User Name    : MY-DOMAIN\JohnDoe
// </auto-generated>
// ---------------------------------------------------------------------------------------
namespace CSharpUwpApp.Properties
{
    public sealed partial class Resources
    {
        private static global::Windows.ApplicationModel.Resources.ResourceLoader resourceLoader;

        /// <summary>
        /// Get or set ResourceLoader implementation
        /// </summary>
        public static global::Windows.ApplicationModel.Resources.ResourceLoader Resource
        {
            get
            {
                if ((resourceLoader == null))
                {
                    Resources.Initialize();
                }
                return resourceLoader;
            }
            set
            {
                resourceLoader = value;
            }
        }

        /// <summary>
        /// Localized resource similar to "Test 1 value"
        /// </summary>
        public static string Test1
        {
            get
            {
                return Resource.GetString("Test1");
            }
        }

        /// <summary>
        /// Localized resource similar to "Test 2 value"
        /// </summary>
        public static string Test2
        {
            get
            {
                return Resource.GetString("Test2");
            }
        }

        /// <summary>
        /// Localized resource similar to "Test 3 value"
        /// </summary>
        public static string Test3
        {
            get
            {
                return Resource.GetString("Test3");
            }
        }

        /// <summary>
        /// Localized resource similar to "Test With Dotted Keys"
        /// </summary>
        public static string Test_With_Dotted_Keys
        {
            get
            {
                return Resource.GetString("Test/With/Dotted/Keys");
            }
        }

        public static void Initialize()
        {
            string executingAssemblyName;
            executingAssemblyName = global::System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            string currentAssemblyName;
            currentAssemblyName = typeof(Resources).AssemblyQualifiedName;
            string[] currentAssemblySplit;
            currentAssemblySplit = currentAssemblyName.Split(',');
            currentAssemblyName = currentAssemblySplit[1].Trim();
            if ((global::Windows.UI.Core.CoreWindow.GetForCurrentThread() == null))
            {
                if (executingAssemblyName.Equals(currentAssemblyName))
                {
                    resourceLoader = global::Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse("Resources");
                }
                else
                {
                    resourceLoader = global::Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse(currentAssemblyName + "/Resources");
                }
            }
            else
            {
                if (executingAssemblyName.Equals(currentAssemblyName))
                {
                    resourceLoader = global::Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Resources");
                }
                else
                {
                    resourceLoader = global::Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView(currentAssemblyName + "/Resources");
                }
            }
        }
    }
}
```

## Example Generated Visual Basic Code

```vbnet
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


'---------------------------------------------------------------------------------------
'<auto-generated>
'    This code was generated by ResW File Code Generator (http://bit.ly/reswcodegen)
'    ResW File Code Generator was written by Christian Resma Helle
'    and is under GNU General Public License version 2 (GPLv2)
'
'    This code contains a helper class exposing property representations
'    of the string resources defined in the specified .ResW file
'
'    Code generation environment:
'        Time and Date: 4/30/2025 12:04:30 AM (UTC-08:00) Pacific Time (US & Canada)
'        Computer Name: my-pc.my-domain.net
'        User Name    : MY-DOMAIN\JohnDoe
'</auto-generated>
'---------------------------------------------------------------------------------------

Partial Public NotInheritable Class Resources

    Private Shared resourceLoader As Global.Windows.ApplicationModel.Resources.ResourceLoader

    '''<summary>
    '''Get or set ResourceLoader implementation
    '''</summary>
    Public Shared Property Resource() As Global.Windows.ApplicationModel.Resources.ResourceLoader
        Get
            If (resourceLoader Is Nothing) Then
                Resources.Initialize
            End If
            Return resourceLoader
        End Get
        Set
            resourceLoader = value
        End Set
    End Property

    '''<summary>
    '''Localized resource similar to "Test 1 value"
    '''</summary>
    Public Shared ReadOnly Property Test1() As String
        Get
            Return Resource.GetString("Test1")
        End Get
    End Property

    '''<summary>
    '''Localized resource similar to "Test 2 value"
    '''</summary>
    Public Shared ReadOnly Property Test2() As String
        Get
            Return Resource.GetString("Test2")
        End Get
    End Property

    '''<summary>
    '''Localized resource similar to "Test 3 value"
    '''</summary>
    Public Shared ReadOnly Property Test3() As String
        Get
            Return Resource.GetString("Test3")
        End Get
    End Property

    '''<summary>
    '''Localized resource similar to "Test With Dotted Keys"
    '''</summary>
    Public Shared ReadOnly Property Test_With_Dotted_Keys() As String
        Get
            Return Resource.GetString("Test/With/Dotted/Keys")
        End Get
    End Property

    Public Shared Sub Initialize()
        Dim executingAssemblyName As String
        executingAssemblyName = Global.System.Reflection.[Assembly].GetEntryAssembly.GetName.Name
        Dim currentAssemblyName As String
        currentAssemblyName = GetType(Resources).AssemblyQualifiedName
        Dim currentAssemblySplit() As String
        currentAssemblySplit = currentAssemblyName.Split(Global.Microsoft.VisualBasic.ChrW(44))
        currentAssemblyName = currentAssemblySplit(1).Trim
        If (Global.Windows.UI.Core.CoreWindow.GetForCurrentThread Is Nothing) Then
            If executingAssemblyName.Equals(currentAssemblyName) Then
                resourceLoader = Global.Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse("Resources")
            Else
                resourceLoader = Global.Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse(currentAssemblyName + "/Resources")
            End If
        Else
            If executingAssemblyName.Equals(currentAssemblyName) Then
                resourceLoader = Global.Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("Resources")
            Else
                resourceLoader = Global.Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView(currentAssemblyName + "/Resources")
            End If
        End If
    End Sub
End Class

## Troubleshooting

### Code Not Generated
- Ensure the Custom Tool property is set correctly.
- Try saving the .resw file again.
- Check the Error List window for any errors.
- Restart Visual Studio.

### IntelliSense Not Working
- Build the project to ensure the generated code compiles.
- Check that the namespace is correct.

### Resources Not Loading
- Ensure the .resw file is in the correct location (usually Properties/Resources.resw).
- Verify the resource keys match exactly.

### Extension Not Appearing
- Confirm the extension is installed and enabled in Extensions > Manage Extensions.
- Restart Visual Studio after installation.

For more help, check the [GitHub Issues](https://github.com/christianhelle/reswcodegen/issues) page.

## Contributing

We welcome contributions! Please:

1. Fork the repository.
2. Create a feature branch.
3. Make your changes.
4. Add tests if applicable.
5. Submit a pull request.

Report bugs or request features via [GitHub Issues](https://github.com/christianhelle/reswcodegen/issues).

## License

This project is licensed under the GNU General Public License version 2 (GPLv2). See the [LICENSE](LICENSE) file for details.

## Links

- [GitHub Repository](https://github.com/christianhelle/reswcodegen)
- [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ResWFileCodeGenerator)
- [Issues](https://github.com/christianhelle/reswcodegen/issues)
- [Buy Me a Coffee](https://www.buymeacoffee.com/christianhelle)
```
