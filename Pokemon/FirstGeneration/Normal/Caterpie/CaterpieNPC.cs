using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Caterpie
{
    public class CaterpieNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Caterpie);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
        }
    }
}