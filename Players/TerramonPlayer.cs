using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terramon.Items.MiscItems;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Pokemon;
using Terramon.Pokemon.FirstGeneration.Fishing;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terramon.UI.Moveset;
using Terramon.UI.SidebarParty;
using Terramon.UI.Starter;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;


namespace Terramon.Players
{

    public sealed partial class TerramonPlayer : ModPlayer
    {

        public List<Item> list = new List<Item>();
        public List<Item> loadList = new List<Item>();

        public int deletepokecase = 0;
		public int premierBallRewardCounter = 0;

        protected Dictionary<string, bool> ActivePet = new Dictionary<string, bool>();
        public int ActivePetId = -1;
        public string ActivePetName = string.Empty;

        public bool pikachuPet = false;
        public bool raichuPet = false;
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
        public bool goldeenPet = false;
        public bool horseaPet = false;
        public bool magikarpPet = false;
        public bool gyaradosPet = false;
        public bool dratiniPet = false;
        public bool dragonairPet = false;
        public bool dragonitePet = false;
        public bool shinyMewPet = false;  
        public bool shinyEeveePet = false;
        public bool baPet1 = false;
        public bool piersPet = false;

        public int pkBallsThrown = 0;
        public int greatBallsThrown = 0;
        public int ultraBallsThrown = 0;
        public int pkmnCaught = 0;

        public int Language = 1;
        public int ItemNameColors = 1;

