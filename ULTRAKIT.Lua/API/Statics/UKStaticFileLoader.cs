using System;
using MoonSharp.Interpreter;
using ULTRAKIT.Lua.API.Abstract;
using System.IO;
using UnityEngine;

namespace ULTRAKIT.Lua.API.Statics
{
    public class UKStaticFileLoader : UKStatic
    {
        public static readonly string AddonDataFolder = Directory.GetCurrentDirectory() + "\\UltraKitAddonData";
        public static readonly Uri AddonDataFolderUri = new Uri(AddonDataFolder, UriKind.Absolute);
        
        public override string name => "FileLoader";

        private static string GetAddonFolder(Script script)
        {
            var addonName = script.GetAddon().ModName;
            return $@"{AddonDataFolder}\{addonName}\";
        }

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

        #region Image loading
        private static Texture2D LoadTexture(ScriptExecutionContext ctx, string path)
        {
            var filePath = $@"{GetAddonFolder(ctx.OwnerScript)}\{path}";
            
            if (!ValidatePath(path, ctx.OwnerScript))
            {
                ctx.LuaError(new ScriptRuntimeException("Trying to access path outside addon's data folder"));
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
            }

            return null;
        }
        
        public Sprite SpriteFromBase64(string base64)
        {
            var imageBytes = Convert.FromBase64String(base64);
            var tex = new Texture2D(2, 2);
            tex.LoadImage(imageBytes);
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            return sprite;
        }
        
        public Texture2D Texture2DFromBase64(string base64)
        {
            var imageBytes = Convert.FromBase64String(base64);
            var tex = new Texture2D(2, 2);
            tex.LoadImage(imageBytes);
            return tex;
        }

        public Sprite LoadSprite(ScriptExecutionContext ctx, string path)
        {
            var img = LoadTexture(ctx, path.Trim());
            if (img is null) return null;
            var rect = new Rect(0, 0, img.width, img.height);
            return Sprite.Create(img, rect, Vector2.one/2);
        }
        
        public Texture2D LoadImage(ScriptExecutionContext ctx, string path)
        {
            var img = LoadTexture(ctx, path.Trim());
            if (img is null) return null;
            return img;
        }
        #endregion
    }
}
