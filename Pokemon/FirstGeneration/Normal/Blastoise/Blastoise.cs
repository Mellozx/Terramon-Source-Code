namespace Terramon.Pokemon.FirstGeneration.Normal.Blastoise
{
    public class Blastoise : ParentPokemon
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Water };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 48;
            projectile.height = 40;
            projectile.scale = 1f;
            drawOriginOffsetY = -14;
        }
    }
}