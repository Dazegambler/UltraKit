using System;
using ULTRAKIT.Lua.Components;

namespace ULTRAKIT.Lua
{
    public class CoolLogger
    {
        public static void Log(UKScriptRuntime c, string msg)
        {
            var mod = Trimmy(c);
            BCE.console.Write($"\n[UKAddon: {mod}] ", ConsoleColor.Cyan);
            BCE.console.Write($"{msg}\n", ConsoleColor.White);
        }

        public static void LogError(UKScriptRuntime c, string msg, string type)
        {
            var mod = Trimmy(c);
            BCE.console.Write($"\n[UKAddon: {mod}] {type}: ", ConsoleColor.DarkRed);
            BCE.console.Write($"{msg}\n", ConsoleColor.Red);
        }
        
        private static string Trimmy(UKScriptRuntime c)
        {
            var mod = c.addon.ModName.Trim();
            return mod.Length <= 9 ? mod : mod.Remove(6) + "...";
        }
    }
}