using System.IO;
using UnityEditor;
using UnityEngine;

namespace Wayway.Engine
{
    public static class FileUtility
    {
        public static bool FindFile(string directory, string fileName)
        {
            return File.Exists($"{directory}/{fileName}");
        }
    }
}
