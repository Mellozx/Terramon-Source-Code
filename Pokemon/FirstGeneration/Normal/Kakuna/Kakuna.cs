using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Kakuna
{
    public class Kakuna : ParentPokemon
    {
        public override int EvolveCost => 3;

        public override Type EvolveTo => typeof(Beedrill.Beedrill);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -14;
        }
    }
}