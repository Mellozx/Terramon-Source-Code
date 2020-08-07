using Terramon.Pokemon.FirstGeneration.Normal.Eevee;
using static Terraria.ModLoader.ModContent;

namespace Terramon.Items.MiscItems.Eggs
{
    public class EveeEgg : BaseEggsClass
    {
        public override int WaitTime => 20 * 60;        //20 minutes
        public override int PokemonToDropType => NPCType<EeveeNPC>();
        public override string PokemonName => "Eevee";
    }
}