using System;

namespace ULTRAKIT.Lua.Attributes
{

    /// <summary>
    /// Call this method whenever a UKScript has been created
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UKScriptConstructor : Attribute
    {
    }

    /// <summary>
    /// Call this method whenever a UKScript has been destroyed
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UKScriptDestructor : Attribute
    {
    }

    /// <summary>
    /// Call this method whenever a UKScript is updated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UKScriptUpdater : Attribute
    {
    }
}
