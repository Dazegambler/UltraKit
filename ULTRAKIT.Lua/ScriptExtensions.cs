using System;
using MoonSharp.Interpreter;
using System.Linq;
using ULTRAKIT.Data;
using ULTRAKIT.Data.Components;
using ULTRAKIT.Lua.Components;

namespace ULTRAKIT.Lua
{
    public static class ScriptExtensions
    {
        public static UKScriptRuntime GetRuntime(this Script script) => UKScriptRuntime.Instances.First(t => t.runtime == script);
        public static UKScriptRuntime GetRuntime(this ScriptExecutionContext ctx) => ctx.OwnerScript.GetRuntime();
        
        public static UKAddonData GetAddon(this Script script) => script.GetRuntime().addon;
        public static UKAddonData GetAddon(this ScriptExecutionContext ctx) => ctx.OwnerScript.GetAddon();
        
        public static UKScript GetUKScript(this Script script) => script.GetRuntime().data;
        public static UKScript GetUKScript(this ScriptExecutionContext ctx) => ctx.GetRuntime().data;
        
        public static void LuaError(this UKScriptRuntime runtime, InterpreterException e) => Debug.LogException(e, runtime);
        public static void LuaError(this ScriptExecutionContext ctx, InterpreterException e) => Debug.LogException(e, ctx);
        public static void LuaError(this ScriptExecutionContext ctx, Exception e) => Debug.LogException(e, ctx);

        // public static void LuaError(this UKScriptRuntime runtime, ScriptRuntimeException e) =>
            // Debug.LogError(e, runtime, "RUNTIME ERROR");

        // public static void LuaError(this UKScriptRuntime runtime, SyntaxErrorException e) =>
            // Debug.LogError(e, runtime, "SYNTAX ERROR");

        // Unused functions. Remove?
        // public static void RegisterSimpleFunc<T>()
        // {
        //     Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T>),
        //         v =>
        //         {
        //             var function = v.Function;
        //             return (Func<T>)(() => function.Call().ToObject<T>());
        //         }
        //     );
        // }
        //
        //
        // public static void RegisterSimpleFunc<T1, TResult>()
        // {
        //     Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function,
        //         typeof(Func<T1, TResult>),
        //         v =>
        //         {
        //             var function = v.Function;
        //             return (Func<T1, TResult>)((T1 p1) => function.Call(p1).ToObject<TResult>());
        //         }
        //     );
        // }
        //
        // public static void RegisterSimpleAction<T>()
        // {
        //     Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action<T>),
        //         v =>
        //         {
        //             var function = v.Function;
        //             return (Action<T>)(p => function.Call(p));
        //         }
        //     );
        // }
        //
        // public static void RegisterSimpleAction()
        // {
        //     Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action),
        //         v =>
        //         {
        //             var function = v.Function;
        //             return (Action)(() => function.Call());
        //         }
        //     );
        // }
    }
}