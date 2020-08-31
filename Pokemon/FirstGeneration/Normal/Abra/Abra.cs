using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Abra
{
    public class Abra : ParentPokemonGastly
    {
        public override int EvolveCost => 11;

        public override Type EvolveTo => typeof(Kadabra.Kadabra);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Psychic };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}