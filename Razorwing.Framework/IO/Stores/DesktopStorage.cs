using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Razorwing.Framework.IO.Stores;

namespace Razorwing.Framework.Platform
{
    public class DesktopStorage : Storage
    {
        protected IResourceStore<byte[]> Store;

        public DesktopStorage(string baseName, IResourceStore<byte[]> store)
            : base(baseName)
        {
            Store = store;
        }

        protected override string LocateBasePath() => @"Terramon/Resources";

        public override bool Exists(string path) => File.Exists(GetFullPath(path));

        public override bool ExistsDirectory(string path) => Directory.Exists(GetFullPath(path));

        public override void DeleteDirectory(string path)
        {
        }


        public override IEnumerable<string> GetDirectories(string path) => getRelativePaths(Directory.GetDirectories(GetFullPath(path)));

        public override IEnumerable<string> GetFiles(string path) => getRelativePaths(Directory.GetFiles(GetFullPath(path)));

        private IEnumerable<string> getRelativePaths(IEnumerable<string> paths)
        {
            //string basePath = Path.GetFullPath(GetFullPath(string.Empty));
            //return paths.Select(Path.GetFullPath).Select(path =>
            //{
            //    if (!path.StartsWith(basePath)) throw new ArgumentException($"\"{path}\" does not start with \"{basePath}\" and is probably malformed");
            //    return path.Substring(basePath.Length).TrimStart(Path.DirectorySeparatorChar);
            //});
            return paths;
        }

        public override string GetFullPath(string path, bool createIfNotExisting = false)
        {
            //path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            //var basePath = Path.GetFullPath(Path.Combine(BasePath, BaseName, SubDirectory));
            //var resolvedPath = Path.GetFullPath(Path.Combine(basePath, path));

            //if (!resolvedPath.StartsWith(basePath)) throw new ArgumentException($"\"{resolvedPath}\" traverses outside of \"{basePath}\" and is probably malformed");

            //if (createIfNotExisting) Directory.CreateDirectory(Path.GetDirectoryName(resolvedPath));
            //return resolvedPath;
            return path;
        }


        public override Stream GetStream(string path, FileAccess access = FileAccess.Read, FileMode mode = FileMode.Open)
        {
            path = GetFullPath(path, false);

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            switch (access)
            {
                case FileAccess.Read:
                    return Store.GetStream(path);
                default:
                    throw new InvalidOperationException("This Storage was set in read-only mode!");
            }
        }

        public override void Delete(string path)
        {
        }

        public override string GetDatabaseConnectionString(string name)
        {
            return string.Empty;
        }

        public override void DeleteDatabase(string name)
        {
        }

        public override void OpenInNativeExplorer()
        {
        }
    }
}
