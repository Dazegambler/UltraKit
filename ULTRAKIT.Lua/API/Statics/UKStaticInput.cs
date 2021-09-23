using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Lua.API.Abstract;
using ULTRAKIT.Lua.Attributes;
using ULTRAKIT.Lua.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ULTRAKIT.Lua.API.Statics
{

    //TODO: actual keybind system
    public class UKLuaInput : UKStatic
    {
        public override string name => "Input";

        Dictionary<Script, List<UKBinding>> bindings = new Dictionary<Script, List<UKBinding>>();

        public float Scroll => Mouse.current.scroll.ReadValue().normalized.y;
        public UKBinding Bind(Script s, Table t)
        {
            var b = new UKBinding(new InputAction(), t.Get("Pressed"), t.Get("Released"));
            b.ActionState.Action.AddBinding(t.Get("Key").String);
            b.enabled = true;

            bindings[s].Add(b);

            return b;
        }

        public void BindSWEP(Script s, Table t)
        {
            var fireB = new UKBinding(new InputAction(), t.Get("FirePressed"), t.Get("FireReleased"));
            fireB.ActionState.Action.AddBinding("<Mouse>/leftButton");

            var altFireB = new UKBinding(new InputAction(), t.Get("AltFirePressed"), t.Get("AltFireReleased"));
            altFireB.ActionState.Action.AddBinding("<Mouse>/rightButton");

            bindings[s].Add(fireB);
            bindings[s].Add(altFireB);
        }

        [UKScriptConstructor]
        void Construct(UKScriptRuntime s)
        {
            bindings.Add(s.runtime, new List<UKBinding>());
        }

        [UKScriptDestructor]
        void Destruct(UKScriptRuntime s)
        {
            bindings.Remove(s.runtime);
        }

        [UKScriptUpdater]
        void Update(UKScriptRuntime s)
        {
            if (!s.initialized || !bindings.ContainsKey(s.runtime)) return;
            
            foreach (var b in bindings[s.runtime])
            {
                if (b.wasPressedThisFrame && b.PressedCallback.IsNotNil())
                {
                    s.FuzzyCall(b.PressedCallback);
                }

                if (b.wasCanceledThisFrame && b.ReleasedCallback.IsNotNil())
                {
                    s.FuzzyCall(b.ReleasedCallback);
                }
            }
        }
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
            set
            {
                if (value)
                {
                    this.ActionState.Action.Enable();
                }
                else
                {
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

    // Actual keybinds system
    public class UKLuaKeybinds : UKStatic
    {
        public override string name => "Keybinds";

        public readonly string Fire1 = "<Mouse>/leftButton";
        public readonly string Fire2 = "<Mouse>/rightButton";
    }
}
