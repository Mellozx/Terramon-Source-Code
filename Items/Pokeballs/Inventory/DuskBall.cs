using Terramon.Items.Pokeballs.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class DuskBall : BaseThrowablePokeballItem<DuskBallProjectile>
    {
        public const string TOOLTIP =
            "A somewhat different Poké Ball." +
            "\nIt makes it easier to catch wild Pokémon at night.";


        public DuskBall() : base(Constants.Pokeballs.UnlocalizedNames.DUSK_BALL, "Dusk Ball", TOOLTIP, Item.sellPrice(gold: 2, silver: 20), ItemRarityID.Green, Constants.Pokeballs.CatchRates.DUSK_BALL)
        {
        }
    }
}
