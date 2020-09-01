using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Razorwing.Framework.IO.Stores;

namespace Terramon.Razorwing.Framework.IO.Stores
{
    internal class EmbeddedStore : IResourceStore<byte[]>
    {
        private Dictionary<string,byte[]> files= new Dictionary<string, byte[]>();
        private const string resourceFolder = "Resources";


        public void Dispose()
        {
            files = null;
        }

        public byte[] Get(string name)
        {
            var path = GetFilename(name);
            if (files.ContainsKey(path))
                return files[path];

            var type = typeof(Embedded);
            var prop = type.GetProperty(path, BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public);
            var str = (string)prop?.GetValue(null)??null;
            if (string.IsNullOrEmpty(str))
            {
                files.Add(path,null);
                return null;
            }
            files.Add(path,Base64Decode(str));
            return files[path];
        }

        public Task<byte[]> GetAsync(string name)
        {
            return Task.FromResult(Get(name));
        }

        public IEnumerable<string> GetAvailableResources()
        {
            return null;
        }

        public Stream GetStream(string name)
        {
            var data = Get(name);
            if (data == null)
                return null;
            return new MemoryStream(data);
        }

        protected string GetFilename(string name)
        {
            if (name.StartsWith(resourceFolder))
                name = name.Substring(resourceFolder.Length + 1);
            return name.Replace('-', '_').Replace('.', '_')
                    .Replace(Path.DirectorySeparatorChar, '_');
        }

        public static byte[] Base64Decode(string base64EncodedData)
        {
            if (base64EncodedData == null) return null;
            return System.Convert.FromBase64String(base64EncodedData);
            //return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
