using System;
using System.Collections.Generic;
using Terramon.Items.MiscItems;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Pokemon;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terramon.UI;
using Terramon.UI.SidebarParty;
using Terramon.UI.Starter;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace Terramon.Players
{

    public sealed partial class TerramonPlayer : ModPlayer
    {
        //
        // Misc/Reg variables
        //

        public int deletepokecase = 0;
		public int premierBallRewardCounter = 0;

        //
        // Pokemon PET variables
        //

        public bool pikachuPet = false;
        public bool bulbasaurPet = false;
        public bool ivysaurPet = false;
		public bool venusaurPet = false;
        public bool caterpiePet = false;
        public bool rattataPet = false;
        public bool squirtlePet = false;
        public bool wartortlePet = false;
		public bool blastoisePet = false;
        public bool charmanderPet = false;
        public bool charmeleonPet = false;
		public bool charizardPet = false;
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

        //
        // Config
        //

        public int Language = 1;
        public int ItemNameColors = 1;


        public static TerramonPlayer Get() => Get(Main.LocalPlayer);
        public static TerramonPlayer Get(Player player) => player.GetModPlayer<TerramonPlayer>();


        public override void Initialize()
        {
            InitializePokeballs();
        }

        public override void ResetEffects()
        {
            pikachuPet = false;
            bulbasaurPet = false;
            ivysaurPet = false;
			venusaurPet = false;
            squirtlePet = false;
            wartortlePet = false;
			blastoisePet = false;
            charmanderPet = false;
            charmeleonPet = false;
			charizardPet = false;
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
            AddStartItem(ref items, ModContent.ItemType<PokeballItem>(), 8);
            AddStartItem(ref items, ModContent.ItemType<Pokedex>());
            AddStartItem(ref items, ModContent.ItemType<Suitcase>());
        }

        public static bool MyUIStateActive(Player player)
        {
            return ChooseStarter.Visible;
        }

        public override void OnEnterWorld(Player player)
        {
            // Starting the Starter Selection screen!
            if (StarterChosen == false)
            {
                ModContent.GetInstance<TerramonMod>()._exampleUserInterface.SetState(new ChooseStarter());
                ChooseStarter.Visible = true;
            }
            else
            {
                UISidebar.Visible = true;
            }
        }
        public override void PreUpdate()
        {
            if (Main.playerInventory)
            {
                UISidebar.Visible = false;
                PartySlots.Visible = true;
            }
            else
            {
                UISidebar.Visible = true;
                PartySlots.Visible = false;
            }
        }


        private void AddStartItem(ref IList<Item> items, int itemType, int stack = 1)
        {
            Item item = new Item();

            item.SetDefaults(itemType);
            item.stack = stack;

            items.Add(item);
        }
		
		public override void PostBuyItem(NPC vendor, Item[] shop, Item item)
		{
			if (vendor.type == ModContent.NPCType<PokemonTrainer>() && item.type == ModContent.ItemType<PokeballItem>())
			{
				TerramonPlayer p = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
				p.premierBallRewardCounter++;
				if (p.premierBallRewardCounter == 10)
				{
					p.premierBallRewardCounter = 0;
					player.QuickSpawnItem(ModContent.ItemType<PremierBallItem>());
				}
			}
		}
        public override void PreSavePlayer()
        {
            
        }


        public override TagCompound Save()
        {
            TagCompound terramonTag = new TagCompound()
            {
                [nameof(StarterChosen)] = StarterChosen
            };

            SavePokeballs(terramonTag);
            SaveParty(terramonTag);

            return terramonTag;
            //ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem
        }

        public override void Load(TagCompound terramonTag)
        {
            StarterChosen = terramonTag.GetBool(nameof(StarterChosen));

            LoadPokeballs(terramonTag);
            LoadParty(terramonTag);
        }


        /// <summary>true if the starter pokemon has been chosen; otherwise false.</summary>
        public bool StarterChosen { get; set; }

        
        public bool StarterPackageBought { get; }
    }
}