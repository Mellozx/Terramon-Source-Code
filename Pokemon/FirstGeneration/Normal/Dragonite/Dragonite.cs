using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Dragonite
{
    public class Dragonite : ParentPokemonFlying
    {
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