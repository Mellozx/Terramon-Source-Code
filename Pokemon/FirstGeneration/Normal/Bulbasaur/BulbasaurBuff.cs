using Terramon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Bulbasaur
{
    public class BulbasaurBuff : PokemonBuff
    {
        public override string ProjectileName { get; } = nameof(Bulbasaur);

        public override void SetDefaults()
        {
            base.SetDefaults();
            DisplayName.SetDefault("Bulbasaur");
            Description.SetDefault("A Bulbasaur is following you around!");
        }
    }
}