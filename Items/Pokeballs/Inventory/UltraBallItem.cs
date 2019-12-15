using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terramon.Achievements;
using Terramon.Items.Pokeballs.Thrown;
using Terramon.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class UltraBallItem : BaseThrowablePokeballItem<UltraBallProjectile>
    {
        public UltraBallItem() : base(Constants.Pokeballs.UnlocalizedNames.ULTRA_BALL, 
            new Dictionary<GameCulture, string>()
            {
                { GameCulture.English, "Ultra Ball" },
                { GameCulture.French, "Hyper Ball" }
            }, 
            new Dictionary<GameCulture, string>()
            {
                { GameCulture.English, "It's an ultra-performance Ball.\nProvides a higher Pokémon catch rate than a Great Ball." },
                { GameCulture.French, "C'est une balle ultra-performante.\nFournit un taux de capture de Pokémon supérieur à celui d'une Super Ball." }
            }, 
            Item.sellPrice(gold: 7, silver: 75), ItemRarityID.Orange, Constants.Pokeballs.CatchRates.ULTRA_BALL, new Color(245, 218, 83))
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("UltraBallCap"));
            recipe.AddIngredient(mod.ItemType("Button"));
            recipe.AddIngredient(mod.ItemType("PokeballBase"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


        protected override void PostPokeballThrown(TerramonPlayer terramonPlayer, int thrownPokeballsCount)
        {
            /*compatibility.GrantAchievementLocal<UltraTossAchievement>(terramonPlayer.player);

            if (thrownPokeballsCount >= 25)
                compatibility.GrantAchievementLocal<ALotOfUltraTossesAchievement>(terramonPlayer.player);*/
        }
    }
}
