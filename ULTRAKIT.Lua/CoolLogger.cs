using System;
using MoonSharp.Interpreter;
using ULTRAKIT.Data.Components;
using ULTRAKIT.Lua.Components;

namespace ULTRAKIT.Lua
{
    public static class Debug
    {
        // https://i.redd.it/83jbj28hnq461.png
        
        private const string nl = "\n";
        private const string in_str = "[Info   :  Ultrakit] ";
        private const string wr_str = "[Warning:  Ultrakit] ";
        private const string er_str = "[Error  :  Ultrakit] ";
        private static Action<string, ConsoleColor> wrt = BCE.console.Write;
        
        public static void Log(string msg, UKScriptRuntime c = null)
        {
            wrt(ad_str(c) ?? in_str, c ? ConsoleColor.Cyan : ConsoleColor.White);
            wrt(msg+nl, ConsoleColor.Gray);
        }
        
        public static void LogWarning(string msg, UKScriptRuntime c = null)
        {
            wrt(ad_str(c) ?? wr_str, ConsoleColor.Yellow);
            wrt(msg+nl, ConsoleColor.Yellow);
        }
        
        public static void LogError(string msg, UKScriptRuntime c = null)
        {
            wrt(ad_str(c) == null ? er_str : ad_str(c), ConsoleColor.DarkRed);
            wrt(msg+nl, ConsoleColor.Red);
        }

        public static void LogError(InterpreterException e, UKScriptRuntime c, string t, UKScript d)
        {
            LogError($"{t} in script \"{d.sourceCode.name}\": {e.DecoratedMessage}", c);
        }
        
        
        private static string ad_str(UKScriptRuntime c)
        {
            if (!c) return null;
            var mod = c.addon.ModName.Trim();
            var str = mod.Length > 9 ? mod.Remove(8) + "…" : mod;
            return $"[UKAddon: {str}] ";
        }
    }
}