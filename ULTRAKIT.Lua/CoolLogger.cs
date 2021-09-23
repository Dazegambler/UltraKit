using System;
using System.Runtime.CompilerServices;
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
        private const string db_str = "[Debug  :  Ultrakit] ";
        private static Action<string, ConsoleColor> wrt = BCE.console.Write;
        
        public static void Log(string msg, UKScriptRuntime c = null)
        {
            wrt(ad_str(c) ?? in_str, c ? ConsoleColor.Blue : ConsoleColor.White);
            wrt(msg+nl, ConsoleColor.Gray);
        }
        
        public static void LogWarning(string msg, UKScriptRuntime c = null)
        {
            wrt(ad_str(c) ?? wr_str, ConsoleColor.Yellow);
            wrt(msg+nl, ConsoleColor.Yellow);
        }
        
        public static void LogError(string msg, UKScriptRuntime c = null)
        {
            wrt(ad_str(c) ?? er_str, ConsoleColor.DarkRed);
            wrt(msg+nl, ConsoleColor.Red);
        }

        public static void LogError(InterpreterException e, UKScriptRuntime c, string t, UKScript d)
        {
            LogError($"{t} in script \"{d.sourceCode.name}\": {e.DecoratedMessage ?? e.Message}", c);
        }

        public static void AAA(string m = "AAA",
            [CallerFilePath]   string f = "",
            [CallerMemberName] string n = "",
            [CallerLineNumber] int l = 0)
        {
            var c1 = ConsoleColor.White;
            var c2 = ConsoleColor.Green;
            wrt(db_str, c1);
            wrt(m, c2);
            wrt(", says ", c1);
            wrt(n, c2);
            wrt(" at ", c1);
            wrt($"line {l}", c2);
            wrt(" of ", c1);
            wrt(f+nl, c2);
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