using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Charmeleon
{
    public class CharmeleonBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pokéball (Charmeleon)");
            Tooltip.SetDefault("Used to send out [c/FF6666:Charmeleon!]"
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
            item.shoot = mod.ProjectileType("Charmeleon");
            item.buffType = mod.BuffType("CharmeleonBuff");
            item.accessory = false;

            item.noMelee = true;

            item.rare = 2;
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }

        public override void OnCraft(Recipe recipe)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve").WithVolume(.7f));
            Main.NewText("[c/FFFF66:Charmander evolved into Charmeleon!]");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CharmanderBall"));
            recipe.AddIngredient(mod.ItemType("RareCandy"), 11);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}