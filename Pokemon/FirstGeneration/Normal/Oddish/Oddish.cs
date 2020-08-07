namespace Terramon.Pokemon.FirstGeneration.Normal.Oddish
{
    public class Oddish : ParentPokemon
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Grass, PokemonType.Poison };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 38;
            projectile.height = 40;
            drawOriginOffsetY = 4;
        }
    }
}