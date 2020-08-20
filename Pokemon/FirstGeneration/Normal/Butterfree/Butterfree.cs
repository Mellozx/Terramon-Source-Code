namespace Terramon.Pokemon.FirstGeneration.Normal.Butterfree
{
    public class Butterfree : ParentPokemonFlying
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Bug, PokemonType.Flying };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
        }
    }
}