using System;

namespace Terramon.Pokemon.FirstGeneration.Shiny.Wartortle
{
    public class WartortleNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Wartortle);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
        }
    }
}