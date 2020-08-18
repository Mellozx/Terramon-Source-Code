namespace Terramon.Pokemon.FirstGeneration.Normal.Rattata
{
    public class Rattata : ParentPokemon
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Normal };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 35;
            projectile.height = 40;
        }
    }
}