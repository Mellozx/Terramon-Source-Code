using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Haunter
{
    public class Haunter : ParentPokemonGastly
    {
        public override int EvolveCost => 1;

        public override EvolveItem EvolveItem => EvolveItem.LinkCable;

        public override Type EvolveTo => typeof(Gengar.Gengar);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Ghost, PokemonType.Poison };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}