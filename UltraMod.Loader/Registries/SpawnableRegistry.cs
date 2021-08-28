using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMod.Data;
using UnityEngine;

namespace UltraMod.Loader.Registries
{
    class SpawnableRegistry
    {
        public static void Register(UltraModItem item)
        {
            var spawnable = ObjFromItem(item);
        }

        public static SpawnableObject ObjFromItem(UltraModItem item)
        {
            SpawnableObject a = new SpawnableObject();

            a.armOffset = Vector3.zero;
            a.armRotationOffset = Vector3.zero;
            a.menuOffset = Vector3.zero;
            a.backgroundColor = Color.white;

            a.gridIcon = item.Icon;

            a.objectName = item.Name;

            a.strategy = "";

            a.enemyType = EnemyType.MinosPrime;
            a.description = item.Desc;
            a.type = "Custom Spawnable";
            a.preview = new GameObject();
                
            
            return a;
        }
    }
}
