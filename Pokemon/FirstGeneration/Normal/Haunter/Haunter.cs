using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Haunter
{
    public class Haunter : ParentPokemonGastly
    {
        public override int EvolveCost => 10;

        public override Type EvolveTo => typeof(Gengar.Gengar);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 38;
            projectile.height = 40;
            drawOriginOffsetY = -36;
            projectile.alpha = 95;
        }
    }
}