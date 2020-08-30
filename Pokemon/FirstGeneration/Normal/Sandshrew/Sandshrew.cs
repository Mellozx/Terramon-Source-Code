using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Sandshrew
{
    public class Sandshrew : ParentPokemon
    {
        public override int EvolveCost => 17;

        public override Type EvolveTo => typeof(Sandslash.Sandslash);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Ground };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}