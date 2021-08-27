using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UltraMod.Data
{
    [CreateAssetMenu(fileName = "UltraModItem", menuName = "UltraMod/ModData")]
    public class UltraModData : ScriptableObject
    {
        public string ModName;
        public string Author;
        [TextArea]
        public string ModDesc;
    }
}