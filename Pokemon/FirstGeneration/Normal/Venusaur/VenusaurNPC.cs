using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Venusaur
{
    public class VenusaurNPC : NotCatchablePKMN
    {
        public override Type HomeClass()
        {
            return typeof(Venusaur);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 24;
            npc.height = 24;
            npc.scale = 1f;
        }
    }
}