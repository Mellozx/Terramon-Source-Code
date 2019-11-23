using Terramon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Ivysaur
{
    public class IvysaurBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ivysaur");
            Description.SetDefault("An Ivysaur is following you around!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.noFallDmg = true;

            player.buffTime[buffIndex] = 40000;
            TerramonPlayer modPlayer = (TerramonPlayer)player.GetModPlayer(mod, "TerramonPlayer");
            modPlayer.ivysaurPet = true;
            bool petProjectileNotSpawned = true;
            if (player.ownedProjectileCounts[mod.ProjectileType("Ivysaur")] > 0)
            {
                petProjectileNotSpawned = false;
            }


            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, mod.ProjectileType("Ivysaur"), 0, 0f, player.whoAmI, 0f, 0f);
            }

        }
    }
}