//#if UNITY_EDITOR
using System.IO;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static UnityEditor.AssetDatabase;

namespace Hampma.Tools
{
    public static class ScriptGenerators
    {
        [MenuItem("Hampma/Scripts/Boot Manager", false)]
        public static void GenerateBootManager()
        {
            GenerateFile("S_BootManager.cs", OpenPath());
            Refresh();
        }

        [MenuItem("Hampma/Scripts/Boot Manager", true)]
        public static bool ValidateGenerateBootManager()
        {
            string file = Path.Combine(OpenPath(), "S_BootManager.cs");
            return !File.Exists(file);
        }

        [MenuItem("Hampma/Scenes/Boot", false)]
        public static void GenerateBootScene()
        {
            GenerateFile("S_Boot.unity", OpenPath());
            Refresh();
        }

        [MenuItem("Hampma/Scenes/Boot", true)]
        public static bool ValidateGenerateBootScene()
        {
            string file = Path.Combine(OpenPath(), "S_Boot.unity");
            return !File.Exists(file);
        }


        // HELPERS
        private static void GenerateFile(string file, string installPath)
        {
            string source = Path.Combine("Packages/com.hampma.tools/Resources", file + ".txt");
            if (!File.Exists(source)) 
            {
                Debug.Log("ScriptGenerators: \t Error \t " + source + " does not exist");
                return;
            }
            string install = Path.Combine(installPath, file);
            File.Copy(source, install);
        }

        private static string OpenPath()
        {
            Type projectWindowUtilType = typeof(ProjectWindowUtil);
            MethodInfo getActiveFolderPath = projectWindowUtilType.GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);
            object obj = getActiveFolderPath.Invoke(null, new object[0]);
            return obj.ToString();
        }
    }
}
//#endif