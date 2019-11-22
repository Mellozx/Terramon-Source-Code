using Microsoft.Xna.Framework.Graphics;
using Terraria.Achievements;

namespace Terramon.Achievements
{
    public abstract class TerramonAchievement
    {
        protected TerramonAchievement(string name, string description, AchievementCategory category)
        {
            Name = name;
            Description = description;

            TextureName = this.GetType().Name;

            LockedTexture = TerramonMod.Instance.GetTexture($"Achievements/{TextureName}Locked");
            UnlockedTexture = TerramonMod.Instance.GetTexture($"Achievements/{TextureName}Unlocked");

            Category = category;
        }


        public string Name { get; }
        public string Description { get; }

        public string TextureName { get; }

        public Texture2D LockedTexture { get; }
        public Texture2D UnlockedTexture { get; }

        public AchievementCategory Category { get; }
    }
}