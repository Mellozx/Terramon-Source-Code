using System;

namespace Terramon.Pokemon.FirstGeneration.Pikachu
{
    public class PikachuNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Pikachu);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
        }
    }
}