# **RCT3Launcher V2** 🚀

[![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/RF103T/RCT3LauncherV2?include_prereleases)](https://github.com/RF103T/RCT3LauncherV2/releases)
![GitHub all releases](https://img.shields.io/github/downloads/RF103T/RCT3LauncherV2/total)
![GitHub Repo stars](https://img.shields.io/github/stars/RF103T/RCT3LauncherV2?style=social)

📖[简体中文](README_zh.md)

---

## 📚Overview

![](https://github.com/RF103T/Resources/blob/main/RCT3LauncherV2/Images/MainWindow_English.png)

A third-party game launcher `RollerCoaster Tycoon® 3 Complete Edition` and `RollerCoaster Tycoon® 3 Platinum`, providing many convenient features outside the game.

It is a remake of [RCT3Launcher](https://github.com/RF103T/RCT3Launcher).

System support: Windows 7 SP1, Windows 8.1, Windows 10 Version 1607+, Windows 11 Version 22000+ (The support situation will change over time, please refer to [.NET 6 - Supported OS versions](https://github.com/dotnet/core/blob/main/release-notes/6.0/supported-os.md))

## 🎈Features achieved
+ Multi-version game management
    - Supports Platinum and Complete Edition.
    - Support multiple game installations.
+ Save management function
    - Get save list and basic information.
    - Save(s) import, export, rename and delete.
+ Update in launcher
+ Multi-language support
    - Simplified Chinese
    - English
    - More language using easy-to-understand multi-language configurations...

## 🌈Features to be implemented
+ Custom music management

## 📋Features in the plan
+ Mods management
+ Game repair wizard for `RollerCoaster Tycoon® 3 Platinum`
+ Game settings
+ `Reshade` plugin settings
+ Translation in more languages

## 🚧Runtime Requirements
| Launcher Version | .NET Runtime Version |
| ---------------- | -------------------- |
| <= 0.2.x         | 5                    |
| > 0.2.x          | 6                    |

## 🕹️Installation
1. Go to the [latest release on the Releases page](https://github.com/RF103T/RCT3LauncherV2/releases/latest).
2. Download the latest `.zip` file in `Assets`. the standard version without any suffix. The `with_environment` version comes with .NET6 environment. The ones with the suffix `with_environment_update` are overwriting upgrade file, please read the **Note** item in **Release Notes** carefully before using, or use launcher to update.
3. According to the version you selected in the previous step, it is divided into the following steps:
   
   (1). If you downloaded standard version. Please check whether the .NET6 environment is installed first. If not, please [install .NET6](https://dotnet.microsoft.com/download/dotnet/6.0/runtime). Then, unzip the downloaded compressed file to any empty folder.
   
   (2). If you downloaded `with_environment` version. Directly unzip the downloaded compressed file to any empty folder.
   
   (3). ⚠️ **Not Recommended!** ⚠️ If you downloaded `with_environment_update` overwriting upgrade file. Please confirm whether the version before the upgrade meets requirements of the overwriting upgrade file, and then check whether there are special instructions in the **Note** item in **Release Notes**, and finally it will be downloaded the compressed file is unzip and overwritten to launcher installation directory.
   
4. Run `RCT3Launcher.exe` file.🌴

## 📎Dependencies and References
+ [haf/DotNetZip.Semverd](https://github.com/haf/DotNetZip.Semverd)
+ [xoofx/markdig](https://github.com/xoofx/markdig)
+ [dotnet/runtime](https://github.com/dotnet/runtime)
+ [microsoft/XamlBehaviorsWpf](https://github.com/Microsoft/XamlBehaviorsWpf)
+ [secana/PeNet](https://github.com/secana/PeNet)
+ [aybe/Windows-API-Code-Pack-1.1](https://github.com/aybe/Windows-API-Code-Pack-1.1)

## 📃License
GNU General Public License v3.0
