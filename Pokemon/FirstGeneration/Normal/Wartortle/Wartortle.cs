using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Wartortle
{
    public class Wartortle : ParentPokemon
    {
        public override int EvolveCost => 20;

        public override Type EvolveTo => typeof(Blastoise.Blastoise);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 35;
            projectile.height = 40;
            drawOriginOffsetY = -8;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.wartortlePet = false;
            }
            if (modPlayer.wartortlePet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}