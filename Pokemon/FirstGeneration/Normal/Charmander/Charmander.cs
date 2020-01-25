using System;
using Microsoft.Xna.Framework;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Charmander
{
    public class Charmander : ParentPokemon
    {
        public override int EvolveCost => 11;

        public override Type EvolveTo => typeof(Charmeleon.Charmeleon);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 34;
            projectile.height = 26;
            drawOriginOffsetY = -14;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.charmanderPet = false;
            }
            if (modPlayer.charmanderPet)
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