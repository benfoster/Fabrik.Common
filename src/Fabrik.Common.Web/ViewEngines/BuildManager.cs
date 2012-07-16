using System;
using System.Collections;
using System.IO;
using System.Web.Compilation;

namespace Fabrik.Web.Common
{
    public interface IBuildManager {
        bool FileExists(string virtualPath);
        Type GetCompiledType(string virtualPath);
        ICollection GetReferencedAssemblies();
        Stream ReadCachedFile(string fileName);
        Stream CreateCachedFile(string fileName);
    }

    public class BuildManagerWrapper : IBuildManager {

        public bool FileExists(string virtualPath) {
            return BuildManager.GetObjectFactory(virtualPath, false) != null;
        }

        public Type GetCompiledType(string virtualPath) {
            return BuildManager.GetCompiledType(virtualPath);
        }

        public ICollection GetReferencedAssemblies() {
            return BuildManager.GetReferencedAssemblies();
        }

        public Stream ReadCachedFile(string fileName) {
            return BuildManager.ReadCachedFile(fileName);
        }

        public Stream CreateCachedFile(string fileName) {
            return BuildManager.CreateCachedFile(fileName);
        }
    }
}