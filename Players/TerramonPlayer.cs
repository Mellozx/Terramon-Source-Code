using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terramon.Items.MiscItems;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Pokemon;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terramon.UI.SidebarParty;
using Terramon.UI.Starter;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace Terramon.Players
{

    public sealed partial class TerramonPlayer : ModPlayer
    {

        public List<Item> list = new List<Item>();
        public List<Item> loadList = new List<Item>();

        public int deletepokecase = 0;
		public int premierBallRewardCounter = 0;

        public bool pikachuPet = false;
        public bool bulbasaurPet = false;
        public bool ivysaurPet = false;
		public bool venusaurPet = false;
        public bool caterpiePet = false;
        public bool metapodPet = false;
        public bool butterfreePet = false;
        public bool weedlePet = false;
        public bool kakunaPet = false;
        public bool beedrillPet = false;
        public bool rattataPet = false;
        public bool pidgeyPet = false;
        public bool pidgeottoPet = false;
        public bool pidgeotPet = false;
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
        public bool dratiniPet = false;
        public bool dragonairPet = false;
        public bool dragonitePet = false;
        public bool shinyMewPet = false;  
        public bool shinyEeveePet = false;  
        public bool piersPet = false;

        public int pkBallsThrown = 0;
        public int greatBallsThrown = 0;
        public int ultraBallsThrown = 0;
        public int pkmnCaught = 0;

        public int Language = 1;
        public int ItemNameColors = 1;

        public int firstslottype = 1;
        public string firstslotname = "*";    
        public int secondslottype = 1;
        public string secondslotname = "*";    
        public int thirdslottype = 1;
        public string thirdslotname = "*";    
        public int fourthslottype = 1;
        public string fourthslotname = "*";    
        public int fifthslottype = 1;
        public string fifthslotname = "*";    
        public int sixthslottype = 1;
        public string sixthslotname = "*";

        public int CycleIndex;


        public static TerramonPlayer Get() => Get(Main.LocalPlayer);
        public static TerramonPlayer Get(Player player) => player.GetModPlayer<TerramonPlayer>();

        public int CatchIndex { get; internal set; }


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
            metapodPet = false;
            butterfreePet = false;
            weedlePet = false;
            kakunaPet = false;
            beedrillPet = false;
            rattataPet = false;
            pidgeyPet = false;
            pidgeottoPet = false;
            pidgeotPet = false;
            eeveePet = false;
            oddishPet = false;
            gloomPet = false;
            gastlyPet = false;
            gengarPet = false;
            haunterPet = false;
            dratiniPet = false;
            dragonairPet = false;
            dragonitePet = false;
            shinyMewPet = false;
            shinyEeveePet = false;
        }


        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            AddStartItem(ref items, ModContent.ItemType<PokeballItem>(), 8);
            AddStartItem(ref items, ModContent.ItemType<Pokedex>());
        }

        public static bool MyUIStateActive(Player player)
        {
            return ChooseStarter.Visible;
        }

        public override void OnEnterWorld(Player player)
        {
            Mod leveledMod = ModLoader.GetMod("Leveled");
            Mod overhaulMod = ModLoader.GetMod("TerrariaOverhaul");
            if (leveledMod != null)
            {
                Main.NewText("Terramon is not compatible with the 'Leveled' mod, which is currently enabled. To prevent mod-breaking bugs, please disable one or the other.", 245, 46, 24, false);
            }
            if (overhaulMod != null)
            {
                Main.NewText("Terramon is not compatible with the 'Terraria Overhaul' mod, which is currently enabled. To prevent mod-breaking bugs, please disable one or the other.", 245, 46, 24, false);
            }
            PartySlots partySlots = ModContent.GetInstance<TerramonMod>().PartySlots;
            if (!partySlots.partyslot1.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partySlots.partyslot1.Item);
            }
            if (!partySlots.partyslot2.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partySlots.partyslot2.Item);
            }
            if (!partySlots.partyslot3.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partySlots.partyslot3.Item);
            }
            if (!partySlots.partyslot4.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partySlots.partyslot4.Item);
            }
            if (!partySlots.partyslot5.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partySlots.partyslot5.Item);
            }
            if (!partySlots.partyslot6.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partySlots.partyslot6.Item);
            }
            if (StarterChosen == false)
            {
                ModContent.GetInstance<TerramonMod>()._exampleUserInterface.SetState(new ChooseStarter());
                ChooseStarter.Visible = true;
                PartySlots.Visible = false;
                UISidebar.Visible = false;
            }
            else
            {
                UISidebar.Visible = true;
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (TerramonMod.PartyCycle.JustPressed)
            {
                if (!player.HasBuff(mod.BuffType(firstslotname + "Buff")) && firstslotname != "*")
                {
                    player.AddBuff(mod.BuffType(firstslotname + "Buff"), 2);
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
                    CombatText.NewText(player.Hitbox, Color.White, "Go! " + firstslotname + "!", true, false);
                }
            }
        }
        public override void PreUpdate()
        {
            if (StarterChosen == true)
            {
                if (Main.playerInventory)
                {
                    if (player.chest != -1 || Main.npcShop != 0 || EvolveUI.Visible)
                    {
                        PartySlots.Visible = false;
                    }
                    else
                    {
                        PartySlots.Visible = true;
                    }
                    UISidebar.Visible = false;
                }
                else
                {
                    EvolveUI.Visible = false;
                    UISidebar.Visible = true;
                    PartySlots.Visible = false;
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.TurnToAir();
                    }
                    

                    if (!ModContent.GetInstance<TerramonMod>().evolveUI.partyslot1.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().evolveUI.partyslot1.Item, ModContent.GetInstance<TerramonMod>().evolveUI.partyslot1.Item.stack);
                        ModContent.GetInstance<TerramonMod>().evolveUI.partyslot1.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().evolveUI.partyslot2.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().evolveUI.partyslot2.Item, ModContent.GetInstance<TerramonMod>().evolveUI.partyslot2.Item.stack);
                        ModContent.GetInstance<TerramonMod>().evolveUI.partyslot2.Item.TurnToAir();
                    }
                }
            }

            if (ChooseStarter.Visible || ChooseStarterBulbasaur.Visible || ChooseStarterCharmander.Visible || ChooseStarterSquirtle.Visible)
            {
                ClearNPCs();
            }
        }

        public override void UpdateAutopause()
        {
            base.UpdateAutopause();
            if (StarterChosen == true)
            {
                if (Main.playerInventory)
                {
                    if (player.chest != -1 || Main.npcShop != 0 || EvolveUI.Visible)
                    {
                        PartySlots.Visible = false;
                    }
                    else
                    {
                        PartySlots.Visible = true;
                    }
                    UISidebar.Visible = false;
                }
                else
                {
                    EvolveUI.Visible = false;
                    UISidebar.Visible = true;
                    PartySlots.Visible = false;
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item, ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.stack);
                        ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.TurnToAir();
                    }


                    if (!ModContent.GetInstance<TerramonMod>().evolveUI.partyslot1.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().evolveUI.partyslot1.Item, ModContent.GetInstance<TerramonMod>().evolveUI.partyslot1.Item.stack);
                        ModContent.GetInstance<TerramonMod>().evolveUI.partyslot1.Item.TurnToAir();
                    }
                    if (!ModContent.GetInstance<TerramonMod>().evolveUI.partyslot2.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(ModContent.GetInstance<TerramonMod>().evolveUI.partyslot2.Item, ModContent.GetInstance<TerramonMod>().evolveUI.partyslot2.Item.stack);
                        ModContent.GetInstance<TerramonMod>().evolveUI.partyslot2.Item.TurnToAir();
                    }
                }
            }

            if (ChooseStarter.Visible || ChooseStarterBulbasaur.Visible || ChooseStarterCharmander.Visible || ChooseStarterSquirtle.Visible)
            {
                ClearNPCs();
            }
        }

        public static void ClearNPCs()
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i] != null && !Main.npc[i].townNPC)
                {
                    Main.npc[i].life = 0;
                    if (Main.netMode == 2) NetMessage.SendData(23, -1, -1, null, i, 0f, 0f, 0f, 0);
                }
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
        


        public override TagCompound Save()
        {
            PartySlots partySlots = ModContent.GetInstance<TerramonMod>().PartySlots;
            if (partySlots.partyslot1.Item.IsAir)
            {
                firstslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.Recalculate();
            }
            if (partySlots.partyslot2.Item.IsAir)
            {
                secondslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.Recalculate();
            }
            if (partySlots.partyslot3.Item.IsAir)
            {
                thirdslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.Recalculate();
            }
            if (partySlots.partyslot4.Item.IsAir)
            {
                fourthslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.Recalculate();
            }
            if (partySlots.partyslot5.Item.IsAir)
            {
                fifthslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.Recalculate();
            }
            if (partySlots.partyslot6.Item.IsAir)
            {
                sixthslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.Recalculate();
            }

            TagCompound tag = new TagCompound()
            {
                [nameof(StarterChosen)] = StarterChosen
            };

            SavePokeballs(tag);

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            StarterChosen = tag.GetBool(nameof(StarterChosen));
            

            LoadPokeballs(tag);
        }


        public bool StarterChosen { get; set; }

        
        public bool StarterPackageBought { get; }
    }
}