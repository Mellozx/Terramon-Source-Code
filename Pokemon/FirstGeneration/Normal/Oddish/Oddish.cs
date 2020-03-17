using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Oddish
{
    public class Oddish : ParentPokemon
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // projectile.scale = 0.5f;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 38;
            projectile.height = 40;
            drawOriginOffsetY = 4;
        }
    }
}