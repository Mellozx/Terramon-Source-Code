using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class MasterBallCaught : BaseCaughtClass
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Master Ball");
            Tooltip.SetDefault("Contains %PokemonName"
                               + "\nLeft click to send out this Pokémon."
                               + "\nRight click to add to your party.");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine nameLine = tooltips.FirstOrDefault(t => t.Name == "ItemName" && t.mod == "Terraria");
            if (isShiny)
            {
                if (nameLine != null) nameLine.text = "Master Ball (" + PokemonName + " ✦)";
            }
            else
            {
                if (nameLine != null) nameLine.text = "Master Ball (" + PokemonName + ")";
            }

            foreach (TooltipLine line2 in tooltips)
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                    line2.overrideColor = new Color(245, 83, 218);

            string tooltipText = tooltips.Find(x => x.Name == "Tooltip0").text;
            if (isShiny)
            {
                tooltipText = tooltipText.Replace("%PokemonName", PokemonName + " ✦");
            }
            else
            {
                tooltipText = tooltipText.Replace("%PokemonName", PokemonName);
            }

            tooltips.Find(x => x.Name == "Tooltip0").text = tooltipText;
            base.ModifyTooltips(tooltips);
        }
    }
}