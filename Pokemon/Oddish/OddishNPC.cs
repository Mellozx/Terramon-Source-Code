using System;
using Terraria;
using Terraria.ID;

namespace Terramon.Pokemon.Oddish
{
    public class OddishNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Oddish);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
        }

        public override void NPCLoot()
        {
            
        }
    }
}