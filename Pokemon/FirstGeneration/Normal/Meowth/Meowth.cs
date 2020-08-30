using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Meowth
{
    public class Meowth : ParentPokemon
    {
        public override int EvolveCost => 23;

        public override Type EvolveTo => typeof(Persian.Persian);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Normal };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}