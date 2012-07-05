using System.IO;

namespace Fabrik.Common
{
    /// <summary>
    /// Utility methods for working with resource paths
    /// </summary>
    public static class PathUtils
    {
        /// <summary>
        /// Makes a filename safe for use within a URL
        /// </summary>
        public static string MakeFileNameSafeForUrls(string fileName)
        {
            Ensure.Argument.NotNullOrEmpty(fileName, "fileName");
            var extension = Path.GetExtension(fileName);
            var safeFileName = Path.GetFileNameWithoutExtension(fileName).ToSlug();
            return Path.Combine(Path.GetDirectoryName(fileName), safeFileName + extension);
        }

        /// <summary>
        /// Combines two URL paths
        /// </summary>
        public static string CombinePaths(string path1, string path2)
        {
            Ensure.Argument.NotNull(path1, "path1");
            Ensure.Argument.NotNull(path2, "path2");
            
            if (path2.IsNullOrEmpty())
                return path1;

            if (path1.IsNullOrEmpty())
                return path2;

            if (path2.StartsWith("http://") || path2.StartsWith("https://"))
                return path2;

            var ch = path1[path1.Length - 1];

            if (ch != '/')
                return (path1.TrimEnd('/') + '/' + path2.TrimStart('/'));

            return (path1 + path2);
        }
    }
}
