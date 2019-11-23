using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Charmander
{
    public class CharmanderNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Charmander);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
        }
    }
}