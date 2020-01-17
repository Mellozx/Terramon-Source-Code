using Microsoft.Xna.Framework;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Charizard
{
    public class Charizard : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 48;
            projectile.height = 40;
			projectile.scale = 1f;
            drawOriginOffsetY = -14;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.charizardPet = false;
            }
            if (modPlayer.charizardPet)
            {
                projectile.timeLeft = 2;
            }
            if (Main.rand.Next(9) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 100, new Color(255, 148, 41), 1f);
            }
        }
    }
}