using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.Bulbasaur
{
    public class Bulbasaur : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 35;
            projectile.height = 40;
            //drawOriginOffsetY = -1;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.bulbasaurPet = false;
            }
            if (modPlayer.bulbasaurPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}