using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terramon.Items.Pokeballs.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class DuskBallItem : BaseThrowablePokeballItem<DuskBallProjectile>
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            if (GetInstance<TerramonConfig>().Language == 1)
            {
                DisplayName.SetDefault("Dusk Ball");
            }
            else if (GetInstance<TerramonConfig>().Language == 2)
            {
                Tooltip.SetDefault("Un Poké Ball quelque peu différent." +
                                    "\nIl est plus facile d'attraper les Pokémon pendant la nuit.");

                DisplayName.SetDefault("Sombre Ball");
            }
        }

        public const string TOOLTIP =
            "A somewhat different Poké Ball." +
            "\nIt makes it easier to catch wild Pokémon at night.";


        public DuskBallItem() : base(Constants.Pokeballs.UnlocalizedNames.DUSK_BALL, "Dusk Ball", TOOLTIP, Item.sellPrice(gold: 2, silver: 20), ItemRarityID.White, Constants.Pokeballs.CatchRates.DUSK_BALL)
        {
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(l => l.Name == "Damage");
            tooltips.RemoveAll(l => l.Name == "CritChance");
            tooltips.RemoveAll(l => l.Name == "Speed");
            tooltips.RemoveAll(l => l.Name == "Knockback");

            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(130, 224, 99);
                }
            }
        }
    }
}
