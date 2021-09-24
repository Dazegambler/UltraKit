using System;
using System.Net;
using BepInEx;
using Humanizer;
using ULTRAKIT.Core;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using ULTRAKIT.Loader;
using ULTRAKIT.Lua.API;
using ULTRAKIT.Lua.API.Statics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = ULTRAKIT.Lua.Debug;

namespace ULTRAKIT
{
    [BepInPlugin("ULTRAKIT", "ULTRAKIT", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public void Start()
        {
            PrintBuildAge();
            CoreContent.Initialize();
            UKLuaAPI.Initialize();
            AddonLoader.Initialize(AddonLoader.BundlePath);
        }

        public void Update()
        {
            // Addon refresh
            if (Keyboard.current.f8Key.wasPressedThisFrame)
            {
                // JetBrains Rider is suggesting me to turn this into a LINQ expression.
                // You better pray to God that I don't.
                foreach (var ukContents in AddonLoader.registry.Values)
                foreach (var content in ukContents)
                    if (content is UKContentPersistent persistent)
                        Destroy(persistent.Prefab);

                foreach (var addon in AddonLoader.registry.Keys)
                {
                    AddonLoader.registry[addon].Clear();
                    addon.Bundle.Unload(true);
                }

                UKStaticRegistry.addonData.Clear();
                UKStaticRegistry.sharedData.Clear();

                AddonLoader.registry.Clear();

                CoreContent.Initialize();
                AddonLoader.LoadAllAddons(AddonLoader.BundlePath);

                RefreshGuns();
            }
        }

        private static void RefreshGuns()
        {
            // This is expected to throw an error since there's not always a GunSetter present (eg. in menus)
            try
            {
                var gs = MonoSingleton<GunSetter>.Instance;
                var storedSlot = gs.gunc.currentSlot;
                var storedVariant = gs.gunc.currentVariation;
                gs.ResetWeapons();
                gs.gunc.currentSlot = storedSlot;
                gs.gunc.currentVariation = storedVariant;
                gs.gunc.YesWeapon();
            }
            catch
            {
            }
        }

        private void OnApplicationQuit()
        {
            // Ensures that the mod can be uninstalled without issue
            PlayerPrefs.SetInt("CurSlo", 1);
        }

        private static void PrintBuildAge()
        {
            // Get time when local built dlls were last modified
            var assemblyPath = System.Reflection.Assembly.GetAssembly(typeof(Plugin)).Location;
            var lastWriteTime = System.IO.File.GetLastWriteTime(assemblyPath);
            var dllContents = Convert.ToBase64String(System.IO.File.ReadAllBytes(assemblyPath));
            
            Debug.Log($"These ULTRAKIT DLLs were last modified {lastWriteTime.Humanize()}", null, ConsoleColor.Green);

            var (ghDllTime, ghDllContents) = LastBuild();
            if (ghDllTime != new DateTime(0))
            {
                if (ghDllContents == dllContents)
                {
                    Debug.Log("Your build matches the one in main.");
                    return;
                }
                var humanized = ghDllTime.Humanize();
                Debug.Log($"Last published build on GitHub was: {humanized}", null, ConsoleColor.Green);
                Debug.Log(
                    DateTime.Compare(ghDllTime, lastWriteTime) > 0
                        ? $"The build on Main is {(ghDllTime - lastWriteTime).Humanize()} younger than yours. Consider updating."
                        : $"Your build is {(ghDllTime - lastWriteTime).Humanize()} newer than the published one.", null,
                    ConsoleColor.Green);
            }
            else
            {
                Debug.Log("Could not retrieve last build info.");
            }
        }

        // Get time and content of last commit to of ultrakit.dll
        private static (DateTime, string) LastBuild()
        {
            // GET request
            string Request(string url)
            {
                var request =
                    (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "BuildAgeGetter";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new System.IO.StreamReader(stream))
                {
                    var data = reader.ReadToEnd();
                    return data;
                }
            }

            // If you touch this I will skin you alive
            string GetJsonValue(string data, string key)
            {
                var indexOfKey = data.IndexOf(key, StringComparison.Ordinal);
                var indexOfValue = indexOfKey + key.Length + 2;
                
                for (var i = indexOfValue; i < data.Length; i++)
                    if (data[i] == '"')
                        return data.Substring(indexOfValue, i - indexOfValue);

                return null;
            }
            
            try
            {
                const string repo = "https://api.github.com/repos/Dazegambler/Ultrakit/";
                var dll = Request($"{repo}contents/build/ULTRAKIT.dll");
                var commits = Request($"{repo}commits?path=build/ULTRAKIT.dll");
                return (DateTime.Parse(GetJsonValue(commits, "\"date\"")), GetJsonValue(dll, "\"content\""));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return (new DateTime(0), "");
            }
        }

    }
}