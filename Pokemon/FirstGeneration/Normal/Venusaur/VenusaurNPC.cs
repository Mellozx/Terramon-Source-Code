using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Venusaur
{
    public class VenusaurNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Venusaur);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 35;
            npc.height = 35;
			npc.scale = 1.2f;
        }
    }
}