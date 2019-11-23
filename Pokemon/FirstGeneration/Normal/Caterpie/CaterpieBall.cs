using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Caterpie
{
    public class CaterpieBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Caterpie");
            Tooltip.SetDefault("Summons a Caterpie to follow you on your adventure!");
        }

        public override void SetDefaults()
        {

            item.CloneDefaults(ItemID.Carrot);

            item.width = 25;
            item.height = 25;

            item.useTime = 20;
            item.useStyle = 1;
            item.useAnimation = 1;

            item.UseSound = SoundID.Item1; item.shoot = mod.ProjectileType("Caterpie");
            item.buffType = mod.BuffType("CaterpieBuff");

            item.value = Item.sellPrice(0, 0, 0, 0);

            item.noMelee = true;
            item.accessory = true;

            item.rare = 1;
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