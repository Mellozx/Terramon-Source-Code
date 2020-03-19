using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Dratini
{
    public class Dratini : ParentPokemon
    {
        public override int EvolveCost => 25;

        public override Type EvolveTo => typeof(Dragonair.Dragonair);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -16;
        }
    }
}