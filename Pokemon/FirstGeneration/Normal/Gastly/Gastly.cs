using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Gastly
{
    public class Gastly : ParentPokemonGastly
    {
        public override int EvolveCost => 20;

        public override Type EvolveTo => typeof(Haunter.Haunter);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 38;
            projectile.height = 40;
            projectile.alpha = 75;
            drawOriginOffsetY = -36;
        }
    }
}