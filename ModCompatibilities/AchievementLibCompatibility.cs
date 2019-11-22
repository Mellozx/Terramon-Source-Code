using Terramon.Achievements;
using Terraria;
using Terraria.ModLoader;
using WebmilioCommons.ModCompatibilities;

namespace Terramon.ModCompatibilities
{
    public class AchievementLibCompatibility : ModCompatibility
    {
        public AchievementLibCompatibility(Mod callerMod) : base(callerMod, "AchievementLib")
        {
        }


        public void RegisterAchievement(TerramonAchievement achievement) =>
            ModInstance.Call("AddAchievement", TerramonMod.Instance, achievement.Name, achievement.Description, achievement.LockedTexture, achievement.UnlockedTexture, achievement.Category);

        public void GrantAchievementLocal<T>(Player player) where T : TerramonAchievement =>
            ModInstance.Call("UnlockLocal", TerramonMod.Instance.Name, TerramonAchievementLoader.Instance.GetName<T>(), player);
    }
}