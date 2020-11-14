using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Ninetales
{
    public class Ninetales : ParentPokemon
    {
        

        public override Type EvolveTo => typeof(Ninetales.Ninetales);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Fire };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}
