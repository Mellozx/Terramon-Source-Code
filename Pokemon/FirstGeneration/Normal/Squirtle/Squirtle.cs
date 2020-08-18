using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Squirtle
{
    public class Squirtle : ParentPokemon
    {
        public override int EvolveCost => 11;

        public override Type EvolveTo => typeof(Wartortle.Wartortle);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Water };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 35;
            projectile.height = 40;
            drawOriginOffsetY = -1;
        }
    }
}