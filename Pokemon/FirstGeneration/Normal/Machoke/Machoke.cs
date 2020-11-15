using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Machoke
{
    public class Machoke : ParentPokemon
    {
        public override int EvolveCost => 1;

        public override EvolveItem EvolveItem => EvolveItem.LinkCable;
        
        public override Type EvolveTo => typeof(Machamp.Machamp);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Fighting };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}
