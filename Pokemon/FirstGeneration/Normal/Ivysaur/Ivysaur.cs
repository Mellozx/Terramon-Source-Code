using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Ivysaur
{
    public class Ivysaur : ParentPokemon
    {
        public override int EvolveCost => 16;

        public override Type EvolveTo => typeof(Venusaur.Venusaur);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 36;
            projectile.height = 28;
			projectile.scale = 1.2f;
            // drawOriginOffsetY = -1;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.ivysaurPet = false;
            }
            if (modPlayer.ivysaurPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}