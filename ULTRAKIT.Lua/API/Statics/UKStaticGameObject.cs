using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{
    class UKStaticGameObject : UKStaticObject
    {
        public override string name => "GameObject";

        //TODO: CreatePrimitive
        public static GameObject Find(string name) => GameObject.Find(name);
        public static GameObject[] FindGameObjectsWithTag(string tag) => GameObject.FindGameObjectsWithTag(tag);
        public static GameObject FindWithTag(string tag) => GameObject.FindWithTag(tag);
    }
}
