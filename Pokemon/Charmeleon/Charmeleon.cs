using Terraria;

namespace Terramon.Pokemon.Charmeleon
{
    public class Charmeleon : ParentPokemon
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
                modPlayer.charmeleonPet = false;
            }
            if (modPlayer.charmeleonPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}