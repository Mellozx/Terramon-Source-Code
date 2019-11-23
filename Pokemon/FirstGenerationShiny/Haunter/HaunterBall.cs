using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Haunter
{
    public class HaunterBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pokéball (Haunter)");
            Tooltip.SetDefault("Used to send out [c/DDA0DD:Haunter!]"
            + "\n[c/B76FB7:Tier Two]");
        }

        public override void SetDefaults()
        {

            item.damage = 0;

            item.width = 24;
            item.height = 24;

            item.useTime = 20;
            item.useStyle = 1;
            item.useAnimation = 20;

            item.UseSound = SoundID.Item2;
            item.shoot = mod.ProjectileType("Haunter");
            item.buffType = mod.BuffType("HaunterBuff");
            item.accessory = false;

            item.noMelee = true;

            item.rare = 6;
        }

        public override void OnCraft(Recipe recipe)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve").WithVolume(.7f));
            Main.NewText("[c/FFFF66:Gastly evolved into Haunter!]");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GastlyBall"));
            recipe.AddIngredient(mod.ItemType("RareCandy"), 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}