using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class Pokeball : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poké Ball");
            Tooltip.SetDefault("A device for catching wild Pokémon."
            + "\nIt is thrown like a ball at the target."
            + "\nIt is designed as a capsule system.");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;

            item.damage = 10;
            item.knockBack = 0;
            item.shootSpeed = 9f;
            item.shoot = ModContent.ProjectileType<Pokeballs.Thrown.PokeballProjectile>();

            item.scale = 0.6f;
            item.maxStack = 99;

            item.useTime = 16;
            item.useStyle = 1;
            item.useAnimation = 16;
            item.UseSound = SoundID.Item1;

            item.rare = 0;
            item.value = 6500;

            item.ranged = true;
            item.autoReuse = false;
            item.consumable = true;
            item.noUseGraphic = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Mod achLib = ModLoader.GetMod("AchievementLib");
            TerramonPlayer TerramonPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            TerramonPlayer.pkBallsThrown++;
            if (TerramonPlayer.pkBallsThrown == 1)
            {
                achLib.Call("UnlockLocal", "Terramon", "First Toss", player);
            }
            if (TerramonPlayer.pkBallsThrown == 25)
            {
                achLib.Call("UnlockLocal", "Terramon", "A Lot of Tosses", player);
            }
            return true;
        }
    }
}
