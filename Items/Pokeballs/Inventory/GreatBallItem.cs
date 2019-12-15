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
    public class GreatBallItem : BaseThrowablePokeballItem<GreatBallProjectile>
    {
        public GreatBallItem() : base(Constants.Pokeballs.UnlocalizedNames.GREAT_BALL,
            new Dictionary<GameCulture, string>()
            {
                { GameCulture.English, "Great Ball" },
                { GameCulture.French, "Super Ball" }
            },
            new Dictionary<GameCulture, string>()
            {
                { GameCulture.English, "A good, high-performance Ball.\nProvides a higher Pokémon catch rate than a Poké Ball." },
                { GameCulture.French, "Un bon Ball très performant.\nFournit un taux de capture de Pokémon supérieur à celui d'une Poké Ball." }
            },
            Item.sellPrice(gold: 3, silver: 25), ItemRarityID.Blue, Constants.Pokeballs.CatchRates.POKE_BALL, new Color(89, 183, 255))
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GreatBallCap"));
            recipe.AddIngredient(mod.ItemType("Button"));
            recipe.AddIngredient(mod.ItemType("PokeballBase"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


        protected override void PostPokeballThrown(TerramonPlayer terramonPlayer, int thrownPokeballsCount)
        {
            /*compatibility.GrantAchievementLocal<GreatTossAchievement>(terramonPlayer.player);

            if (thrownPokeballsCount >= 25)
                compatibility.GrantAchievementLocal<ALotOfGreatTossesAchievement>(terramonPlayer.player);*/
        }
    }
}
