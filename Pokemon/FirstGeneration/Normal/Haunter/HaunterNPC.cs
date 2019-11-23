using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Haunter
{
    public class HaunterNPC : ParentPokemonNPCNight
    {
        public override Type HomeClass() => typeof(Haunter);

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