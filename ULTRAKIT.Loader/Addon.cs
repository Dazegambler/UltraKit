using MoonSharp.Interpreter;
using System.Linq;
using ULTRAKIT.Data;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using UnityEngine;

namespace ULTRAKIT.Loader
{
    public class Addon
    {
        public UKAddonData Data;

        bool _enabled;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (PersistentInjector.persistDict.ContainsKey(this))
                {
                    var persistents = PersistentInjector.persistDict[this];
                    foreach (var pfb in persistents)
                    {
                        pfb.SetActive(value);
                    }
                }

                _enabled = value;
            }
        }
        public string Path;
        public AssetBundle Bundle;
    }
}
