namespace Terramon.Pokemon.FirstGeneration.Normal.Arcanine
{
    public class Arcanine : ParentPokemon
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Fire };

        public virtual ExpGroup ExpGroup => ExpGroup.Slow;

        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
}