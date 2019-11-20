using System;

namespace Terramon.Pokemon.Ivysaur
{
    public class IvysaurNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Ivysaur);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
        }
    }
}