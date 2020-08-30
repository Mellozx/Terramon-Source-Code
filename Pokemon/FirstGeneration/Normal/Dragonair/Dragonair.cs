using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Dragonair
{
    public class Dragonair : ParentPokemon
    {
        public override int EvolveCost => 15;

        public override Type EvolveTo => typeof(Dragonite.Dragonite);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Dragon };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}