using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Pidgeotto
{
    public class Pidgeotto : ParentPokemonFlying
    {
        public override int EvolveCost => 18;

        public override Type EvolveTo => typeof(Pidgeot.Pidgeot);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -8;
        }
    }
}