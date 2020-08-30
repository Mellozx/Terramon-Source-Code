using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Slowpoke
{
    public class Slowpoke : ParentPokemon
    {
        public override int EvolveCost => 32;

        public override Type EvolveTo => typeof(Slowbro.Slowbro);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Water, PokemonType.Psychic };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}