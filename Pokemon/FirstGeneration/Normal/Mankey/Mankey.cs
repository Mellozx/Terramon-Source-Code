using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terramon.Players;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;
using static Terramon.Pokemon.ExpGroups;

namespace Terramon.Pokemon.FirstGeneration.Normal.Mankey
{
    public class Mankey : ParentPokemon
    {
        public override int EvolveCost => 23;

        public override Type EvolveTo => typeof(Primeape.Primeape);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Fighting };

        public virtual ExpGroup ExpGroup => ExpGroup.MediumFast;

        public override void SetDefaults()
        {
            base.SetDefaults();

            
            
        }
    }
}