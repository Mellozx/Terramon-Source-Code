namespace Terramon.Pokemon.FirstGeneration.Normal.Venusaur
{
    public class Venusaur : ParentPokemon
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Grass, PokemonType.Poison };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 48;
            projectile.height = 40;
            projectile.scale = 1f;
            // drawOriginOffsetY = -1;
        }
    }
}