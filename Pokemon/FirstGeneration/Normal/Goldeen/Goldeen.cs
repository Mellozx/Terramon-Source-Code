using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;
using static Terramon.Pokemon.ExpGroups;

namespace Terramon.Pokemon.FirstGeneration.Normal.Goldeen
{
    public class Goldeen : ParentPokemon
    {
        public override int EvolveCost => 28;

        public override Type EvolveTo => typeof(Seaking.Seaking);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Water };

        public virtual ExpGroup ExpGroup => ExpGroup.MediumFast;

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}