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

        public void GrantAchievementLocal<T>(Player player) where T : TerramonAchievement =>
            ModInstance.Call("UnlockLocal", TerramonMod.Instance.Name, TerramonAchievementLoader.Instance.GetName<T>(), player);
    }
}