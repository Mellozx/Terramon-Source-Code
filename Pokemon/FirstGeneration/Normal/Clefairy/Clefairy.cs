using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;
using static Terramon.Pokemon.ExpGroups;

namespace Terramon.Pokemon.FirstGeneration.Normal.Clefairy
{
    public class Clefairy : ParentPokemon
    {
        public override int EvolveCost => 1;

        public override EvolveItem EvolveItem => EvolveItem.MoonStone;

        public override Type EvolveTo => typeof(Clefable.Clefable);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Fairy };

        public virtual ExpGroup ExpGroup => ExpGroup.Fast;

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}