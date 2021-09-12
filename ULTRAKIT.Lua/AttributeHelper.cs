using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ULTRAKIT.Lua
{
    public static class AttributeHelper
    {

        /// <summary>
        /// Gets all types with a specific attribute in the current assembly
        /// </summary>
        /// <typeparam name="T">The type of the requested attribute</typeparam>
        /// <returns>A dictionary with the keys as the types and the value as the attributes theirselves</returns>
        public static Dictionary<Type, T> GetTypesWith<T>()
            where T : Attribute
        {
            var asm = Assembly.GetExecutingAssembly();
            var res = asm
                .GetTypes()
                .Where(t => t.GetCustomAttribute<T>() != null)
                .ToDictionary(t => t, t => t.GetCustomAttribute<T>());


            return res;
        }

        /// <summary>
        /// Gets all methods with a specific attribute in the current assembly
        /// </summary>
        /// <typeparam name="T">The type of the requested attribute</typeparam>
        /// <returns>A dictionary with the keys as the methods and the value as the attributes theirselves</returns>
        public static Dictionary<MethodInfo, T> GetMethodsWith<T>()
            where T : Attribute
        {
            var asm = Assembly.GetExecutingAssembly();
            var res = asm
                .GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttribute<T>() != null)
                .ToDictionary(m => m, m => m.GetCustomAttribute<T>());

            return res;
        }

        /// <summary>
        /// Gets all methods with a specific attribute in the specified type
        /// </summary>
        /// <typeparam name="T">The type of the requested attribute</typeparam>
        /// <returns>A dictionary with the keys as the methods and the value as the attributes theirselves</returns>
        public static Dictionary<MethodInfo, T> GetMethodsWith<T>(Type target)
            where T : Attribute
        {
            var asm = Assembly.GetExecutingAssembly();
            var res = target
                .GetMethods()
                .Where(m => m.GetCustomAttribute<T>() != null)
                .ToDictionary(m => m, m => m.GetCustomAttribute<T>());

            return res;
        }

        /// <summary>
        /// Gets all subclasses of a specific type in the executing assembly
        /// </summary>
        /// <param name="type">The base type to be searched</typeparam>
        /// <returns>A list of all types in the assembly that extend T</returns>
        public static List<Type> GetDerivedTypes(Type type)
        {
            var asm = Assembly.GetExecutingAssembly();
            var res = asm
                .GetTypes()
                .Where(t => t != type && !t.IsAbstract && IsSubclassOfRawGeneric(type, t))
                .ToList();


            return res;
        }

        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}
