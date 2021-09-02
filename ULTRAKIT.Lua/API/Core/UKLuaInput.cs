using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Lua.Attributes;
using ULTRAKIT.Lua.Components;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace ULTRAKIT.Lua.API
{
    [UKLuaStatic("Keybinds")]
    public static class UKLuaKeybinds
    {
        public static readonly string Fire1 = "<Mouse>/leftButton";
        public static readonly string Fire2 = "<Mouse>/rightButton";
    }

    
    [MoonSharpUserData]
    public class UKBinding
    {
        public bool wasPressedThisFrame => ActionState.WasPerformedThisFrame;
        public bool wasCanceledThisFrame => ActionState.WasCanceledThisFrame;
        public bool isHeld => ActionState.HoldTime > 0;
        public float holdTime => ActionState.HoldTime;
        
        
        public bool enabled
        {
            get => this.ActionState.Action.enabled;
            set {
                if (value) {
                    this.ActionState.Action.Enable();
                } else {
                    this.ActionState.Action.Disable();
                } 
            }
        }

        [MoonSharpHidden]
        public UKBinding(InputAction action, DynValue pressedCallback, DynValue releasedCallback)
        {
            this.ActionState = new InputActionState(action);
            this.PressedCallback = pressedCallback;
            this.ReleasedCallback = releasedCallback;
        }

        [MoonSharpHidden]
        public InputActionState ActionState;
        [MoonSharpHidden]
        public DynValue PressedCallback;
        [MoonSharpHidden]
        public DynValue ReleasedCallback;
    }

    //TODO: IsDown method that checks for Keybind
    [UKLuaStatic("Input")]
    public static class UKLuaInput
    {
        public static Dictionary<Script, List<UKBinding>> bindings = new Dictionary<Script, List<UKBinding>>();

        [UKScriptConstructor]
        static void Construct(UKScriptRuntime s)
        {
            bindings.Add(s.runtime, new List<UKBinding>());
        }

        [UKScriptDestructor]
        static void Destruct(UKScriptRuntime s)
        {
            bindings.Remove(s.runtime);
        }

        [MoonSharpHidden]
        //update attribute
        public static void Update(UKScriptRuntime s)
        {
            foreach(var b in bindings[s.runtime])
            {
                if (b.wasPressedThisFrame && b.PressedCallback.IsNotNil())
                {
                    s.FuzzyCall(b.PressedCallback);
                }

                if (b.wasCanceledThisFrame && b.PressedCallback.IsNotNil())
                {
                    s.FuzzyCall(b.ReleasedCallback);
                }
            }
        }

        public static float Scroll => Mouse.current.scroll.ReadValue().y;

        public static UKBinding Bind(Script s, Table t)
        {
            var b = new UKBinding(new InputAction(), t.Get("Pressed"), t.Get("Released"));
            b.ActionState.Action.AddBinding(t.Get("Key").String);
            b.enabled = true;
            bindings[s].Add(b);

            Debug.Log(b.ActionState.PerformedFrame);

            return b;
        }
    }
}
