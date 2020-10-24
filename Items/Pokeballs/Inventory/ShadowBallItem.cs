using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terramon.Items.Pokeballs.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class ShadowBallItem : BaseThrowablePokeballItem<ShadowBallProjectile>
    {
        public ShadowBallItem() : base(Constants.Pokeballs.UnlocalizedNames.PREMIER_BALL,
            new Dictionary<GameCulture, string>
            {
                {GameCulture.English, "Shadow Ball"},
                {GameCulture.French, "Ombre Ball"}
            },
            new Dictionary<GameCulture, string>
            {
                {
                    GameCulture.English,
                    "A spooky ball that seems to work best under certain time conditions."
                },
                {
                    GameCulture.French,
                    "Une balle effrayante qui semble fonctionner le mieux dans certaines conditions de temps."      //Google Translate stuff, it's not my fault if it's wrong
                }
            },
            Item.sellPrice(), ItemRarityID.White, Constants.Pokeballs.CatchRates.SHADOW_BALL)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Pumpkin, 3);
            recipe.AddIngredient(mod.ItemType("Button"));
            recipe.AddIngredient(mod.ItemType("PokeballBase"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine nameLine = tooltips.FirstOrDefault(t => t.Name == "ItemName" && t.mod == "Terraria");

            tooltips.RemoveAll(l => l.Name == "Damage");
            tooltips.RemoveAll(l => l.Name == "CritChance");
            tooltips.RemoveAll(l => l.Name == "Speed");
            tooltips.RemoveAll(l => l.Name == "Knockback");

            if (NameColorOverride != null)
                tooltips.Find(t => t.Name == "ItemName").overrideColor = NameColorOverride;

            foreach (TooltipLine line2 in tooltips)
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                    line2.overrideColor = new Color(186, 207, 222);
        }
    }
}