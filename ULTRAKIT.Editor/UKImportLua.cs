using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string windowPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var fileName = "script.lua";
            int index = 2;
            while (File.Exists($"{windowPath}/{fileName}")) {
                fileName = $"script{index}.lua";
                index++;
            }

            var finalPath = $@"{windowPath}/{fileName}";

            AssetDatabase.StartAssetEditing();
            using(FileStream fs = File.Create(finalPath))
            {
                // THIS BLOCK IS NECESSARY TO ENSURE THAT THE FILESTREAM IS CLOSED BEFORE THE SCRIPTEDIMPORTER RUNS
            }
            AssetDatabase.StopAssetEditing();
            AssetDatabase.Refresh();
        }
    }
}
