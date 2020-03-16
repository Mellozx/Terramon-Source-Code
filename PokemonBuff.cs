using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terramon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Terramon
{
    public abstract class PokemonBuff : ModBuff
    {
        public abstract string ProjectileName { get; }

        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.noFallDmg = true;

            player.buffTime[buffIndex] = 40000;
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            modPlayer.bulbasaurPet = true;
            var petProjectileNotSpawned = !(player.ownedProjectileCounts[mod.ProjectileType(ProjectileName)] > 0);


            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, mod.ProjectileType("Bulbasaur"), 0, 0f, player.whoAmI, 0f, 0f);
            }

        }
    }
}
