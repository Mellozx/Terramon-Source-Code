using System.Collections.Generic;
using Terramon.Achievements;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class PremierBallItem : BaseThrowablePokeballItem<PremierBallProjectile>
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            if (GetInstance<TerramonConfig>().Language == 1)
            {
                DisplayName.SetDefault("Premier Ball");
            }
            else if (GetInstance<TerramonConfig>().Language == 2)
            {
                Tooltip.SetDefault("Un Ball rare fait en commémoration d'un événement." +
                                    "\nObtenu après l'achat de 10 Poké Balls réguliers à la fois.");
                DisplayName.SetDefault("Honor Ball");
            }
        }

        public const string TOOLTIP = "A rare Ball made in commemoration of some event." +
                                      "\nObtained after buying 10 regular Poké Balls at once.";


        public PremierBallItem() : base(Constants.Pokeballs.UnlocalizedNames.PREMIER_BALL, "Premier Ball", TOOLTIP, Item.sellPrice(copper: 0),
            ItemRarityID.White, Constants.Pokeballs.CatchRates.PREMIER_BALL)
        {
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(l => l.Name == "Damage");
            tooltips.RemoveAll(l => l.Name == "CritChance");
            tooltips.RemoveAll(l => l.Name == "Speed");
            tooltips.RemoveAll(l => l.Name == "Knockback");
        }
    }
}
