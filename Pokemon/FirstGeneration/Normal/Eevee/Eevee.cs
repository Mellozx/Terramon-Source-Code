using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Eevee
{
    public class Eevee : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 34; //-6
            projectile.height = 24; //-4
            drawOriginOffsetY = -20;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.eeveePet = false;
            }
            if (modPlayer.eeveePet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}