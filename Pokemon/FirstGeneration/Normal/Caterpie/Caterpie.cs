using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Caterpie
{
    public class Caterpie : ParentPokemon
    {
        public override int EvolveCost => 2;

        public override Type EvolveTo => typeof(Metapod.Metapod);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -14;
        }
    }
}