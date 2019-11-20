using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.Squirtle
{
    public class SquirtleBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Squirtle");
            Description.SetDefault("A Squirtle is following you around!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.noFallDmg = true;

            player.buffTime[buffIndex] = 40000;
            TerramonPlayer modPlayer = (TerramonPlayer)player.GetModPlayer(mod, "TerramonPlayer");
            modPlayer.squirtlePet = true;
            bool petProjectileNotSpawned = true;
            if (player.ownedProjectileCounts[mod.ProjectileType("Squirtle")] > 0)
            {
                petProjectileNotSpawned = false;
            }


            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, mod.ProjectileType("Squirtle"), 0, 0f, player.whoAmI, 0f, 0f);
            }

        }
    }
}