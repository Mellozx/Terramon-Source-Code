using System;

namespace Terramon.Pokemon.Charmeleon
{
    public class CharmeleonNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Charmeleon);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
        }
    }
}