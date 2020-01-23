using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Dragonair
{
    public class Dragonair : ParentPokemon
    {
        public override int EvolveCost => 25;

        public override Type EvolveTo => typeof(Dragonite.Dragonite);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -26;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.dragonairPet = false;
            }
            if (modPlayer.dragonairPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}