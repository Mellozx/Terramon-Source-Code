using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Venusaur
{
    public class Venusaur : ParentPokemon
    {
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