using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Caterpie
{
    public class Caterpie : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 38;
            projectile.height = 38;
            drawOriginOffsetY = -1;
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