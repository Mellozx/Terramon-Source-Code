using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class GreatBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Great Ball");
            Tooltip.SetDefault("A good, high-performance Ball."
            + "\nProvides a higher Pokémon catch rate than a Poké Ball.");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;

            item.damage = 20;
            item.knockBack = 0;
            item.shootSpeed = 10f;
            item.shoot = ModContent.ProjectileType<Pokeballs.Thrown.GreatBallProjectile>();

            item.scale = 0.6f;
            item.maxStack = 99;

            item.useTime = 16;
            item.useStyle = 1;
            item.useAnimation = 16;
            item.UseSound = SoundID.Item1;

            item.rare = 0;
            item.value = 32500;

            item.ranged = true;
            item.autoReuse = false;
            item.consumable = true;
            item.noUseGraphic = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Mod achLib = ModLoader.GetMod("AchievementLib");
            TerramonPlayer TerramonPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            TerramonPlayer.greatBallsThrown++;
            if (TerramonPlayer.greatBallsThrown == 1)
            {
                achLib.Call("UnlockLocal", "Terramon", "Great Toss", player);
            }
            if (TerramonPlayer.greatBallsThrown == 25)
            {
                achLib.Call("UnlockLocal", "Terramon", "A Lot of Great Tosses", player);
            }
            return true;
        }
    }
}
