using ULTRAKIT.Lua.API.Abstract;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{
    class UKStaticObject : UKStatic
    {
        public override string name => "Object";

        public void Destroy(Object obj) => Object.Destroy(obj);
        public void Destroy(Object obj, float t) => Object.Destroy(obj, t);
        // DestroyImmediate, DontDestroyOnLoad, FindObjectOfType, FindObjectsOfType
        public Object Instantiate(Object original) => Object.Instantiate(original);
        public Object Instantiate(Object original, Transform parent) => Object.Instantiate(original, parent);
        public Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace) => Object.Instantiate(original, parent, instantiateInWorldSpace);
        public Object Instantiate(Object original, Vector3 position, Quaternion rotation) => Object.Instantiate(original, position, rotation);
        public Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent) => Object.Instantiate(original, position, rotation, parent);
    }
}
