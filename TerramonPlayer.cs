using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Terramon
{
    public class TerramonPlayer : ModPlayer
    {
        //
        // Misc/Reg variables
        //

        public bool starterHasBeenChosen = false; // Has the starter been chosen yet?
        public static bool starterPackageNotBought = true;
        public int deletepokecase = 0;

        //
        // Pokemon PET variables
        //

        public bool pikachuPet = false;
        public bool bulbasaurPet = false;
        public bool ivysaurPet = false;
        public bool caterpiePet = false;
        public bool rattataPet = false;
        public bool squirtlePet = false;
        public bool wartortlePet = false;
        public bool charmanderPet = false;
        public bool charmeleonPet = false;
        public bool oddishPet = false;
        public bool eeveePet = false;
        public bool gloomPet = false;
        public bool gastlyPet = false;
        public bool haunterPet = false;
        public bool gengarPet = false;
        public bool shinyMewPet = false; // Ex
        public bool shinyEeveePet = false; // Ex
        public bool piersPet = false;

        //
        // Stat Vars
        //

        public int pkBallsThrown = 0;
        public int greatBallsThrown = 0;
        public int ultraBallsThrown = 0;
        public int pkmnCaught = 0;
        public override void ResetEffects()
        {
            pikachuPet = false;
            bulbasaurPet = false;
            ivysaurPet = false;
            squirtlePet = false;
            wartortlePet = false;
            charmanderPet = false;
            charmeleonPet = false;
            caterpiePet = false;
            rattataPet = false;
            eeveePet = false;
            oddishPet = false;
            gloomPet = false;
            gastlyPet = false;
            gengarPet = false;
            haunterPet = false;
            shinyMewPet = false;
            shinyEeveePet = false;
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            AddStartItem(ref items, mod.ItemType("Pokeball"), 12);
            AddStartItem(ref items, mod.ItemType("Pokedex"), 1);
            AddStartItem(ref items, mod.ItemType("Suitcase"), 1);
        }

        private void AddStartItem(ref IList<Item> items, int itemType, int stack)
        {
            Item item = new Item();
            item.SetDefaults(itemType);
            item.stack = stack;
            items.Add(item);
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(starterHasBeenChosen)] = starterHasBeenChosen
            };
        }

        public override void Load(TagCompound tag)
        {
            starterHasBeenChosen = tag.GetBool(nameof(starterHasBeenChosen));
            pkBallsThrown = tag.GetInt(nameof(pkBallsThrown));
        }
    }
}