using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Lua.Attributes;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace UltraMod.Lua.API
{
    [UKLuaStatic("Keybinds")]
    public enum UKLuaDefaultKeybinds
    {
        Fire1,
        Fire2
    }


    [UKLuaStatic("Binding")]
    public class UKLuaBinding
    {
        public static List<UKLuaBinding> bindings = new List<UKLuaBinding>();

        InputAction action;
        public Action Started, Ended;

        UKLuaBinding(UKLuaDefaultKeybinds bind)
        {
            bindings.Add(this);
            var insource = MonoSingleton<InputManager>.Instance.InputSource;
            switch (bind)
            {
                case UKLuaDefaultKeybinds.Fire1:
                    action = insource.Fire1.Action;
                    break;
                case UKLuaDefaultKeybinds.Fire2:
                    action = insource.Fire2.Action;
                    break;
                default:
                    return;
            }

            action.started += CallStarted;
            action.canceled += CallEnded;
        }

        void CallStarted(CallbackContext ctx)
        {
            Started.Invoke();
        }

        void CallEnded(CallbackContext ctx)
        {
            Ended.Invoke();
        }

        public static void Unbind(UKLuaBinding b)
        {
            b.action.started -= b.CallStarted;
            b.action.canceled -= b.CallEnded;
        }

        public static UKLuaBinding Bind(Script s, UKLuaDefaultKeybinds bind)
        {
            return new UKLuaBinding(bind);
        }
    }
}
