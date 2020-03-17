using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Weedle
{
    public class Weedle : ParentPokemon
    {
        public override int EvolveCost => 2;

        public override Type EvolveTo => typeof(Kakuna.Kakuna);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -10;
        }
    }
}