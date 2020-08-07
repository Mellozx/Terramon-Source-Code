using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Weedle
{
    public class Weedle : ParentPokemon
    {
        public override int EvolveCost => 2;

        public override Type EvolveTo => typeof(Kakuna.Kakuna);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Bug, PokemonType.Poison };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -10;
        }
    }
}