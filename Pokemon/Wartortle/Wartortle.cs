using Terraria;

namespace Terramon.Pokemon.Wartortle
{
    public class Wartortle : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 35;
            projectile.height = 40;
            drawOriginOffsetY = -1;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.wartortlePet = false;
            }
            if (modPlayer.wartortlePet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}