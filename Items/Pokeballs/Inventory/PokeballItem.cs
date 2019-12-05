using Terramon.Achievements;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.Players;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class PokeballItem : BaseThrowablePokeballItem<PokeballProjectile>
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            if (GetInstance<TerramonConfig>().ItemNameColors == 1 || GetInstance<TerramonConfig>().ItemNameColors == 2) 
            {
                DisplayName.SetDefault("[c/ff5757:Poké Ball]");
            }
            else DisplayName.SetDefault("Poké Ball");

        }

        public const string TOOLTIP = "A device for catching wild Pokémon." +
                                      "\nIt is thrown like a ball at the target." +
                                      "\nIt is designed as a capsule system.";


        public PokeballItem() : base(Constants.Pokeballs.UnlocalizedNames.POKE_BALL, "[c/ff5757:Poké Ball]", TOOLTIP, Item.sellPrice(silver: 65), 
            ItemRarityID.White, Constants.Pokeballs.CatchRates.POKE_BALL)
        {
        }


        protected override void PostPokeballThrown(TerramonPlayer terramonPlayer, int thrownPokeballsCount)
        {
            /*compatibility.GrantAchievementLocal<FirstTossAchievement>(terramonPlayer.player);
            
            if (thrownPokeballsCount >= 25)
                compatibility.GrantAchievementLocal<ALotOfTossesAchievement>(terramonPlayer.player);*/
        }
    }
}
