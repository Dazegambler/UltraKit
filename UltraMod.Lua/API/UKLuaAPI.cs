using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Lua.API.Proxies;
using UltraMod.Lua.API.Proxies.Component;
using UltraMod.Lua.Attributes;
using UltraMod.Lua.Components;
using UnityEngine;

namespace UltraMod.Lua.API
{
    public class UKLuaAPI
    {
        public static Dictionary<string, System.Type> staticDict = new Dictionary<string, System.Type>();
        public static List<MethodInfo> 
            constructionMethods = new List<MethodInfo>(), 
            destructionMethods = new List<MethodInfo>();

        public static void Initialize()
        {
            ScriptHelper.RegisterSimpleAction();
            UserData.RegisterAssembly();

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var stat = type.GetCustomAttribute<UKLuaStaticAttribute>();
                if (stat != null)
                {
                    UserData.RegisterType(type);
                    staticDict.Add(stat.luaName, type);
                }

                foreach(var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static))
                {
                    //TODO: update callback to move all code away from ukscriptruntime
                    var con = method.GetCustomAttribute<UKScriptConstructor>();
                    if (con != null)
                    {
                        constructionMethods.Add(method);
                    }

                    var des = method.GetCustomAttribute<UKScriptConstructor>();
                    if (des != null)
                    {
                        destructionMethods.Add(method);
                    }
                }
            }

            Debug.Log(constructionMethods.Count);

            //TODO: proxy these
            RegisterValueType<Vector3>();

            //TODO: AUTOMATE WITH A PROXY ATTRIBUTE
            //https://stackoverflow.com/questions/293905/reflection-getting-the-generic-arguments-from-a-system-type-instance
            //call method with reflection so you can use runtime types as the type arguments!!!!
            RegisterProxy<UKLuaGameObject, GameObject>();
            RegisterProxy<UKLuaRigidbody, Rigidbody>();
            RegisterProxy<UKLuaTransform, Transform>();
            RegisterProxy<UKLuaEnemy, EnemyIdentifier>();
            RegisterProxy<UKLuaLineRenderer, LineRenderer>();
            RegisterProxy<UKLuaAudioSource, AudioSource>();
        }

        public static void ConstructScript(UKScriptRuntime script)
        {
            
            foreach (var pair in staticDict)
            {
                script.runtime.Globals[pair.Key] = pair.Value;
            }
            
            foreach (var method in constructionMethods)
            {
                
                method.Invoke(null, new object[] { script });
            }
        }

        public static void DestructScript(UKScriptRuntime script) {
            foreach (var method in constructionMethods)
            {
                method.Invoke(null, new object[] { script });
            }
        }

        //TEMP
        public static void RegisterValueType<T>()
        {
            UserData.RegisterType<Vector3>();
            staticDict.Add(typeof(T).Name, typeof(T));
        }

        // holy shit
        public static void RegisterProxy<P, T>()
            where T : class
            where P : UKLuaProxy<T>
        {
            UserData.RegisterProxyType<P, T>((o) => (P)Activator.CreateInstance(typeof(P), o));
        }
    }
}
