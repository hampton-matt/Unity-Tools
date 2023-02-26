using System.IO;
using UnityEditor;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace Hampma.Tools
{
    public static class FolderTools
    {
        // FIRST SETUP
        [MenuItem("Hampma/Tools/Project Setup", false)]
        public static void CreateDefaultFolders()
        {
            if(!EditorUtility.DisplayDialog("Setup New Project",
                "Would you like to setup the default folders and files for a new project?",
                "Yes", "Cancel")) return;
            CreateDirectories("", "Editor", "Project", "Project/Features", "Project/Game");
            CreateDirectories("Project/Game", "Scenes", "Scripts", "Data");
            Refresh();
        }
        [MenuItem("Hampma/Tools/Project Setup", true)]
        public static bool CheckCreateDefaultFolders()
        {
            return !(IsValidFolder("Assets/Project") && IsValidFolder("Assets/Project/Features") && IsValidFolder("Assets/Project/Game"));
        }

        [MenuItem("Hampma/Tools/New Feature")]
        public static void CreateNewFeature()
        {
            CreateDirectories("Project/Features", "NewFeature");
            CreateDirectories("Project/Features/NewFeature", "Scripts", "Objects", "Scenes", "Art", "UI");
            Refresh();
        }

        public static void CreateDirectories(string root, params string[] dirs)
        {
            var fullpath = Path.Combine(dataPath, root);
            foreach (var dir in dirs)
            {
                Directory.CreateDirectory(Path.Combine(fullpath, dir));
            }
        }
    }
}