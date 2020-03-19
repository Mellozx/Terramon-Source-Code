using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Wartortle
{
    public class Wartortle : ParentPokemon
    {
        public override int EvolveCost => 20;

        public override Type EvolveTo => typeof(Blastoise.Blastoise);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 35;
            projectile.height = 40;
            drawOriginOffsetY = -8;
        }
    }
}