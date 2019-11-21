using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.Rattata
{
    public class RattataBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Rattata");
            Description.SetDefault("A Rattata is following you around!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.noFallDmg = true;

            player.buffTime[buffIndex] = 40000;
            TerramonPlayer modPlayer = (TerramonPlayer)player.GetModPlayer(mod, "TerramonPlayer");
            modPlayer.rattataPet = true;
            bool petProjectileNotSpawned = true;
            if (player.ownedProjectileCounts[mod.ProjectileType("Rattata")] > 0)
            {
                petProjectileNotSpawned = false;
            }


            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, mod.ProjectileType("Rattata"), 0, 0f, player.whoAmI, 0f, 0f);
            }

        }
    }
}