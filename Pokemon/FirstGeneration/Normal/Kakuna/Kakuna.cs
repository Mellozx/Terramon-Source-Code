using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Kakuna
{
    public class Kakuna : ParentPokemon
    {
        public override int EvolveCost => 3;

        public override Type EvolveTo => typeof(Beedrill.Beedrill);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Bug, PokemonType.Poison };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -14;
        }
    }
}