using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public class UltraBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultra Ball");
            Tooltip.SetDefault("It's an ultra-performance Ball."
            + "\nProvides a higher Pokémon catch rate than a Great Ball.");
        }
        public override void SetDefaults()
        {

            item.width = 24;
            item.height = 24;

            item.damage = 30;
            item.knockBack = 0;
            item.shootSpeed = 11f;
            item.shoot = ModContent.ProjectileType<Pokeballs.Thrown.UltraBallProjectile>();

            item.scale = 0.6f;
            item.maxStack = 99;

            item.useTime = 16;
            item.useStyle = 1;
            item.useAnimation = 16;
            item.UseSound = SoundID.Item1;

            item.rare = 0;
            item.value = 77500;

            item.ranged = true;
            item.autoReuse = false;
            item.consumable = true;
            item.noUseGraphic = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Mod achLib = ModLoader.GetMod("AchievementLib");
            TerramonPlayer TerramonPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            TerramonPlayer.ultraBallsThrown++;
            if (TerramonPlayer.ultraBallsThrown == 1)
            {
                achLib.Call("UnlockLocal", "Terramon", "Ultra Toss", player);
            }
            if (TerramonPlayer.ultraBallsThrown == 25)
            {
                achLib.Call("UnlockLocal", "Terramon", "A Lot of Ultra Tosses", player);
            }
            return false;
        }
    }
}
