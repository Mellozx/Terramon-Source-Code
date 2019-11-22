using Terramon.Achievements;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.ModCompatibilities;
using Terramon.Players;
using Terraria;
using Terraria.ID;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class PokeballItem : BaseThrowablePokeballItem<PokeballProjectile>
    {
        public const string TOOLTIP = "A device for catching wild Pokémon." +
                                      "\nIt is thrown like a ball at the target." +
                                      "\nIt is designed as a capsule system.";


        public PokeballItem() : base(Constants.Pokeballs.UnlocalizedNames.POKE_BALL, "Poké Ball", TOOLTIP, Item.sellPrice(silver: 65), 
            ItemRarityID.White, Constants.Pokeballs.CatchRates.POKE_BALL)
        {
        }


        protected override void OnCheckShootAchievements(TerramonPlayer terramonPlayer, AchievementLibCompatibility compatibility, int thrownPokeballsCount)
        {
            compatibility.GrantAchievementLocal<FirstTossAchievement>(terramonPlayer.player);
            
            if (thrownPokeballsCount >= 25)
                compatibility.GrantAchievementLocal<ALotOfTossesAchievement>(terramonPlayer.player);
        }
    }
}
