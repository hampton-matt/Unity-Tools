#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace Hampma.Tools
{
    public class EnumTools
    {
        [MenuItem("Hampma/Enums/Generate Scenes", false)]
        public static void GenerateScenes()
        {
            WriteEnumsToFile(GetFileNames(GetFilePaths("t:scene")), "Project/Game/Data",
                "E_Scene.cs", "Scene");
            Refresh();
        }
        [MenuItem("Hampma/Enums/Generate Scenes", true)]
        public static bool ValidateGenerateScenes()
        {
            return IsValidFolder("Assets/Project/Game/Data");
        }

        [MenuItem("Hampma/Enums/Clear Scenes", false)]
        public static void ClearScenes()
        {
            WriteEnumsToFile(new string[0], "Project/Game/Data", "E_Scene.cs", "Scene");
            Refresh();
        }

        // HELPERS
        private static string[] GetFilePaths(string filters, string[] folders = null)
        {
            string[] guids = FindAssets(filters, folders);
            string[] filePaths = new string[guids.Length];
            for (int i = 0; i < guids.Length; i++) filePaths[i] = GUIDToAssetPath(guids[i]);
            return filePaths;
        }

        private static string[] GetFileNames(string[] filePaths)
        {
            string[] names = new string[filePaths.Length];
            for (int i = 0; i < filePaths.Length; i++)
            {
                string[] dirs = filePaths[i].Split('/');
                string[] fileParts = dirs[dirs.Length - 1].Split('.');
                names[i] = fileParts[0];
            }
            return names;
        }

        private static void WriteEnumsToFile(string[] enums, string path, string fileName, string enumName, bool addDataPath = true)
        {
            string _path = addDataPath ? Path.Combine(dataPath, path) : path;
            string _filePath = Path.Combine(_path, fileName);
            ClearFile(_filePath);
            if (Directory.Exists(_path))
            {
                using (StreamWriter streamWriter = new StreamWriter(_filePath))
                {
                    streamWriter.WriteLine("namespace Hampma.Core {");
                    streamWriter.WriteLine("\tpublic enum " + enumName);
                    streamWriter.WriteLine("\t{");
                    streamWriter.WriteLine("\t\tNone = 0,");
                    for (int i = 0; i < enums.Length; i++)
                    {
                        streamWriter.WriteLine("\t\t" + enums[i] + " = " + enums[i].GetHashCode().ToString() + ",");
                    }
                    streamWriter.WriteLine("\t}");
                    streamWriter.WriteLine("}");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Invalid Folder Structure",
                    "Could not find folder - " + _path, "OK");
            }
        }

        private static void ClearFile(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }
    }
}
#endif