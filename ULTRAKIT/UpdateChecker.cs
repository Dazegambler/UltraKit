using System;
using System.Net;
using Humanizer;
using ULTRAKIT.Lua;

namespace ULTRAKIT
{
    public static class UpdateChecker
    {
        private static void PrintStatus()
        {
            // Get time when local built dlls were last modified
            var assemblyPath = System.Reflection.Assembly.GetAssembly(typeof(Plugin)).Location;
            var lastWriteTime = System.IO.File.GetLastWriteTime(assemblyPath);
            var dllContents = Convert.ToBase64String(System.IO.File.ReadAllBytes(assemblyPath));

            Debug.Log($"Your ULTRAKIT DLLs were last modified {lastWriteTime.Humanize()}", null, ConsoleColor.Green);

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