# **RCT3Launcher V2** 🚀

[![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/RF103T/RCT3LauncherV2?include_prereleases)](https://github.com/RF103T/RCT3LauncherV2/releases)
![GitHub all releases](https://img.shields.io/github/downloads/RF103T/RCT3LauncherV2/total)
![GitHub Repo stars](https://img.shields.io/github/stars/RF103T/RCT3LauncherV2?style=social)

📖[English](README.md)

---

<h3 align="center" >📢 此存储库正在迁移到<a href="https://github.com/dotnet/wpf">.NET6</a>。 📢</h1>

---

## 📚简介

![](https://github.com/RF103T/Resources/blob/main/RCT3LauncherV2/Images/MainWindow_Chinese.png)

适用于 `RollerCoaster Tycoon® 3 Complete Edition` 和 `RollerCoaster Tycoon® 3 Platinum` 的第三方游戏启动器，在游戏外提供许多的便捷功能。

此项目是对[RCT3Launcher](https://github.com/RF103T/RCT3Launcher)的重制。

系统支持：Windows 7 SP1、Windows 8.1、Windows 10 Version 1607+、Windows 11 Version 22000+ （支持情况会随时间变化，请参考[.NET 5.0支持的操作系统版本](https://github.com/dotnet/core/blob/master/release-notes/5.0/5.0-supported-os.md)）

## 🎈已经实现的功能
+ 多版本游戏管理
    - 支持白金版（Platinum）和完全版（Complete Edition）。
    - 支持多个游戏安装位置。
+ 存档实用功能
    - 获取存档列表和基本信息。
    - 存档导入、导出、重命名和删除。
+ 启动器更新
+ 多语言支持
    - 简体中文和英文。

## 🌈即将实现的功能
+ 自定义音乐管理

## 📋计划中的功能
+ 扩充管理
+ 针对 `RollerCoaster Tycoon® 3 Platinum` 的游戏修复向导
+ 游戏设置
+ `Reshade` 插件设置
+ 更多语言的翻译

## 🕹️安装
1. 打开[最新发布版页面](https://github.com/RF103T/RCT3LauncherV2/releases/latest)
2. 在 `Assets` 中下载合适的版本，其中没有任何后缀的为标准版本；带 `with_environment` 后缀的为自带.NET5环境的版本；带 `with_environment_update` 后缀的为覆盖升级包，请在使用前仔细阅读 **更新内容** 中的 **注意** 项目，或者使用启动器的更新功能。
3. 根据你在上个步骤选择的版本分为以下的步骤：
   
   (1). 如果你下载了标准版本，请先检查是否安装了.NET5环境，如果没有安装，请先[点击这里安装.NET5](https://dotnet.microsoft.com/download/dotnet/5.0/runtime)。然后，将下载的压缩包解压到任意空文件夹。
   
   (2). 如果你下载了 `with_environment` 版本，直接将下载的压缩包解压到任意空文件夹。
   
   (3). ⚠️ **不建议** ⚠️ 如果你下载了 `with_environment_update` 覆盖升级包，请先确认升级前的版本是否符合覆盖升级包要求，然后检查 **更新内容** 中的 **注意** 项目是否有特别说明，最后将下载的压缩包解压并覆盖到启动器安装目录。
   
4. 运行 `RCT3Launcher.exe`。🌴

## 📎引用和依赖项
+ [haf/DotNetZip.Semverd](https://github.com/haf/DotNetZip.Semverd)
+ [xoofx/markdig](https://github.com/xoofx/markdig)
+ [dotnet/runtime](https://github.com/dotnet/runtime)
+ [microsoft/XamlBehaviorsWpf](https://github.com/Microsoft/XamlBehaviorsWpf)
+ [secana/PeNet](https://github.com/secana/PeNet)
+ [aybe/Windows-API-Code-Pack-1.1](https://github.com/aybe/Windows-API-Code-Pack-1.1)

## 📃许可证
GNU General Public License v3.0
