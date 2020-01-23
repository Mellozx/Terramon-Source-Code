using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Caterpie
{
    public class Caterpie : ParentPokemon
    {
        public override int EvolveCost => 2;

        public override Type EvolveTo => typeof(Metapod.Metapod);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -14;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.caterpiePet = false;
            }
            if (modPlayer.caterpiePet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}