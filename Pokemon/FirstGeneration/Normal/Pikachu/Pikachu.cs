using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Pikachu
{
    public class Pikachu : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 35;
            projectile.height = 35;
            drawOriginOffsetY = -11;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.pikachuPet = false;
            }
            if (modPlayer.pikachuPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}