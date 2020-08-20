using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Horsea
{
    public class Horsea : ParentPokemon
    {
        public override PokemonType[] PokemonTypes => new[] { PokemonType.Water };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 34; //-6
            projectile.height = 24; //-4
            drawOriginOffsetY = -20;
        }

        public override void AI()
        {
            base.AI();
            if (Main.rand.Next(12) == 0)
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 34, 0f, 0f, 100);
        }
    }
}