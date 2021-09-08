using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using UnityEditor;
using UnityEngine;

namespace ULTRAKIT.Editor
{
    public class CreateAssets
    {
        [MenuItem("Assets/ULTRAKIT/Create Addon")]
        public static void CreateAddon()
        {
            //NONE OF THE COMMENTS WORK IF YOU MANAGE TO GET IT WORKING LET ME KNOW
            //Data.UKAddonData ModData = Addon();
            ///Data.UKAddonData.CreateInstance<Data.UKAddonData>();
            ///AssetDatabase.CreateAsset(ModData, $"Assets/{ModData.name}");
        }
    }
}
