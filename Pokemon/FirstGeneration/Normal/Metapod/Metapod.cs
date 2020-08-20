using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Metapod
{
    public class Metapod : ParentPokemon
    {
        public override int EvolveCost => 20;

        public override Type EvolveTo => typeof(Butterfree.Butterfree);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Bug };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -14;
        }
    }
}