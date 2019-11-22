using System;
using Terraria;
using Terraria.ID;

namespace Terramon.Pokemon.FirstGeneration.Gastly
{
    public class GastlyNPC : ParentPokemonNPCNight
    {
        public override Type HomeClass() => typeof(Gastly);

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