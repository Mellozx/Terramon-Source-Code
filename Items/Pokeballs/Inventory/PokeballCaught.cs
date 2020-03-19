using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class PokeballCaught : BaseCaughtClass
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Poké Ball");
            Tooltip.SetDefault("Contains %PokemonName"
                               + "\nLeft click to send out this Pokémon (or return it to this ball)."
                               + "\nRight click to add to your party.");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine nameLine = tooltips.FirstOrDefault(t => t.Name == "ItemName" && t.mod == "Terraria");
            if (nameLine != null) nameLine.text = "Poké Ball (" + PokemonName + ")";

            foreach (TooltipLine line2 in tooltips)
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                    line2.overrideColor = new Color(255, 87, 87);

            string tooltipText = tooltips.Find(x => x.Name == "Tooltip0").text;
            tooltipText = tooltipText.Replace("%PokemonName", PokemonName);

            tooltips.Find(x => x.Name == "Tooltip0").text = tooltipText;
            base.ModifyTooltips(tooltips);
        }
    }
}