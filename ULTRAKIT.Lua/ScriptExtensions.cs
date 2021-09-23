using MoonSharp.Interpreter;
using System.Linq;
using ULTRAKIT.Data;
using ULTRAKIT.Lua.Components;

namespace ULTRAKIT.Lua
{
    public static class ScriptExtensions
    {
        public static UKAddonData GetAddon(this Script script) => UKScriptRuntime.Instances.First(t => t.runtime == script).addon;
        public static UKScriptRuntime GetRuntime(this Script script) => UKScriptRuntime.Instances.First(t => t.runtime == script);

        public static void LuaError(this UKScriptRuntime runtime, ScriptRuntimeException e) =>
            Debug.LogError(e, runtime, "RUNTIME ERROR", runtime.data);

        public static void LuaError(this UKScriptRuntime runtime, SyntaxErrorException e) =>
            Debug.LogError(e, runtime, "SYNTAX ERROR", runtime.data);

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