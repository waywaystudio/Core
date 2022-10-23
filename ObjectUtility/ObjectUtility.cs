#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Wayway.Engine
{
    /// <summary>
    /// Object Utility Class. Only for in Editor Condition
    /// </summary>
    public static class ObjectUtility
    {
        /// <summary>
        /// Get Object "Only" Name without Path
        /// </summary>
        /// <param name="directory">Searching FolderPath</param>
        /// <param name="defaultName">Set Default Name</param>
        /// <returns>FileName</returns>
        public static string GetUniqueName(string directory, string defaultName)
        {
            var uniqueID = GetUniqueNameWithPath(directory, defaultName);
            uniqueID = uniqueID.Remove(0, directory.Length + 1);

            return uniqueID;
        }

        /// <summary>
        /// Get Object Unique name, with index;
        /// ex. Assets/Project/File -> File 01, File 02...
        /// </summary>
        /// <param name="directory">Searching Folder Path, ex Assets/Project</param>
        /// <param name="defaultName">Set Default Name, ex FileName (if isExist, return FileName 01)</param>
        /// <returns>Unique Name with path ex Assets/Project/FileName 01.asset</returns>
        public static string GetUniqueNameWithPath(string directory, string defaultName)
        {
            return AssetDatabase.GenerateUniqueAssetPath($"{directory}/{defaultName}.asset");
        }

        /// <summary>
        /// Get Type Class Path
        /// </summary>
        /// <typeparam name="T">Any of Class</typeparam>
        /// <param name="includeFileName">Return include FileName</param>
        /// <returns>ex Assets/Project/FileName.asset</returns>
        public static string GetObjectPath<T>(bool includeFileName) where T : class
        {
            var gUid = AssetDatabase.FindAssets($"t:{typeof(T).FullName}").First();
            var assetPath = AssetDatabase.GUIDToAssetPath(gUid);

            return includeFileName ?
                assetPath :
                assetPath.Remove(assetPath.LastIndexOf('/') + 1, assetPath.Length - (assetPath.LastIndexOf('/') + 1));
        }

        /// <summary>
        /// Get Type Class Name, without Path
        /// </summary>
        /// <typeparam name="T">Any of Class</typeparam>
        /// <param name="includeExtension">include fileExtension ex .asset .meta .cs</param>
        /// <returns>FileName</returns>
        public static string GetObjectName<T>(bool includeExtension)
        {
            var gUid = AssetDatabase.FindAssets($"t:{typeof(T).FullName}").First();
            var assetPath = AssetDatabase.GUIDToAssetPath(gUid);

            return includeExtension ?
                assetPath.Remove(0, assetPath.LastIndexOf('/') + 1) :
                assetPath.Remove(0, assetPath.LastIndexOf('/') + 1).Replace(".asset", "");
        }

        public static GameObject GetPrefabInclude<T>(string directory = "Assets/") where T : MonoBehaviour
        {
            var gUidList = AssetDatabase.FindAssets($"t:GameObject,", new[] {directory});

            return gUidList.Select(AssetDatabase.GUIDToAssetPath)
                           .Select(assetPath => AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject)
                           .FirstOrDefault(asset => asset != null && asset.GetComponent<T>() != null);
        }

        public static string GetPrefabPathInclude<T>(string directory = "Assets/") where T : MonoBehaviour
        {
            var prefab = GetPrefabInclude<T>();

            return AssetDatabase.GetAssetPath(prefab);
        }

        public static void Save(Object target)
        {
            var targetObject = GetObjectName<GameObject>(false);
            
            EditorUtility.SetDirty(target);
        }

        public static void Refresh()
        {
            AssetDatabase.Refresh();
        }
    }
}
#endif