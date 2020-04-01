using Terramon.Pokemon.Moves;

namespace Terramon.Pokemon.FirstGeneration.Normal.Pidgeot
{
    public class Pidgeot : ParentPokemonFlying
    {
        public override string[] DefaultMove => new [] {nameof(ShootMove), nameof(HealMove),"",""};

        public override PokemonType[] PokemonTypes => new[] {PokemonType.Normal, PokemonType.Flying};

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 32;
            projectile.height = 32;
            drawOriginOffsetY = -8;
        }
    }
}