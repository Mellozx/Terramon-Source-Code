using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Koffing
{
    public class Koffing : ParentPokemon
    {
        public override int EvolveCost => 30;

        public override Type EvolveTo => typeof(Weezing.Weezing);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Poison };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}