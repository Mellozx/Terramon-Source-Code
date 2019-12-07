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
    public class UltraBallItem : BaseThrowablePokeballItem<UltraBallProjectile>
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            if (GetInstance<TerramonConfig>().Language == 1)
            {
                DisplayName.SetDefault("Ultra Ball");
            }
            else if (GetInstance<TerramonConfig>().Language == 2)
            {
                Tooltip.SetDefault("C'est un ballon ultra-performant." +
                                    "\nFournit un taux de capture de Pokémon supérieur à celui d'une Super Ball.");
                
                DisplayName.SetDefault("Hyper Ball");
            }
        }

        public const string TOOLTIP = "It's an ultra-performance Ball." +
                                      "\nProvides a higher Pokémon catch rate than a Great Ball.";


        public UltraBallItem() : base(Constants.Pokeballs.UnlocalizedNames.ULTRA_BALL, "Ultra Ball", TOOLTIP, Item.sellPrice(gold: 7, silver: 75), ItemRarityID.White, Constants.Pokeballs.CatchRates.ULTRA_BALL)
        {
        }


        protected override void PostPokeballThrown(TerramonPlayer terramonPlayer, int thrownPokeballsCount)
        {
            /*compatibility.GrantAchievementLocal<UltraTossAchievement>(terramonPlayer.player);

            if (thrownPokeballsCount >= 25)
                compatibility.GrantAchievementLocal<ALotOfUltraTossesAchievement>(terramonPlayer.player);*/
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
                        line2.overrideColor = new Color(245, 218, 83);
                    }
                }
        }
    }
}
