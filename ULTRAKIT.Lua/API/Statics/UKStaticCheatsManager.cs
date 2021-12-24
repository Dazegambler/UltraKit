using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using ULTRAKIT.Lua.API.Abstract;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{

    public class UKStaticCheatsManager : UKStatic
    {
        public override string name => "CheatsManager";

        public bool exists => MonoSingleton<CheatsController>.Instance != null;
        private CheatsManager cheatsManager => MonoSingleton<CheatsManager>.Instance;
        private Dictionary<String, ICheat> cheats => MonoSingleton<CheatsManager>.Instance.GetPrivate<Dictionary<String, ICheat>>("idToCheat");

        private List<string> whiteList = new List<string> {
            "ultrakill.sandbox.rebuild-nav",
            "ultrakill.full-bright",
            "ultrakill.noclip",
            "ultrakill.flight",
            "ultrakill.infinite-wall-jumps",
            "ultrakill.no-weapons-cooldown",
            "ultrakill.infinite-power-ups",
            "ultrakill.blind-enemies",
            "ultrakill.disable-enemy-spawns",
            "ultrakill.invincible-enemies",
            "ultrakill.sandbox.crash-mode"
            };

        public void PrintCheatIDs()
        {
            foreach (KeyValuePair<String, ICheat> entry in cheats)
            {
                Debug.Log(entry.Key);
            }
        }

        public bool SetCheatState(string id, bool enabled)
        {
            if (cheats.ContainsKey(id))
            {
                if (whiteList.Contains(id))
                {
                    cheatsManager.WrappedSetState(cheats[id], enabled);
                    cheatsManager.UpdateCheatState(cheats[id]);
                    return (cheats[id].IsActive);
                }
                else
                {
                    Debug.LogWarning("Cheat '" + id + "' is not whitelisted");
                    return (false);
                }
            }
            else
            {
                Debug.LogWarning("No cheat '" + id + "' exists");
                return (false);
            }
        }

        public bool GetCheatState(string id)
        {
            if (cheats.ContainsKey(id))
            {
                return (cheats[id].IsActive);
            }
            else
            {
                Debug.LogWarning("No cheat '" + id + "'exists");
                return (false);
            }
        }

        public void AddToWhitelist(string longName)
        {
            string id = "ultrakit." + longName;
            if (!whiteList.Contains(id))
            {
                whiteList.Add(id);
            }
            else
            {
                Debug.LogWarning("No cheat '" + id + "'exists");
            }
        }
    }
}
