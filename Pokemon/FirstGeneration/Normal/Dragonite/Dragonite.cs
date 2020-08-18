namespace Terramon.Pokemon.FirstGeneration.Normal.Dragonite
{
    public class Dragonite : ParentPokemonFlying
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Dragon, PokemonType.Flying };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            projectile.scale = 1.15f;
            drawOriginOffsetY = -8;
        }
    }
}