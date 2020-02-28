# Notes on compiling the application

## Application release

### New version

* Update the GUI application version number.
* Update the CLI application version number.
* Update installer project version number.

### Build

To compile the application for different versions of the Evolution SDK follow these steps:

* Add **ONE** of the following constants to the DefineConstants section of the WineMS.Evolution.csproj file. Set the constant and do a build. Then change it to the next one and create a build.
   * EVO720
   * EVO920
   
      E.g.:
      
    ```xml
    <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
      <DebugSymbols>true</DebugSymbols>
      <DebugType>full</DebugType>
      <Optimize>false</Optimize>
      <OutputPath>bin\Debug\</OutputPath>
      <DefineConstants>DEBUG;TRACE;EVO720</DefineConstants>
      <ErrorReport>prompt</ErrorReport>
      <WarningLevel>4</WarningLevel>
    </PropertyGroup>
    ```
* Rebuild the solution.
* Build the installer project (WineMsSetup).
* Compress the installer files into a file using the following naming convention: 
    WineMsSetup.\<major version>.\<minor version>-\<SDK version>.zip
    E.g.: 
      WineMsSetup.1.16-720.zip
      WineMsSetup.1.16-920.zip
* Repeat the Build steps for each version of the SDK.

## How to reference a version of the SDK

Add a conditional ItemGroup to the project file.

E.g.:

```xml
<ItemGroup Condition="$(DefineConstants.Contains('EVO720'))">
  <Reference Include="Pastel.Evolution, Version=7.20.0.0, Culture=neutral, PublicKeyToken=86fac4c1500a3756, processorArchitecture=MSIL">
    <SpecificVersion>False</SpecificVersion>
    <HintPath>..\..\..\EvolutionSdk\720\Pastel.Evolution.dll</HintPath>
  </Reference>
</ItemGroup>

<ItemGroup Condition="$(DefineConstants.Contains('EVO920'))">
  <Reference Include="Pastel.Evolution, Version=7.20.0.0, Culture=neutral, PublicKeyToken=86fac4c1500a3756, processorArchitecture=MSIL">
    <SpecificVersion>False</SpecificVersion>
    <HintPath>..\..\..\EvolutionSdk\920\Pastel.Evolution.dll</HintPath>
  </Reference>
  <Reference Include="Pastel.Evolution.Common">
    <HintPath>..\..\..\EvolutionSdk\920\Pastel.Evolution.Common.dll</HintPath>
  </Reference>
</ItemGroup>
```