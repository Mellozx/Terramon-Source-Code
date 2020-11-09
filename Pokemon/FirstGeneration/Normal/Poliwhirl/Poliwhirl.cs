using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Poliwhirl
{
    public class Poliwhirl : ParentPokemon
    {
        public override int EvolveCost => 1;

        public override EvolveItem EvolveItem => EvolveItem.WaterStone;

        public override Type EvolveTo => typeof(Poliwrath.Poliwrath);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Water };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}