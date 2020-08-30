using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Rattata
{
    public class Rattata : ParentPokemon
    {
        public override int EvolveCost => 15;

        public override Type EvolveTo => typeof(Raticate.Raticate);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Normal };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}