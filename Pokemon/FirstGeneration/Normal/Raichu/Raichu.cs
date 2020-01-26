using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Raichu

{
    public class Raichu : ParentPokemon
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
                modPlayer.raichuPet = false;
            }
            if (modPlayer.raichuPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}