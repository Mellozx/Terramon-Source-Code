using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Ponyta
{
    public class Ponyta : ParentPokemon
    {
        public override int EvolveCost => 35;

        public override Type EvolveTo => typeof(Rapidash.Rapidash);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Fire };

        public virtual ExpGroup ExpGroup => ExpGroup.MediumFast;

        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
}