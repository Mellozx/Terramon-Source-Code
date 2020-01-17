using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Pidgeotto
{
    public class Pidgeotto : ParentPokemonFlying
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -8;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.pidgeottoPet = false;
            }
            if (modPlayer.pidgeottoPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}