using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Paras
{
    public class Paras : ParentPokemon
    {
        public override int EvolveCost => 19;

        public override Type EvolveTo => typeof(Parasect.Parasect);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Bug, PokemonType.Grass };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}