using Terramon.Achievements;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.Players;
using Terraria;
using Terraria.ID;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class PremierBallItem : BaseThrowablePokeballItem<PokeballProjectile>
    {
        public const string TOOLTIP = "A rare Ball made in commemoration of some event." +
                                      "\nObtained after buying 10 regular Poké Balls at once.";


        public PremierBallItem() : base(Constants.Pokeballs.UnlocalizedNames.POKE_BALL, "Premier Ball", TOOLTIP, Item.sellPrice(copper: 0),
            ItemRarityID.White, Constants.Pokeballs.CatchRates.POKE_BALL)
        {
        }
    }
}
