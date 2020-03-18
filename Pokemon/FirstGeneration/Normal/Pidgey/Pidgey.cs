using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Pidgey
{
    public class Pidgey : ParentPokemon
    {
        public override int EvolveCost => 13;

        public override Type EvolveTo => typeof(Pidgeotto.Pidgeotto);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 34; //-6
            projectile.height = 24; //-4
            drawOriginOffsetY = -17;
        }
    }
}