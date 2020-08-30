using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Exeggcute
{
    public class Exeggcute : ParentPokemon
    {
        public override int EvolveCost => 26;

        public override Type EvolveTo => typeof(Exeggutor.Exeggutor);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Grass, PokemonType.Psychic };

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}