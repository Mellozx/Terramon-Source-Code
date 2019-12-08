using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Venusaur
{
    public class Venusaur : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 48;
            projectile.height = 40;
			projectile.scale = 1.2f;
            // drawOriginOffsetY = -1;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.venusaurPet = false;
            }
            if (modPlayer.venusaurPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}