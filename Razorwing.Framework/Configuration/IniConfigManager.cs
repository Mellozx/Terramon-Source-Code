using System;
using System.Collections.Generic;
using System.IO;
using Razorwing.Framework.Bindables;
using Razorwing.Framework.Platform;
using Terraria.Localization;


namespace Razorwing.Framework.Configuration
{
    public class IniConfigManager<TLookup> : ConfigManager<TLookup>
        where TLookup : struct, Enum
    {
        /// <summary>
        /// The backing file used to store the config. Null means no persistent storage.
        /// </summary>
        protected virtual string Filename => $@"{culture.Name}.lang";

        protected GameCulture culture;

        private readonly Storage storage;

        public IniConfigManager(Storage storage, GameCulture culture = null)
            : base(null)
        {
            this.storage = storage;
            this.culture = culture ?? GameCulture.English;

            InitialiseDefaults();
            Load();
        }

        protected override void PerformLoad()
        {
            if (string.IsNullOrEmpty(Filename)) return;

            using (var stream = storage.GetStream(Filename))
            {
                if (stream == null)
                    return;

                using (var reader = new StreamReader(stream))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        int equalsIndex = line.IndexOf('=');

                        if (line.Length == 0 || line[0] == '#' || equalsIndex < 0) continue;

                        string key = line.Substring(0, equalsIndex).Trim();
                        string val = line.Remove(0, equalsIndex + 1).Trim();

                        if (!Enum.TryParse(key, out TLookup lookup))
                            continue;

                        if (ConfigStore.TryGetValue(lookup, out IBindable b))
                        {
                            try
                            {
                                b.Parse(val);
                            }
                            catch (Exception e)
                            {
                            }
                        }
                        else if (AddMissingEntries)
                            Set(lookup, val);
                    }
                }
            }
        }

        protected override bool PerformSave()
        {
            //if (string.IsNullOrEmpty(Filename)) return false;

            //try
            //{
            //    using (var stream = storage.GetStream(Filename, FileAccess.Write, FileMode.Create))
            //    using (var w = new StreamWriter(stream))
            //    {
            //        foreach (var p in ConfigStore)
            //            w.WriteLine(@"{0} = {1}", p.Key, p.Value.ToString().Replace("\n", "\\n").Replace("\r", ""));
            //    }
            //}
            //catch
            //{
            //    return false;
            //}

            return true;
        }
    }
}
