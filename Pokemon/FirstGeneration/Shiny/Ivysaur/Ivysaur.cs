using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Shiny.Ivysaur
{
    public class Ivysaur : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 42;
            projectile.height = 40;
            // drawOriginOffsetY = -1;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.ivysaurPet = false;
            }
            if (modPlayer.ivysaurPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}