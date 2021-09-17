using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data;
using UnityEditor;
using UnityEngine;

namespace ULTRAKIT.EditorScripts
{
    [CustomEditor(typeof(UKAddonData))]
    public class UKAddonDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();
            if(GUILayout.Button("Export mod"))
            {
                //string assetBundleDirectory = "Assets/ExportTemp";
                //if (!Directory.Exists(Application.streamingAssetsPath))
                //{
                //    Directory.CreateDirectory(assetBundleDirectory);
                //}
                //BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

                var data = target as UKAddonData;
                var assetPath = UnityEditor.AssetDatabase.GetAssetPath(data.GetInstanceID());
                
                // Doesn't prompt user on export if export path field has a value
                string outputPath = data.ExportPath == null || data.ExportPath.Length == 0 
                    ? EditorUtility.SaveFilePanel("Export mod", "Assets", data.ModName, "") 
                    : data.ExportPath;
                
                // Saves the selected path to the field
                if (data.ExportPath == null || data.ExportPath.Length == 0)
                {
                    EditorUtility.SetDirty(target);
                    data.ExportPath = outputPath;
                }

                var assetLabel = UnityEditor.AssetDatabase.GetImplicitAssetBundleName(assetPath);
                BuildAssetBundleByName(assetLabel, outputPath);
            }
        }

        public static void BuildAssetBundleByName(string name, string outputPath)
        {
            var tempPath = @"Assets/_tempexport";
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }

            Directory.CreateDirectory(tempPath);


            var build = new AssetBundleBuild();
            build.assetBundleName = name;
            build.assetNames = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(name);

            BuildPipeline.BuildAssetBundles(tempPath, new AssetBundleBuild[] { build }, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

            File.Copy($@"{tempPath}/{name}", outputPath, true);

            Directory.Delete(tempPath, true);

            UnityEditor.AssetDatabase.Refresh();
            Debug.Log($@"Mod export successful at {outputPath}");
        }
    }
}
