using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Diglett
{
    public class Diglett : ParentPokemon
    {
        public override int EvolveCost => 21;

        public override Type EvolveTo => typeof(Dugtrio.Dugtrio);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Ground };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}