        private TagCompound _partySlot1 = null;
        private TagCompound _partySlot2 = null;
        private TagCompound _partySlot3 = null;
        private TagCompound _partySlot4 = null;
        private TagCompound _partySlot5 = null;
        private TagCompound _partySlot6 = null;
        public TagCompound PartySlot1
        {
            get { return _partySlot1; }
            set
            {
                _partySlot1 = value;
                if (value == null)
                {
                    ((TerramonMod)mod).PartySlots.partyslot1.Item.TurnToAir();
                    return;
                }

                if (!((TerramonMod) mod).PartySlots?.partyslot1?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod) mod).PartySlots.partyslot1.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active)
                    {
                        modItem.Load(value);
                    }
                }
            }
        }
        public TagCompound PartySlot2
        {
            get { return _partySlot2; }
            set
            {
                _partySlot2 = value;
                if (value == null)
                {
                    ((TerramonMod)mod).PartySlots.partyslot2.Item.TurnToAir();
                    return;
                }

                if (!((TerramonMod)mod).PartySlots?.partyslot2?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod)mod).PartySlots.partyslot2.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active)
                    {
                        modItem.Load(value);
                    }
                }
            }
        }
        public TagCompound PartySlot3
        {
            get { return _partySlot3; }
            set
            {
                _partySlot3 = value;
                if (value == null)
                {
                    ((TerramonMod)mod).PartySlots.partyslot3.Item.TurnToAir();
                    return;
                }

                if (!((TerramonMod)mod).PartySlots?.partyslot3?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod)mod).PartySlots.partyslot3.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active)
                    {
                        modItem.Load(value);
                    }
                }
            }
        }
        public TagCompound PartySlot4
        {
            get { return _partySlot4; }
            set
            {
                _partySlot4 = value;
                if (value == null)
                {
                    ((TerramonMod)mod).PartySlots.partyslot4.Item.TurnToAir();
                    return;
                }

                if (!((TerramonMod)mod).PartySlots?.partyslot4?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod)mod).PartySlots.partyslot4.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active)
                    {
                        modItem.Load(value);
                    }
                }
            }
        }
        public TagCompound PartySlot5
        {
            get { return _partySlot5; }
            set
            {
                _partySlot5 = value;
                if (value == null)
                {
                    ((TerramonMod)mod).PartySlots.partyslot5.Item.TurnToAir();
                    return;
                }
                if (!((TerramonMod)mod).PartySlots?.partyslot5?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod)mod).PartySlots.partyslot5.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active)
                    {
                        modItem.Load(value);
                    }
                }
            }
        }
        public TagCompound PartySlot6
        {
            get { return _partySlot6; }
            set
            {
                _partySlot6 = value;
                if (value == null)
                {
                    ((TerramonMod)mod).PartySlots.partyslot6.Item.TurnToAir();
                    return;
                }
                if (!((TerramonMod)mod).PartySlots?.partyslot6?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod)mod).PartySlots.partyslot6.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active)
                    {
                        modItem.Load(value);
                    }
                }
            }
        }


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
            //Initialise active pets bools
            var list = TerramonMod.GetPokemonsNames();
            ActivePet = new Dictionary<string, bool>();
            foreach (var it in list)
            {
                ActivePet.Add(it, false);
            }
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
            goldeenPet = false;
            horseaPet = false;
            magikarpPet = false;
            gyaradosPet = false;
            dratiniPet = false;
            dragonairPet = false;
            dragonitePet = false;
            shinyMewPet = false;
            shinyEeveePet = false;
            //Set any active pet to false
            foreach (var it in ActivePet.Keys.ToArray())
            {
                ActivePet[it] = false;
            }

        }

        

        /// <summary>
        /// Enable only one pet for player at once
        /// </summary>
        /// <param name="name">Pokemon type name</param>
        public void ActivatePet(string name)
        {
            ResetEffects();

            if (ActivePet.ContainsKey(name))
                ActivePet[name] = true;
            else
                throw new InvalidOperationException($"Pokemon {name} not registered! Please send log files to mod devs!");
        }

        public bool IsPetActive(string name)
        {

            if (ActivePet.ContainsKey(name))
                return ActivePet[name];

            throw new InvalidOperationException($"Pokemon {name} not registered! Please send log files to mod devs!");
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

        private void LoadPartySlot(Item modItem, TagCompound value)
        {
            //var modItem = ((TerramonMod)mod).PartySlots.partyslot1.Item.modItem;
            var en = (TerramonMod.PokeballFactory.Pokebals) value.GetByte(BaseCaughtClass.POKEBAL_PROPERTY);
            if (en == 0)
            {
                modItem.TurnToAir();
            }
            else
            {
                modItem.SetDefaults(TerramonMod.PokeballFactory.GetPokeballType(en));
                modItem.modItem.Load(value);
            }
        }

        public override void OnEnterWorld(Player player)
        {
            Moves.Visible = true;
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

            //TODO: Override sidebarUI here
            if(PartySlot1 != null)
                LoadPartySlot(((TerramonMod)mod).PartySlots.partyslot1.Item, PartySlot1);
            if (PartySlot2 != null)
                LoadPartySlot(((TerramonMod)mod).PartySlots.partyslot2.Item, PartySlot2);
            if (PartySlot3 != null)
                LoadPartySlot(((TerramonMod)mod).PartySlots.partyslot3.Item, PartySlot3);
            if (PartySlot4 != null)
                LoadPartySlot(((TerramonMod)mod).PartySlots.partyslot4.Item, PartySlot4);
            if (PartySlot5 != null)
                LoadPartySlot(((TerramonMod)mod).PartySlots.partyslot5.Item, PartySlot5);
            if (PartySlot6 != null)
                LoadPartySlot(((TerramonMod)mod).PartySlots.partyslot6.Item, PartySlot6);

            //Running one update to load sidebar without requiring to open inv
            ((TerramonMod)mod).PartySlots.UpdateUI(null);

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
        //fishing for pokemon
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (junk)
            {
                return;
            }
            if (liquidType == 0 && player.ZoneBeach && Main.rand.NextBool(6)) //16.7% chance from fishing
            {
                caughtType = ItemType<MagikarpFish>();
            }
            if (liquidType == 0 && player.ZoneBeach && Main.rand.NextBool(12)) //8.3% chance from fishing
            {
                caughtType = Main.rand.Next(new int[] { ItemType<GoldeenFish>(), ItemType<HorseaFish>() });
            }
            if (liquidType == 0 && Main.rand.NextBool(100)) //1% chance from fishing
            {
                caughtType = ItemType<MagikarpFish>();
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
                }
            }

            if (ChooseStarter.Visible || ChooseStarterBulbasaur.Visible || ChooseStarterCharmander.Visible || ChooseStarterSquirtle.Visible)
            {
                ClearNPCs();
            }

            //TODO: Move all buff icons in one folder
            var type = BuffType<PokemonBuff>();
            if (player.HasBuff(type) && !string.IsNullOrEmpty(ActivePetName))
            {
                Main.buffTexture[type] =
                    ModContent.GetTexture(
                        $"Terramon/Pokemon/FirstGeneration/Normal/{ActivePetName}/{ActivePetName}Buff");
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
                [nameof(StarterChosen)] = StarterChosen,
            };

            if (PartySlot1 != null && PartySlot1.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
            {
                tag.Add(nameof(PartySlot1), PartySlot1);
            }
            if (PartySlot2 != null && PartySlot2.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
            {
                tag.Add(nameof(PartySlot2), PartySlot2);
            }
            if (PartySlot3 != null && PartySlot3.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
            {
                tag.Add(nameof(PartySlot3), PartySlot3);
            }
            if (PartySlot4 != null && PartySlot4.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
            {
                tag.Add(nameof(PartySlot4), PartySlot4);
            }
            if (PartySlot5 != null && PartySlot5.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
            {
                tag.Add(nameof(PartySlot5), PartySlot5);
            }
            if (PartySlot6 != null && PartySlot6.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
            {
                tag.Add(nameof(PartySlot6), PartySlot6);
            }



            SavePokeballs(tag);

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            StarterChosen = tag.GetBool(nameof(StarterChosen));
            PartySlot1 = tag.ContainsKey(nameof(PartySlot1)) ? tag.GetCompound(nameof(PartySlot1)) : null;
            PartySlot2 = tag.ContainsKey(nameof(PartySlot2)) ? tag.GetCompound(nameof(PartySlot2)) : null;
            PartySlot3 = tag.ContainsKey(nameof(PartySlot3)) ? tag.GetCompound(nameof(PartySlot3)) : null;
            PartySlot4 = tag.ContainsKey(nameof(PartySlot4)) ? tag.GetCompound(nameof(PartySlot4)) : null;
            PartySlot5 = tag.ContainsKey(nameof(PartySlot5)) ? tag.GetCompound(nameof(PartySlot5)) : null;
            PartySlot6 = tag.ContainsKey(nameof(PartySlot6)) ? tag.GetCompound(nameof(PartySlot6)) : null;
            LoadPokeballs(tag);
        }


        public bool StarterChosen { get; set; }

        
        public bool StarterPackageBought { get; }
    }
}