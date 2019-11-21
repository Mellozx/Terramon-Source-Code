using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terramon.Items.Pokeballs.Inventory;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terramon.Players
{
    public sealed partial class TerramonPlayer
    {
        private void InitializePokeballs()
        {
            ThrownPokeballs = new Dictionary<string, int>();
        }


        #region Thrown Pokeballs

        public void IncrementThrownPokeballs(BasePokeballItem pokeball) => IncrementThrownPokeballs(pokeball.UnlocalizedName);
        public int IncrementThrownPokeballs(string unlocalizedName) => ++ThrownPokeballs[unlocalizedName];

        public int GetThrownPokeballsCount(BasePokeballItem pokeball) => GetThrownPokeballsCount(pokeball.UnlocalizedName);
        public int GetThrownPokeballsCount(string unlocalizedName) => GetOrCreateThrownPokeballs(unlocalizedName);

        private int GetOrCreateThrownPokeballs(string unlocalizedName)
        {
            if (!ThrownPokeballs.ContainsKey(unlocalizedName))
                ThrownPokeballs.Add(unlocalizedName, 0);

            return ThrownPokeballs[unlocalizedName];
        }

        #endregion


        private void SavePokeballs(TagCompound tag)
        {
            tag.Add(nameof(ThrownPokeballs), ThrownPokeballs);
        }

        private void LoadPokeballs(TagCompound tag)
        {
            ThrownPokeballs = tag.Get<Dictionary<string, int>>(nameof(ThrownPokeballs));
        }


        public Dictionary<string, int> ThrownPokeballs { get; private set; }
    }
}
