using Terramon.Items.Pokeballs.Thrown;
using Terraria;
using Terraria.ID;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class DuskBallItem : BaseThrowablePokeballItem<DuskBallProjectile>
    {
        public const string TOOLTIP =
            "A somewhat different Poké Ball." +
            "\nIt makes it easier to catch wild Pokémon at night.";


        public DuskBallItem() : base(Constants.Pokeballs.UnlocalizedNames.DUSK_BALL, "Dusk Ball", TOOLTIP, Item.sellPrice(gold: 2, silver: 20), ItemRarityID.Green, Constants.Pokeballs.CatchRates.DUSK_BALL)
        {
        }
    }
}
