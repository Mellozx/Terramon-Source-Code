using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Gastly
{
    public class Gastly : ParentPokemon
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
            drawOriginOffsetY = -36;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.gastlyPet = false;
            }
            if (modPlayer.gastlyPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}