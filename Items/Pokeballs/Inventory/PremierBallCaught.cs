using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class PremierBallCaught : BaseCaughtClass
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Premier Ball");
            Tooltip.SetDefault("Contains %PokemonName"
                               + "\nLeft click to send out this Pokémon."
                               + "\nRight click to add to your party.");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine nameLine = tooltips.FirstOrDefault(t => t.Name == "ItemName" && t.mod == "Terraria");
            if (nameLine != null) nameLine.text = "Premier Ball (" + PokemonName + ")";

            string tooltipText = tooltips.Find(x => x.Name == "Tooltip0").text;
            tooltipText = tooltipText.Replace("%PokemonName", PokemonName);

            tooltips.Find(x => x.Name == "Tooltip0").text = tooltipText;
            base.ModifyTooltips(tooltips);
        }
    }
}