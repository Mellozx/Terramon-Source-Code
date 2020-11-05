using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Clefairy
{
    public class Clefairy : ParentPokemon
    {
        public override int EvolveCost => 1;

        public override EvolveItem EvolveItem => EvolveItem.MoonStone;

        public override Type EvolveTo => typeof(Clefable.Clefable);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Fairy };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}