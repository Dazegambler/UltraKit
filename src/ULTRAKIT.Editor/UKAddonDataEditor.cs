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
        static string outputPath = "";

        public override void OnInspectorGUI()
        {
            var data = target as UKAddonData;
            var assetPath = UnityEditor.AssetDatabase.GetAssetPath(data.GetInstanceID());

            EditorGUILayout.LabelField("ADDON DATA", EditorStyles.boldLabel);
            DrawDefaultInspector();
            EditorGUILayout.Space(20);

            EditorGUILayout.LabelField("EXPORT", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();

            // Doesn't prompt user on export if export path field has a value
            outputPath = EditorGUILayout.TextField(outputPath);
            if (GUILayout.Button("Browse"))
            {
                outputPath = EditorUtility.SaveFilePanel("Export mod", "Assets", data.ModName, "ukaddon");
            }
            EditorGUILayout.EndHorizontal();


            if(GUILayout.Button("Export mod"))
            {   
                if(outputPath == null || outputPath.Length == 0)
                {
                    EditorUtility.DisplayDialog("Invalid Path", "Please choose a valid export directory", "Ok");
                    return;
                }
                

                var assetLabel = UnityEditor.AssetDatabase.GetImplicitAssetBundleName(assetPath);
                var buildFailed = !BuildAssetBundleByName(assetLabel, outputPath);
                if (buildFailed) Debug.LogError("Mod export failed.");
            }
        }

        // Rerturns true on success
        public static bool BuildAssetBundleByName(string name, string outputPath)
        {
            if (File.Exists(outputPath))
            {
                if (!EditorUtility.DisplayDialog("Replace File", $@"File '{Path.GetFileName(outputPath)}' already exists. Would you like to replace it?", "Yes", "No"))
                {
                    return false;
                }
            }

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

            try
            {
                File.Copy($@"{tempPath}/{name}", outputPath, true);
            }
            catch (ArgumentException)
            {
                Debug.LogError("Assetbundle has no name. Did you forget to assign one to the folder in the editor?");
                return false;
            }

            Directory.Delete(tempPath, true);

            UnityEditor.AssetDatabase.Refresh();
            Debug.Log($@"Mod export successful at {outputPath}");
            return true;
        }
    }
}
