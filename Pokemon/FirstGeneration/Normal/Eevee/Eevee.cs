namespace Terramon.Pokemon.FirstGeneration.Normal.Eevee
{
    public class Eevee : ParentPokemon
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Normal };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 34; //-6
            projectile.height = 24; //-4
            drawOriginOffsetY = -20;
        }
    }
}