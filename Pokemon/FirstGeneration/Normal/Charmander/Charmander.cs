using System;
using Microsoft.Xna.Framework;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Charmander
{
    public class Charmander : ParentPokemon
    {
        public override int EvolveCost => 11;

        public override Type EvolveTo => typeof(Charmeleon.Charmeleon);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Fire };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 34;
            projectile.height = 26;
            drawOriginOffsetY = -14;
        }

        public override void AI()
        {
            base.AI();
            if (Main.rand.Next(9) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 100, new Color(255, 148, 41), 1f);
            }
        }
    }
}