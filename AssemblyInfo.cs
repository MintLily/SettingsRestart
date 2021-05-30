using System;
using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

[assembly: AssemblyTitle(SettingsRestart.BuildInfo.Name)]
[assembly: AssemblyDescription(SettingsRestart.BuildInfo.Description)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(SettingsRestart.BuildInfo.Company)]
[assembly: AssemblyProduct(SettingsRestart.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + SettingsRestart.BuildInfo.Author)]
[assembly: AssemblyTrademark(SettingsRestart.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(SettingsRestart.BuildInfo.Version)]
[assembly: AssemblyFileVersion(SettingsRestart.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(SettingsRestart.Main),
    SettingsRestart.BuildInfo.Name,
    SettingsRestart.BuildInfo.Version,
    SettingsRestart.BuildInfo.Author,
    SettingsRestart.BuildInfo.DownloadLink)]
[assembly: MelonColor(ConsoleColor.Yellow)]

//[assembly: MelonOptionalDependencies("", "", "", "")]
// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("VRChat", "VRChat")]