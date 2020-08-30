using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Jigglypuff
{
    public class Jigglypuff : ParentPokemon
    {
        public override int EvolveCost => 12;

        public override Type EvolveTo => typeof(Wigglytuff.Wigglytuff);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Normal, PokemonType.Fairy };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}