using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokeballs.Inventory
{
    public class DuskBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dusk Ball");
            Tooltip.SetDefault("A somewhat different Poké Ball."
            + "\nIt makes it easier to catch wild Pokémon at night.");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;

            item.damage = 10;
            item.knockBack = 0;
            item.shootSpeed = 9f;
            item.shoot = ModContent.ProjectileType<Pokeballs.Thrown.DuskBallProjectile>();

            item.scale = 0.6f;
            item.maxStack = 99;

            item.useTime = 16;
            item.useStyle = 1;
            item.useAnimation = 16;
            item.UseSound = SoundID.Item1;

            item.rare = 0;
            item.value = 22000;

            item.ranged = true;
            item.autoReuse = false;
            item.consumable = true;
            item.noUseGraphic = true;
        }
    }
}
