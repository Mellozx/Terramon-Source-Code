using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Gastly
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