using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon.Pikachu
{
    public class PikachuBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pikachu");
            Tooltip.SetDefault("Summons a Pikachu to follow you on your adventure!"
            + "\n[c/B76FB7:Tier One]");
        }

        public override void SetDefaults()
        {

            item.CloneDefaults(ItemID.Carrot);

            item.width = 25;
            item.height = 25;

            item.useTime = 20;
            item.useStyle = 4;
            item.useAnimation = 1;

            item.UseSound = SoundID.Item1; item.shoot = mod.ProjectileType("Pikachu");
            item.buffType = mod.BuffType("PikachuBuff");

            item.noMelee = true;
            item.accessory = true;

            item.rare = 8;
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