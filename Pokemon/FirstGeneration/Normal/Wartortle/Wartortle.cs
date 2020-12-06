using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;
using static Terramon.Pokemon.ExpGroups;

namespace Terramon.Pokemon.FirstGeneration.Normal.Wartortle
{
    public class Wartortle : ParentPokemon
    {
        public override int EvolveCost => 20;

        public override Type EvolveTo => typeof(Blastoise.Blastoise);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Water };

        public virtual ExpGroup ExpGroup => ExpGroup.MediumSlow;

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}