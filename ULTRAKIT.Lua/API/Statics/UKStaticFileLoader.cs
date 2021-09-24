using System;
using System.Linq;
using MoonSharp.Interpreter;
using ULTRAKIT.Lua.API.Abstract;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace ULTRAKIT.Lua.API.Statics
{
    public class UKStaticFileLoader : UKStatic
    {
        public static readonly string AddonDataFolder = Directory.GetCurrentDirectory() + "\\UltraKitAddonData";
        
        public override string name => "FileLoader";

        private static string GetAddonFolder(Script script)
        {
            var addonName = script.GetAddon().ModName;
            return $@"{AddonDataFolder}\{addonName}";
        }

        public static string GetLocation()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string GetAllFileNames(Script script)
        {
            Debug.Log("Getting all file names now.");
            
            var str = "";
            foreach (var file in Directory.EnumerateFiles(GetAddonFolder(script)))
            {
                str += file;
            }
            
            return str;
        }

        public static Texture2D LoadImage(ScriptExecutionContext ctx, string path)
        {
            var filePath = $@"{GetAddonFolder(ctx.OwnerScript)}\{path}";

            try
            {
                var texture2D = new Texture2D(10, 10);
                texture2D.LoadImage(File.ReadAllBytes(filePath));
                return texture2D;
            }
            catch (FileNotFoundException e)
            {
                // var e = ScriptRuntimeException.BadArgument(0, "LoadImage", $"File {path} does not exist");
                ctx.LuaError(e);
            }
            catch (InterpreterException e)
            {
                ctx.LuaError(e);
            }

            return null;
        }

        private static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }
        
        private static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms,imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
