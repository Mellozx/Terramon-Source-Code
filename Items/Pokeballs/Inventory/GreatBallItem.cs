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
    public class GreatBallItem : BaseThrowablePokeballItem<GreatBallProjectile>
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            if (GetInstance<TerramonConfig>().Language == 1)
            {
                
            } else if (GetInstance<TerramonConfig>().Language == 2)
            {
                Tooltip.SetDefault("Un bon Ball très performant." +
                                    "\nFournit un taux de capture de Pokémon supérieur à celui d'une Poké Ball.");
        
                DisplayName.SetDefault("Super Ball");
            }
        }

        public const string TOOLTIP =
            "A good, high-performance Ball." +
            "\nProvides a higher Pokémon catch rate than a Poké Ball.";


        public GreatBallItem() : base(Constants.Pokeballs.UnlocalizedNames.GREAT_BALL, "Great Ball", TOOLTIP, Item.sellPrice(gold: 3, silver: 25), 
            ItemRarityID.White, Constants.Pokeballs.CatchRates.POKE_BALL)
        {
        }


        protected override void PostPokeballThrown(TerramonPlayer terramonPlayer, int thrownPokeballsCount)
        {
            /*compatibility.GrantAchievementLocal<GreatTossAchievement>(terramonPlayer.player);

            if (thrownPokeballsCount >= 25)
                compatibility.GrantAchievementLocal<ALotOfGreatTossesAchievement>(terramonPlayer.player);*/
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
                        line2.overrideColor = new Color(89, 183, 255);
                    }
                }
            
        }
    }
}
