using Terramon.Achievements;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.Players;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class UltraBallItem : BaseThrowablePokeballItem<UltraBallProjectile>
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            if (GetInstance<TerramonConfig>().ItemNameColors == 1 || GetInstance<TerramonConfig>().ItemNameColors == 2)
            {
                DisplayName.SetDefault("[c/f5da53:Ultra Ball]");
            }
            else DisplayName.SetDefault("Ultra Ball");

        }

        public const string TOOLTIP = "It's an ultra-performance Ball." +
                                      "\nProvides a higher Pokémon catch rate than a Great Ball.";


        public UltraBallItem() : base(Constants.Pokeballs.UnlocalizedNames.ULTRA_BALL, "[c/f5da53:Ultra Ball]", TOOLTIP, Item.sellPrice(gold: 7, silver: 75), ItemRarityID.White, Constants.Pokeballs.CatchRates.ULTRA_BALL)
        {
        }


        protected override void PostPokeballThrown(TerramonPlayer terramonPlayer, int thrownPokeballsCount)
        {
            /*compatibility.GrantAchievementLocal<UltraTossAchievement>(terramonPlayer.player);

            if (thrownPokeballsCount >= 25)
                compatibility.GrantAchievementLocal<ALotOfUltraTossesAchievement>(terramonPlayer.player);*/
        }
    }
}
