namespace Terramon.Pokemon.FirstGeneration.Normal.Raichu

{
    public class Raichu : ParentPokemon
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Electric };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 35;
            projectile.height = 35;
            drawOriginOffsetY = -11;
        }
    }
}