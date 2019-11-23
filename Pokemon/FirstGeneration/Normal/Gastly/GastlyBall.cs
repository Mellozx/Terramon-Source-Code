using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Gastly
{
    public class GastlyBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pokéball (Gastly)");
            Tooltip.SetDefault("Used to send out [c/DDA0DD:Gastly!]"
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
            item.shoot = mod.ProjectileType("Gastly");
            item.buffType = mod.BuffType("GastlyBuff");
            item.accessory = false;

            item.noMelee = true;

            item.rare = 6;
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