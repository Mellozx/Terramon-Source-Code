using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Nidorino
{
    public class Nidorino : ParentPokemon
    {
        public override int EvolveCost => 12;

        public override Type EvolveTo => typeof(Nidoking.Nidoking);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Poison };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}