using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Ponyta
{
    public class Ponyta : ParentPokemon
    {
        public override int EvolveCost => 35;

        public override Type EvolveTo => typeof(Rapidash.Rapidash);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Fire };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}