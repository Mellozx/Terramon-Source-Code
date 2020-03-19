using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Charmeleon
{
    public class Charmeleon : ParentPokemon
    {
        public override int EvolveCost => 20;

        public override Type EvolveTo => typeof(Charizard.Charizard);

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 22;
            projectile.height = 20;
            drawOriginOffsetY = -30;
        }

        public override void AI()
        {
            base.AI();
            if (Main.rand.Next(9) == 0)
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 100,
                    new Color(255, 91, 59));
        }
    }
}