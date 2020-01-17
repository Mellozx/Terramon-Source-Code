using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Dragonite
{
    public class Dragonite : ParentPokemonFlying
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            projectile.scale = 1.15f;
            drawOriginOffsetY = -8;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.dragonitePet = false;
            }
            if (modPlayer.dragonitePet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}