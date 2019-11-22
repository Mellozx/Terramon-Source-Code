using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Oddish
{
    public class Oddish : ParentPokemon
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // projectile.scale = 0.5f;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 38;
            projectile.height = 40;
            drawOriginOffsetY = 4;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.oddishPet = false;
            }
            if (modPlayer.oddishPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}