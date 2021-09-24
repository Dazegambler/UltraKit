using System;
using MoonSharp.Interpreter;
using ULTRAKIT.Lua.API.Abstract;
using System.IO;
using System.Runtime.Remoting.Contexts;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{
    public class UKStaticFileLoader : UKStatic
    {
        public static readonly string AddonDataFolder = Directory.GetCurrentDirectory() + "\\Addons\\Resources";
        
        public override string name => "FileLoader";

        #region Helper methods
        private static string GetPath(ScriptExecutionContext ctx, string path) 
            => $@"{GetAddonFolder(ctx.OwnerScript)}\{path.Trim()}";
        
        private static string GetAddonFolder(Script script)
            => $@"{AddonDataFolder}\{script.GetAddon().ModName}\";

        private static ScriptRuntimeException NoAccessException =
            new ScriptRuntimeException("Trying to access path outside addon's data folder");
        #endregion

        // Implement later
        // public static string GetAllFileNames(Script script)
        // {
        //     Debug.Log("Getting all file names now.");
        //     
        //     var str = "";
        //     foreach (var file in Directory.EnumerateFiles(GetAddonFolder(script).ToString()))
        //     {
        //         str += file;
        //     }
        //     
        //     return str;
        // }
        
        private static bool ValidatePath(string path, Script script)
        {
            var addonFolder = new Uri(GetAddonFolder(script), UriKind.Absolute);
            var pathToFile = new Uri(addonFolder, path);
            var fileIsInAddonFolder = pathToFile.AbsolutePath.StartsWith(addonFolder.AbsolutePath);
            
            return fileIsInAddonFolder;
        }

        #region Text loading
        public static string LoadText(ScriptExecutionContext ctx, string path)
        {
            var filePath = GetPath(ctx, path);
            
            if (!ValidatePath(path.Trim(), ctx.OwnerScript))
            {
                ctx.LuaError(NoAccessException);
                return null;
            }

            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception e)
            {
                ctx.LuaError(e);
                return null;
            }
        }
        #endregion

        #region Image loading
        public static Texture2D LoadTexture(ScriptExecutionContext ctx, string path)
        {
            var filePath = GetPath(ctx, path);
            
            if (!ValidatePath(path.Trim(), ctx.OwnerScript))
            {
                ctx.LuaError(NoAccessException);
                return null;
            }
            
            try
            {
                var texture2D = new Texture2D(10, 10);
                texture2D.LoadImage(File.ReadAllBytes(filePath));
                return texture2D;
            }
            catch (Exception e)
            {
                // var e = ScriptRuntimeException.BadArgument(0, "LoadImage", $"File {path} does not exist");
                ctx.LuaError(e);
                return null;
            }
        }
        
        public static Sprite SpriteFromBase64(string base64)
        {
            var imageBytes = Convert.FromBase64String(base64);
            var tex = new Texture2D(2, 2);
            tex.LoadImage(imageBytes);
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            return sprite;
        }
        
        public static Texture2D Texture2DFromBase64(string base64)
        {
            var imageBytes = Convert.FromBase64String(base64);
            var tex = new Texture2D(1,1);
            tex.LoadImage(imageBytes);
            return tex;
        }

        public static Sprite LoadSprite(ScriptExecutionContext ctx, string path)
        {
            var img = LoadTexture(ctx, path.Trim());
            if (img is null) return null;
            var rect = new Rect(0, 0, img.width, img.height);
            return Sprite.Create(img, rect, Vector2.one/2);
        }
        #endregion
    }
}
