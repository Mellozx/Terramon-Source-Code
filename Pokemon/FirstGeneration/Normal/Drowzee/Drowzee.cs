using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;
using static Terramon.Pokemon.ExpGroups;

namespace Terramon.Pokemon.FirstGeneration.Normal.Drowzee
{
    public class Drowzee : ParentPokemon
    {
        public override int EvolveCost => 21;

        public override Type EvolveTo => typeof(Hypno.Hypno);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Psychic };

        public virtual ExpGroup ExpGroup => ExpGroup.MediumFast;

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}