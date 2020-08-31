using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Magnemite
{
    public class Magnemite : ParentPokemonGastly
    {
        public override int EvolveCost => 25;

        public override Type EvolveTo => typeof(Magneton.Magneton);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Electric, PokemonType.Steel };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}