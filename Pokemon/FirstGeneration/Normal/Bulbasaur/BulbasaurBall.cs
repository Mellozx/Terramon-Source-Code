using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Bulbasaur
{
    public class BulbasaurBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pokéball (Bulbasaur)");
            Tooltip.SetDefault("Used to send out [c/33FF33:Bulbasaur!]"
            + "\n[c/B76FB7:Tier One]");
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
            item.shoot = mod.ProjectileType("Bulbasaur");
            item.buffType = mod.BuffType("BulbasaurBuff");
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
    }
}