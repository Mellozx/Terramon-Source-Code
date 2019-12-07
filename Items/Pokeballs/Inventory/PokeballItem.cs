using Microsoft.Xna.Framework;
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
    public class PokeballItem : BaseThrowablePokeballItem<PokeballProjectile>
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            if (GetInstance<TerramonConfig>().Language == 2) // French
            {
                Tooltip.SetDefault("Un appareil pour attraper les Pokémon." +
                                    "\nIl est lancé comme une balle sur la cible." +
                                    "\nIl est utiliser comme un système de capsule.");
            }
        }

        public const string TOOLTIP = "A device for catching wild Pokémon." +
                                      "\nIt is thrown like a ball at the target." +
                                      "\nIt is designed as a capsule system.";

        public PokeballItem() : base(Constants.Pokeballs.UnlocalizedNames.POKE_BALL, "Poké Ball", TOOLTIP, Item.sellPrice(silver: 65), 
            ItemRarityID.White, Constants.Pokeballs.CatchRates.POKE_BALL)
        {
        }


        protected override void PostPokeballThrown(TerramonPlayer terramonPlayer, int thrownPokeballsCount)
        {
            /*compatibility.GrantAchievementLocal<FirstTossAchievement>(terramonPlayer.player);
            
            if (thrownPokeballsCount >= 25)
                compatibility.GrantAchievementLocal<ALotOfTossesAchievement>(terramonPlayer.player);*/
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
                        line2.overrideColor = new Color(255, 87, 87);
                    }
                }
            
        }
    }
}
