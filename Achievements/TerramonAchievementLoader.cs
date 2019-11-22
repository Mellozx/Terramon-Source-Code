using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using WebmilioCommons.Loaders;

namespace Terramon.Achievements
{
    public sealed class TerramonAchievementLoader : SingletonLoader<TerramonAchievementLoader, TerramonAchievement>
    {
        private Dictionary<Type, string> _nameByType;


        public override void PreLoad() => _nameByType = new Dictionary<Type, string>();

        public override void Unload()
        {
            _nameByType = new Dictionary<Type, string>();

            base.Unload();
        }


        protected override void PostAdd(Mod mod, TerramonAchievement item)
        {
            if (TerramonMod.Instance.AchievementLibLoaded)
                TerramonMod.Instance.AchievementLibCompatibility.RegisterAchievement(item);

            _nameByType.Add(item.GetType(), item.Name);
        }


        public string GetName<T>() where T : TerramonAchievement => _nameByType[typeof(T)];
    }
}