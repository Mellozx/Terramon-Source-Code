using FargowiltasSouls.ModCompatibilities;
using Terraria.Achievements;
using Terraria.ModLoader;

namespace Terramon.ModCompatibilities
{
    public class AchievementLibCompatibility : ModCompatibility
    {
        public AchievementLibCompatibility(Mod callerMod) : base(callerMod, "AchievementLib")
        {
        }


        public override ModCompatibility TryLoad()
        {
            return this;
        }
    }
}