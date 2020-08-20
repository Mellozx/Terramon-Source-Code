namespace Terramon.Pokemon.FirstGeneration.Normal.Beedrill
{
    public class Beedrill : ParentPokemonFlying
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Bug, PokemonType.Poison };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
        }
    }
}