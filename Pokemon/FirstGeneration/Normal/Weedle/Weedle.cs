using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Weedle
{
    public class Weedle : ParentPokemon
    {
        public override int EvolveCost => 2;

        public override Type EvolveTo => typeof(Kakuna.Kakuna);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -10;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.weedlePet = false;
            }
            if (modPlayer.weedlePet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}