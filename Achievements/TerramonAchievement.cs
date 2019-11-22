using System;
using AchievementLib.Elements;
using Terraria.Achievements;

namespace Terramon.Achievements
{
    public abstract class TerramonAchievement : ModAchievement
    {
        private readonly string _name, _description;
        private readonly AchievementCategory _category;


        protected TerramonAchievement(string name, string description, AchievementCategory category)
        {
            _name = name;
            _description = description;
            _category = category;

            TextureName = this.GetType().Name;
        }


        public override void SetDefaults()
        {
            Name = _name;
            Description = _description;

            Category = _category;

            LockedTexture = mod.GetTexture($"{TextureName}Locked");
            UnlockedTexture = mod.GetTexture($"{TextureName}Unlocked");
        }


        public string TextureName { get; }
    }
}