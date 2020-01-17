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
    public class PremierBallItem : BaseThrowablePokeballItem<PremierBallProjectile>
    {
        public PremierBallItem() : base(Constants.Pokeballs.UnlocalizedNames.PREMIER_BALL,
            new Dictionary<GameCulture, string>()
            {
                { GameCulture.English, "Premier Ball" },
                { GameCulture.French, "Honor Ball" }
            },
            new Dictionary<GameCulture, string>()
            {
                { GameCulture.English, "A rare Ball made in commemoration of some event.\nObtained after buying 10 regular Poké Balls at once." },
                { GameCulture.French, "Un Ball rare fait en commémoration d'un événement.\nObtenu après l'achat de 10 Poké Balls réguliers à la fois." }
            },
            Item.sellPrice(copper: 0), ItemRarityID.White, Constants.Pokeballs.CatchRates.PREMIER_BALL, null)
        {
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("PremierBallCap"));
            recipe.AddIngredient(mod.ItemType("Button"));
            recipe.AddIngredient(mod.ItemType("PokeballBase"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}