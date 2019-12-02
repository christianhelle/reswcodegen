[![Build Status](https://christianhelle.visualstudio.com/ResW%20File%20Code%20Generator/_apis/build/status/CI%20Build?branchName=master)](https://christianhelle.visualstudio.com/ResW%20File%20Code%20Generator/_build/latest?definitionId=11&branchName=master)
[![Version](https://vsmarketplacebadge.apphb.com/version/ChristianResmaHelle.ResWFileCodeGenerator.svg)](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ResWFileCodeGenerator) 
[![Installs](https://vsmarketplacebadge.apphb.com/downloads-short/ChristianResmaHelle.ResWFileCodeGenerator.svg)](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ResWFileCodeGenerator) 
[![Rating](https://vsmarketplacebadge.apphb.com/rating-star/ChristianResmaHelle.ResWFileCodeGenerator.svg)](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.ResWFileCodeGenerator)

# ResW File Code Generator
A Visual Studio Custom Tool for generating a strongly typed helper class for accessing localized resources from a .ResW file.

**Features**

- Define custom namespace for the generated file
- Auto-updating of generated code file when changes are made to the .ResW Resource file
- XML documentation style comments like "Localized resource similar to '[the value]'"
- Supports Visual Studio 2015, 2017, and 2019
- Supports dotted keys - Replaces **.** with **_** (e.g. `Something.Awesome` = `Something_Awesome`)

**Custom Tools**

- ReswFileCodeGenerator - Generates a public class
- InternalReswFileCodeGenerator - Generates an internal (C#) / friend (VB) class

**Supported Languages**

- C#
- Visual Basic

**Screenshots**

![ReswFileCodeGenerator Custom Tool](https://github.com/christianhelle/reswcodegen/raw/master/images/reswfilecodegenerator-customtool.png)

![InternalReswFileCodeGenerator Custom Tool](https://github.com/christianhelle/reswcodegen/raw/master/images/internalreswfilecodegenerator-customtool.png)


**Example C# Usage**

    string test1, test2, test3;

    void LoadLocalizedStrings()
    {
        test1 = App1.LocalizedResources.Resources.Test1;
        test2 = App1.LocalizedResources.Resources.Test2;
        test3 = App1.LocalizedResources.Resources.Test3;
        test4 = App1.LocalizedResources.Resources.Test_With_Dotted_Keys;
    }


**Example VB Usage**

    Dim test1, test2, test3

    Private Sub LoadLocalizedStrings()
        test1 = AppVb.LocalizedStrings.Resources.Test1
        test2 = AppVb.LocalizedStrings.Resources.Test2
        test3 = AppVb.LocalizedStrings.Resources.Test3
        test4 = AppVb.LocalizedStrings.Resources.Test_With_Dotted_Keys;
    End Sub


**Example Generated C# Code**

    //------------------------------------------------------------------------------
    // <auto-generated>
    //     This code was generated by a tool.
    //     Runtime Version:4.0.30319.42000
    //
    //     Changes to this file may cause incorrect behavior and will be lost if
    //     the code is regenerated.
    // </auto-generated>
    //------------------------------------------------------------------------------

    // --------------------------------------------------------------------------------------------------
    // <auto-generatedInfo>
    // 	This code was generated by ResW File Code Generator (http://bit.ly/reswcodegen)
    // 	ResW File Code Generator was written by Christian Resma Helle
    // 	and is under GNU General Public License version 2 (GPLv2)
    // 
    // 	This code contains a helper class exposing property representations
    // 	of the string resources defined in the specified .ResW file
    // 
    // 	Generated: 05/20/2019 15:47:37
    // </auto-generatedInfo>
    // --------------------------------------------------------------------------------------------------
    namespace App2
    {
        using Windows.ApplicationModel.Resources;
    
    
        public sealed partial class Resources
        {
        
            private static ResourceLoader resourceLoader;
        
            /// <summary>
            /// Get or set ResourceLoader implementation
            /// </summary>
            public static ResourceLoader Resource
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
            /// Localized resource similar to "test"
            /// </summary>
            public static string Test
            {
                get
                {
                    return Resource.GetString("Test");
                }
            }
        
            /// <summary>
            /// Localized resource similar to "test"
            /// </summary>
            public static string Test2
            {
                get
                {
                    return Resource.GetString("Test2");
                }
            }
        
            /// <summary>
            /// Localized resource similar to "test"
            /// </summary>
            public static string Test3
            {
                get
                {
                    return Resource.GetString("Test3");
                }
            }
        
            /// <summary>
            /// Localized resource similar to "test"
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
                executingAssemblyName = Windows.UI.Xaml.Application.Current.GetType().AssemblyQualifiedName;
                string[] executingAssemblySplit;
                executingAssemblySplit = executingAssemblyName.Split(',');
                executingAssemblyName = executingAssemblySplit[1];
                string currentAssemblyName;
                currentAssemblyName = typeof(Resources).AssemblyQualifiedName;
                string[] currentAssemblySplit;
                currentAssemblySplit = currentAssemblyName.Split(',');
                currentAssemblyName = currentAssemblySplit[1];
                if (executingAssemblyName.Equals(currentAssemblyName))
                {
                    resourceLoader = ResourceLoader.GetForCurrentView("Resources");
                }
                else
                {
                    resourceLoader = ResourceLoader.GetForCurrentView(currentAssemblyName + "/Resources");
                }
            }
        }
    }



**Example Generated Visual Basic Code**

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

    Imports Windows.ApplicationModel.Resources

    '--------------------------------------------------------------------------------------------------
    '<auto-generatedInfo>
    '	This code was generated by ResW File Code Generator (http://bit.ly/reswcodegen)
    '	ResW File Code Generator was written by Christian Resma Helle
    '	and is under GNU General Public License version 2 (GPLv2)
    '
    '	This code contains a helper class exposing property representations
    '	of the string resources defined in the specified .ResW file
    '
    '	Generated: 05/20/2019 15:48:18
    '</auto-generatedInfo>
    '--------------------------------------------------------------------------------------------------

    Partial Public NotInheritable Class Resources
    
        Private Shared resourceLoader As ResourceLoader
    
        '''<summary>
        '''Get or set ResourceLoader implementation
        '''</summary>
        Public Shared Property Resource() As ResourceLoader
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
        '''Localized resource similar to "test"
        '''</summary>
        Public Shared ReadOnly Property Test() As String
            Get
                Return Resource.GetString("Test")
            End Get
        End Property
    
        '''<summary>
        '''Localized resource similar to "test"
        '''</summary>
        Public Shared ReadOnly Property Test2() As String
            Get
                Return Resource.GetString("Test2")
            End Get
        End Property
    
        '''<summary>
        '''Localized resource similar to "test"
        '''</summary>
        Public Shared ReadOnly Property Test3() As String
            Get
                Return Resource.GetString("Test3")
            End Get
        End Property
    
        '''<summary>
        '''Localized resource similar to "test"
        '''</summary>
        Public Shared ReadOnly Property Test_With_Dotted_Keys() As String
            Get
                Return Resource.GetString("Test/With/Dotted/Keys")
            End Get
        End Property
    
        Public Shared Sub Initialize()
            Dim executingAssemblyName As String
            executingAssemblyName = Windows.UI.Xaml.Application.Current.GetType().AssemblyQualifiedName
            Dim executingAssemblySplit() As String
            executingAssemblySplit = executingAssemblyName.Split(Global.Microsoft.VisualBasic.ChrW(44))
            executingAssemblyName = executingAssemblySplit(1)
            Dim currentAssemblyName As String
            currentAssemblyName = GetType(Resources).AssemblyQualifiedName
            Dim currentAssemblySplit() As String
            currentAssemblySplit = currentAssemblyName.Split(Global.Microsoft.VisualBasic.ChrW(44))
            currentAssemblyName = currentAssemblySplit(1)
            If executingAssemblyName.Equals(currentAssemblyName) Then
                resourceLoader = ResourceLoader.GetForCurrentView("Resources")
            Else
                resourceLoader = ResourceLoader.GetForCurrentView(currentAssemblyName + "/Resources")
            End If
        End Sub
    End Class
