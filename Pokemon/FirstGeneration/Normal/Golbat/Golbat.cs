using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Golbat
{
    public class Golbat : ParentPokemonFlying
    {
        

        

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Poison, PokemonType.Flying };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}