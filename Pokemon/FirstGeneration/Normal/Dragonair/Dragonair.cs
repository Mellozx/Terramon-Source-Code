using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Dragonair
{
    public class Dragonair : ParentPokemon
    {
        public override int EvolveCost => 25;

        public override Type EvolveTo => typeof(Dragonite.Dragonite);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Dragon };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -26;
        }
    }
}