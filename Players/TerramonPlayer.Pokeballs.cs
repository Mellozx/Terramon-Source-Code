using System.Collections.Generic;
using Terramon.Items.Pokeballs.Inventory;
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

        public void IncrementThrownPokeballs(BasePokeballItem pokeball)
        {
            IncrementThrownPokeballs(pokeball.UnlocalizedName);
        }

        public int IncrementThrownPokeballs(string unlocalizedName)
        {
            return ThrownPokeballs[unlocalizedName] = GetOrCreateThrownPokeballs(unlocalizedName) + 1;
        }

        public int GetThrownPokeballsCount(BasePokeballItem pokeball)
        {
            return GetThrownPokeballsCount(pokeball.UnlocalizedName);
        }

        public int GetThrownPokeballsCount(string unlocalizedName)
        {
            return GetOrCreateThrownPokeballs(unlocalizedName);
        }

        private int GetOrCreateThrownPokeballs(string unlocalizedName)
        {
            if (!ThrownPokeballs.ContainsKey(unlocalizedName))
                ThrownPokeballs.Add(unlocalizedName, 0);

            return ThrownPokeballs[unlocalizedName];
        }

        #endregion


        private void SavePokeballs(TagCompound tag)
        {
            TagCompound thrownPokeballs = new TagCompound();

            foreach (KeyValuePair<string, int> kvp in ThrownPokeballs)
                thrownPokeballs.Add(kvp.Key, kvp.Value);

            tag.Add(nameof(ThrownPokeballs), thrownPokeballs);
        }

        private void LoadPokeballs(TagCompound tag)
        {
            ThrownPokeballs = new Dictionary<string, int>();

            foreach (KeyValuePair<string, object> kvp in tag.GetCompound(nameof(ThrownPokeballs)))
                ThrownPokeballs.Add(kvp.Key, int.Parse(kvp.Value.ToString()));
        }


        public Dictionary<string, int> ThrownPokeballs { get; private set; }
    }
}