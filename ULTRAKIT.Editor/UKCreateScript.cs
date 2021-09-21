using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace ULTRAKIT.EditorScripts
{
    [ScriptedImporter(1, "lua")]
    public class UKImportLua : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var txt = File.ReadAllText(ctx.assetPath);
            ctx.AddObjectToAsset("Text Asset", new TextAsset(txt));
        }
    }

    public class UKCreateScript
    {
        [MenuItem("Assets/ULTRAKIT/New Lua Script")]
        public static void LuaScript(MenuCommand command)
        {
            string windowPath = GetWindowPath();
            var fileName = "script.lua";
            var finalPath = AssetDatabase.GenerateUniqueAssetPath($@"{windowPath}/{fileName}");

            AssetDatabase.StartAssetEditing();
            using(FileStream fs = File.Create(finalPath))
            {
                // THIS BLOCK IS NECESSARY TO ENSURE THAT THE FILESTREAM IS CLOSED BEFORE THE SCRIPTEDIMPORTER RUNS
            }
            AssetDatabase.StopAssetEditing();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/ULTRAKIT/New Addon Data")]
        public static void AddonData()
        {
            CreateObject<UKAddonData>("New Addon Data");
        }
        
        [MenuItem("Assets/ULTRAKIT/New Persistent Prefab")]
        public static void Prefab()
        {
            CreateObject<UKContentPersistent>("New Persistent Prefab");
        }
        
        [MenuItem("Assets/ULTRAKIT/New Weapon")]
        public static void Weapon()
        {
            CreateObject<UKContentWeapon>("New Persistent Prefab");
        }

        [MenuItem("Assets/ULTRAKIT/New Spawnable")]
        public static void SpawnableObject()
        {
            CreateObject<UKContentSpawnable>("New Spawnable");
        }

        static string GetWindowPath()
        {
            var res = AssetDatabase.GetAssetPath(Selection.activeObject);
            if(res == "")
            {
                return "Assets/";
            }

            return res;
        }

        static void CreateObject<T>(string name)
            where T : ScriptableObject
        {
            var obj = ScriptableObject.CreateInstance<T>();
            string windowPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            string finalPath = AssetDatabase.GenerateUniqueAssetPath($@"{windowPath}\{name}.asset");

            AssetDatabase.CreateAsset(obj, finalPath);
            AssetDatabase.Refresh();
            Selection.activeObject = obj;
        }
    }
}
