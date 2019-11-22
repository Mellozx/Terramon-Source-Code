using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Bulbasaur
{
    public class BulbasaurNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Bulbasaur);

        public override void SetDefaults()
        {
            base.SetDefaults();
			npc.width = 20;
			npc.height = 20;
        }
    }
}