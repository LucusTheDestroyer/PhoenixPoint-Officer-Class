﻿using Base.Defs;
using HarmonyLib;
using I2.Loc;
using Newtonsoft.Json;
using PhoenixPoint.Modding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UsefulMethods
{
    internal class Helper
    {
        internal static string ModDirectory;
        internal static string TexturesDirectory;
        internal static string LocalizationDirectory;
        internal static ModMain ModInstance;


        public static void Initialize(ModMain instance)
        {
                ModInstance = instance;
                ModDirectory = instance.Instance.Entry.Directory;
                LocalizationDirectory = Path.Combine(ModDirectory, "Assets", "Localization");
                TexturesDirectory = Path.Combine(ModDirectory, "Assets", "Textures");
        }

        /// <summary>
        /// Copy fields of two objects of the same or derived classes by using reflection
        /// </summary>
        /// <param name="src">The source object</param>
        /// <param name="dst">The destination object, can be an instance of a derived class of the source, all additional fields are skipped</param>
        /// <param name="bindFlags"></param>
        public static void CopyFieldsByReflection(object src, object dst, BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            Type srcType = src.GetType();
            foreach (FieldInfo dstFieldInfo in dst.GetType().GetFields(bindFlags))
            {
                FieldInfo srcField = srcType.GetField(dstFieldInfo.Name, bindFlags);
                if (srcField != null && srcField.Name != "Guid")
                {
                    dstFieldInfo.SetValue(dst, srcField.GetValue(src));
                }
            }
        }

        // Read localization from CSV file
        public static void AddLocalizationFromCSV(string LocalizationFileName, string Category = null)
        {
            try
            {
                string CSVstring = File.ReadAllText(Path.Combine(LocalizationDirectory, LocalizationFileName));
                if (!CSVstring.EndsWith("\n"))
                {
                    CSVstring += "\n";
                }
                LanguageSourceData SourceToChange = Category == null ? // if category is not given
                    LocalizationManager.Sources[0] :                   // use fist language source
                    LocalizationManager.Sources.First(source => source.GetCategories().Contains(Category));
                if (SourceToChange != null)
                {
                    int numBefore = SourceToChange.mTerms.Count;
                    _ = SourceToChange.Import_CSV(string.Empty, CSVstring, eSpreadsheetUpdateMode.AddNewTerms, ',');
                    LocalizationManager.LocalizeAll(true);    // Force localing all enabled labels/sprites with the new data
                    int numAfter = SourceToChange.mTerms.Count;
                    ModInstance.Logger.LogInfo($"Added {numAfter - numBefore} terms from {LocalizationFileName} in localization source {SourceToChange}, category: {Category}");
                }
                else
                {
                    ModInstance.Logger.LogInfo($"No language source with category {Category} found!");
                }                
            }
            catch (Exception e)
            {
                ModInstance.Logger.LogInfo($"Localisation Import failed: {e}");
            }
        }
        

        public static Sprite CreateSpriteFromImageFile(string imageFileName, int width = 128, int height = 128, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipChain = true)
        {
            try
            {
                string filePath = Path.Combine(TexturesDirectory, imageFileName);
                byte[] data = File.Exists(filePath) ? File.ReadAllBytes(filePath) : throw new FileNotFoundException("File not found: " + filePath);
                Texture2D texture = new Texture2D(width, height, textureFormat, mipChain);
                return ImageConversion.LoadImage(texture, data)
                    ? Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.0f, 0.0f))
                    : null;
            }
            catch (Exception e)
            {
                ModInstance.Logger.LogInfo($"Creation of Image from File failed: {e}");
                return null;
            }
        }  
    }
}
