using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.NidoranF
{
    public class Nidoranf : ParentPokemon
    {
        public override int EvolveCost => 11;

        public override Type EvolveTo => typeof(Nidorina.Nidorina);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Poison };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}