using AchievementLib.Elements;
using Microsoft.Xna.Framework;
using Terramon.Achievements;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class UltraBall : BaseThrowablePokeballItem<UltraBallProjectile>
    {
        public const string TOOLTIP = "It's an ultra-performance Ball." +
                                      "\nProvides a higher Pokémon catch rate than a Great Ball.";


        public UltraBall() : base(Constants.Pokeballs.UnlocalizedNames.ULTRA_BALL, "Ultra Ball", TOOLTIP, Item.sellPrice(gold: 7, silver: 75), ItemRarityID.Orange, Constants.Pokeballs.CatchRates.ULTRA_BALL)
        {
        }


        protected override void OnCheckShootAchievements(TerramonPlayer terramonPlayer, Mod achievementsLib, int thrownPokeballsCount)
        {
            ModAchievement.UnlockLocal<UltraTossAchievement>(terramonPlayer.player);

            if (thrownPokeballsCount >= 25)
                ModAchievement.UnlockLocal<ALotOfUltraTossesAchievement>(terramonPlayer.player);
        }
    }
}
