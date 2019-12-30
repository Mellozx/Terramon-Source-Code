using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Charmander
{
    public class Charmander : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 34;
            projectile.height = 26;
            drawOriginOffsetY = -14;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.charmanderPet = false;
            }
            if (modPlayer.charmanderPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}