using Terramon.Achievements;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.Players;
using Terraria;
using Terraria.ID;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class GreatBallItem : BaseThrowablePokeballItem<GreatBallProjectile>
    {
        public const string TOOLTIP =
            "A good, high-performance Ball." +
            "\nProvides a higher Pokémon catch rate than a Poké Ball.";


        public GreatBallItem() : base(Constants.Pokeballs.UnlocalizedNames.GREAT_BALL, "Great Ball", TOOLTIP, Item.sellPrice(gold: 3, silver: 25), 
            ItemRarityID.White, Constants.Pokeballs.CatchRates.POKE_BALL)
        {
        }


        protected override void PostPokeballThrown(TerramonPlayer terramonPlayer, int thrownPokeballsCount)
        {
            /*compatibility.GrantAchievementLocal<GreatTossAchievement>(terramonPlayer.player);

            if (thrownPokeballsCount >= 25)
                compatibility.GrantAchievementLocal<ALotOfGreatTossesAchievement>(terramonPlayer.player);*/
        }
    }
}
