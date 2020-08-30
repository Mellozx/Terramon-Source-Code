using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Kadabra
{
    public class Kadabra : ParentPokemon
    {
        public override int EvolveCost => 16;

        public override Type EvolveTo => typeof(Alakazam.Alakazam);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Psychic };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}