using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Rattata
{
    public class RattataNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Rattata);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
        }
    }
}