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
// using System.Net;
//
// namespace ULTRAKIT.Lua.API.Statics
// {
//     public class UKStaticFileLoader : UKStatic
//     {
//         public static readonly string AddonDataFolder = Directory.GetCurrentDirectory() + "/UltraKitAddonData";
//         
//         public override string name => "FileLoader";
//
//         private static string GetAddonFolder(Script script)
//         {
//             var addonName = ScriptToAddonConverter.GetAddonFromScript(script).ModName;
//             return $@"{AddonDataFolder}\{addonName}";
//         }
//
//         public string GetLocation()
//         {
//             return Directory.GetCurrentDirectory();
//         }
//
//         public string GetAllFileNames(Script script)
//         {
//             Debug.Log("Getting all file names now.");
//             
//             var str = "";
//             foreach (var file in Directory.EnumerateFiles(GetAddonFolder(script)))
//             {
//                 str += file;
//             }
//             
//             return str;
//         }
//         
//
//         public Texture2D LoadImage(Script script, string filePath)
//         {
//             Debug.Log("Getting " + filePath);
//             Debug.Log($@"{GetAddonFolder(script)}\{filePath}");
//             try
//             {
//                 Debug.Log(File.Exists($@"{GetAddonFolder(script)}\{filePath}"));
//                 
//                 var image = Image.FromFile($@"{GetAddonFolder(script)}\{filePath}");
//                 // var image = ByteArrayToImage(File.ReadAllBytes($@"{GetAddonFolder(script)}\{filePath}"));
//                 
//                 var texture2D = new Texture2D(image.Width, image.Height);
//                 texture2D.LoadImage(ImageToByteArray(image));
//                 return texture2D;
//             }
//             catch (Exception e)
//             {
//                 Debug.LogError(e);
//                 return null;
//             }
//         }
//
//         private Image ByteArrayToImage(byte[] byteArrayIn)
//         {
//             using (var ms = new MemoryStream(byteArrayIn))
//             {
//                 return Image.FromStream(ms);
//             }
//         }
//         
//         private byte[] ImageToByteArray(Image imageIn)
//         {
//             using (var ms = new MemoryStream())
//             {
//                 imageIn.Save(ms,imageIn.RawFormat);
//                 return ms.ToArray();
//             }
//         }
//     }
// }
