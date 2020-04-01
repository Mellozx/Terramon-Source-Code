using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Microsoft.Xna.Framework;
using Terramon.Items.MiscItems;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Pokemon;
using Terramon.Pokemon.FirstGeneration.Fishing;
using Terramon.Pokemon.Moves;
using Terramon.UI.Moveset;
using Terramon.UI.SidebarParty;
using Terramon.UI.Starter;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
// ReSharper disable ParameterHidesMember
// ReSharper disable LocalVariableHidesMember


namespace Terramon.Players
{
    public sealed partial class TerramonPlayer : ModPlayer
    {
        public List<Item> list = new List<Item>();
        public List<Item> loadList = new List<Item>();

        public int deletepokecase = 0;
        public int premierBallRewardCounter;

        private Dictionary<string, bool> ActivePet = new Dictionary<string, bool>();
        public int ActivePetId = -1;
        public string ActivePetName = string.Empty;
        public bool CombatReady;
        public bool AutoUse;

        public bool Attacking = false;

        public int ActivePartySlot
        {
            get => _activePartySlot;
            set
            {
                _activePartySlot = value;

                MoveSet = new BaseMove[] {null, null, null, null};

                TagCompound tag;
                switch (value)
                {
                    case 1:
                        tag = PartySlot1;
                        break;
                    case 2:
                        tag = PartySlot2;
                        break;
                    case 3:
                        tag = PartySlot3;
                        break;
                    case 4:
                        tag = PartySlot4;
                        break;
                    case 5:
                        tag = PartySlot5;
                        break;
                    case 6:
                        tag = PartySlot6;
                        break;
                    default:
                        return;
                }

                var m1 = tag.ContainsKey(BaseCaughtClass.MOVE1) ? tag.GetString(BaseCaughtClass.MOVE1) : null;
                var m2 = tag.ContainsKey(BaseCaughtClass.MOVE2) ? tag.GetString(BaseCaughtClass.MOVE2) : null;
                var m3 = tag.ContainsKey(BaseCaughtClass.MOVE3) ? tag.GetString(BaseCaughtClass.MOVE3) : null;
                var m4 = tag.ContainsKey(BaseCaughtClass.MOVE4) ? tag.GetString(BaseCaughtClass.MOVE4) : null;
                MoveSet[0] = !string.IsNullOrEmpty(m1) ? TerramonMod.GetMove(m1) : null;
                MoveSet[1] = !string.IsNullOrEmpty(m2) ? TerramonMod.GetMove(m2) : null;
                MoveSet[2] = !string.IsNullOrEmpty(m3) ? TerramonMod.GetMove(m3) : null;
                MoveSet[3] = !string.IsNullOrEmpty(m4) ? TerramonMod.GetMove(m4) : null;
            }
        }

        private int _activePartySlot = -1;
        public BaseMove[] MoveSet;
        public int Cooldown;

        public int pkBallsThrown = 0;
        public int greatBallsThrown = 0;
        public int ultraBallsThrown = 0;
        public int pkmnCaught = 0;

        public int Language = 1;
        public int ItemNameColors = 1;

        private TagCompound _partySlot1;
        private TagCompound _partySlot2;
        private TagCompound _partySlot3;
        private TagCompound _partySlot4;
        private TagCompound _partySlot5;
        private TagCompound _partySlot6;

        public TagCompound PartySlot1
        {
            get => _partySlot1;
            set
            {
                _partySlot1 = value;
                if (value == null)
                {
                    ((TerramonMod) mod).PartySlots.partyslot1.Item.TurnToAir();
                    return;
                }

                if (!((TerramonMod) mod).PartySlots?.partyslot1?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod) mod).PartySlots.partyslot1.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active) modItem.Load(value);
                }
            }
        }

        public TagCompound PartySlot2
        {
            get => _partySlot2;
            set
            {
                _partySlot2 = value;
                if (value == null)
                {
                    ((TerramonMod) mod).PartySlots.partyslot2.Item.TurnToAir();
                    return;
                }

                if (!((TerramonMod) mod).PartySlots?.partyslot2?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod) mod).PartySlots.partyslot2.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active) modItem.Load(value);
                }
            }
        }

        public TagCompound PartySlot3
        {
            get => _partySlot3;
            set
            {
                _partySlot3 = value;
                if (value == null)
                {
                    ((TerramonMod) mod).PartySlots.partyslot3.Item.TurnToAir();
                    return;
                }

                if (!((TerramonMod) mod).PartySlots?.partyslot3?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod) mod).PartySlots.partyslot3.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active) modItem.Load(value);
                }
            }
        }

        public TagCompound PartySlot4
        {
            get => _partySlot4;
            set
            {
                _partySlot4 = value;
                if (value == null)
                {
                    ((TerramonMod) mod).PartySlots.partyslot4.Item.TurnToAir();
                    return;
                }

                if (!((TerramonMod) mod).PartySlots?.partyslot4?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod) mod).PartySlots.partyslot4.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active) modItem.Load(value);
                }
            }
        }

        public TagCompound PartySlot5
        {
            get => _partySlot5;
            set
            {
                _partySlot5 = value;
                if (value == null)
                {
                    ((TerramonMod) mod).PartySlots.partyslot5.Item.TurnToAir();
                    return;
                }

                if (!((TerramonMod) mod).PartySlots?.partyslot5?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod) mod).PartySlots.partyslot5.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active) modItem.Load(value);
                }
            }
        }

        public TagCompound PartySlot6
        {
            get => _partySlot6;
            set
            {
                _partySlot6 = value;
                if (value == null)
                {
                    ((TerramonMod) mod).PartySlots.partyslot6.Item.TurnToAir();
                    return;
                }

                if (!((TerramonMod) mod).PartySlots?.partyslot6?.Item?.IsAir ?? false)
                {
                    //We need to update data inside item
                    var modItem = ((TerramonMod) mod).PartySlots.partyslot6.Item.modItem;
                    if (modItem != null && modItem.item != null && modItem.item.active) modItem.Load(value);
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


        public static TerramonPlayer Get()
        {
            return Get(Main.LocalPlayer);
        }

        public static TerramonPlayer Get(Player player)
        {
            return player.GetModPlayer<TerramonPlayer>();
        }

        public int CatchIndex { get; internal set; }


        public override void Initialize()
        {
            InitializePokeballs();
            //Initialise active pets bools
            // ReSharper disable once LocalVariableHidesMember
            var list = TerramonMod.GetPokemonsNames();
            ActivePet = new Dictionary<string, bool>();
            foreach (var it in list) ActivePet.Add(it, false);
        }

        public override void ResetEffects()
        {
            //Set any active pet to false
            foreach (var it in ActivePet.Keys.ToArray()) ActivePet[it] = false;
        }


        /// <summary>
        ///     Enable only one pet for player at once
        /// </summary>
        /// <param name="name">Pokemon type name</param>
        /// <param name="combatReady">This pokemon summoned from party UI?</param>
        public void ActivatePet(string name, bool combatReady = true)
        {
            ResetEffects();

            if (string.IsNullOrEmpty(name) || name == "*")
            {
                ActivePetId = -1;
                ActivePetName = "";
                CombatReady = false;
                return;
            }

            if (!combatReady)
                ActivePartySlot = -1;
            CombatReady = combatReady;

            if (ActivePet.ContainsKey(name))
                ActivePet[name] = true;
            else
                throw new InvalidOperationException(
                    $"Pokemon {name} not registered! Please send log files to mod devs!");
        }

        public bool IsPetActive(string name)
        {
            if (ActivePet.ContainsKey(name))
                return ActivePet[name];

            throw new InvalidOperationException($"Pokemon {name} not registered! Please send log files to mod devs!");
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            AddStartItem(ref items, ItemType<PokeballItem>(), 8);
            AddStartItem(ref items, ItemType<Pokedex>());
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
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            modPlayer.Attacking = false;
            Moves.Visible = true;
            Mod leveledMod = ModLoader.GetMod("Leveled");
            Mod overhaulMod = ModLoader.GetMod("TerrariaOverhaul");
            if (leveledMod != null)
                Main.NewText(
                    "Terramon is not compatible with the 'Leveled' mod, which is currently enabled. To prevent mod-breaking bugs, please disable one or the other.",
                    245, 46, 24);
            if (overhaulMod != null)
                Main.NewText(
                    "Terramon is not compatible with the 'Terraria Overhaul' mod, which is currently enabled. To prevent mod-breaking bugs, please disable one or the other.",
                    245, 46, 24);

            //TODO: Override sidebarUI here
            if (PartySlot1 != null)
                LoadPartySlot(((TerramonMod) mod).PartySlots.partyslot1.Item, PartySlot1);
            if (PartySlot2 != null)
                LoadPartySlot(((TerramonMod) mod).PartySlots.partyslot2.Item, PartySlot2);
            if (PartySlot3 != null)
                LoadPartySlot(((TerramonMod) mod).PartySlots.partyslot3.Item, PartySlot3);
            if (PartySlot4 != null)
                LoadPartySlot(((TerramonMod) mod).PartySlots.partyslot4.Item, PartySlot4);
            if (PartySlot5 != null)
                LoadPartySlot(((TerramonMod) mod).PartySlots.partyslot5.Item, PartySlot5);
            if (PartySlot6 != null)
                LoadPartySlot(((TerramonMod) mod).PartySlots.partyslot6.Item, PartySlot6);

            //Running one update to load sidebar without requiring to open inv
            ((TerramonMod) mod).PartySlots.UpdateUI(null);

            if (StarterChosen == false)
            {
                GetInstance<TerramonMod>()._exampleUserInterface.SetState(new ChooseStarter());
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
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize,
            int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (junk) return;
            if (liquidType == 0 && player.ZoneBeach && Main.rand.NextBool(6)) //16.7% chance from fishing
                caughtType = ItemType<MagikarpFish>();
            if (liquidType == 0 && player.ZoneBeach && Main.rand.NextBool(12)) //8.3% chance from fishing
                caughtType = Main.rand.Next(new[] {ItemType<GoldeenFish>(), ItemType<HorseaFish>()});
            if (liquidType == 0 && Main.rand.NextBool(100)) //1% chance from fishing
                caughtType = ItemType<MagikarpFish>();
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (TerramonMod.PartyCycle.JustPressed)
                if (!player.HasBuff(mod.BuffType(firstslotname + "Buff")) && firstslotname != "*")
                {
                    player.AddBuff(mod.BuffType(firstslotname + "Buff"), 2);
                    Main.PlaySound(GetInstance<TerramonMod>()
                        .GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
                    CombatText.NewText(player.Hitbox, Color.White, "Go! " + firstslotname + "!", true);
                }
        }

        public override void PreUpdate()
        {
            if (StarterChosen)
            {
                if (Main.playerInventory)
                {
                    if (player.chest != -1 || Main.npcShop != 0 || EvolveUI.Visible)
                        PartySlots.Visible = false;
                    else
                        PartySlots.Visible = true;
                    UISidebar.Visible = false;
                }
                else
                {
                    EvolveUI.Visible = false;
                    UISidebar.Visible = true;
                    PartySlots.Visible = false;
                }
            }

            if (ChooseStarter.Visible || ChooseStarterBulbasaur.Visible || ChooseStarterCharmander.Visible ||
                ChooseStarterSquirtle.Visible) ClearNPCs();

            if (!Main.dedServ)
            {
                var type = BuffType<PokemonBuff>();
                if (player.HasBuff(type) && !string.IsNullOrEmpty(ActivePetName))
                    Main.buffTexture[type] =
                        GetTexture(
                            $"Terramon/Buffs/{ActivePetName}Buff");
            }

            //Note: If you compile code from VS -> moves don't have cooldowns
            if (Cooldown > 0 && ActiveMove == null)
#if DEBUG
                Cooldown = 0;
#else
                Cooldown--;
#endif

            //Moves logic
            if (CombatReady && ActivePartySlot > 0 && ActivePartySlot <= 6 && ActivePetId != -1
                && Main.projectile[ActivePetId].modProjectile is ParentPokemon) //Integrity check
            {
                if (ActiveMove != null)
                {
                    if (!ActiveMove.Update(Main.projectile[ActivePetId],
                        (ParentPokemon) Main.projectile[ActivePetId].modProjectile, this))
                        ActiveMove = null;
                }
                else if (Cooldown <= 0)
                {
                    var mod = (TerramonMod) this.mod;
                    if (AutoUse)
                    {
                        var f1 = MoveSet[0]?.AutoUseWeight(Main.projectile[ActivePetId],
                                         (ParentPokemon) Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this) ?? 0;
                        var f2 = MoveSet[1]?.AutoUseWeight(Main.projectile[ActivePetId],
                                         (ParentPokemon)Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this) ?? 0;
                        var f3 = MoveSet[2]?.AutoUseWeight(Main.projectile[ActivePetId],
                                         (ParentPokemon)Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this) ?? 0;
                        var f4 = MoveSet[3]?.AutoUseWeight(Main.projectile[ActivePetId],
                                         (ParentPokemon)Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this) ?? 0;
                        var sum = f1 + f2 + f3 + f4 + 1500; //1500 is idle
                        var w = Main.rand.Next(sum);
                        if (w < f1)
                        {
                            ActiveMove = MoveSet[0];
                            if (ActiveMove.PerformInWorld(Main.projectile[ActivePetId],
                                (ParentPokemon)Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this))
                                Cooldown = ActiveMove.Cooldown;
                            else
                                ActiveMove = null;
                        }
                        else if(w < f1 + f2)
                        {
                            ActiveMove = MoveSet[1];
                            if (ActiveMove.PerformInWorld(Main.projectile[ActivePetId],
                                (ParentPokemon)Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this))
                                Cooldown = ActiveMove.Cooldown;
                            else
                                ActiveMove = null;
                        }
                        else if (w < f1 + f2 + f3)
                        {
                            ActiveMove = MoveSet[2];
                            if (ActiveMove.PerformInWorld(Main.projectile[ActivePetId],
                                (ParentPokemon)Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this))
                                Cooldown = ActiveMove.Cooldown;
                            else
                                ActiveMove = null;
                        }else if (w < f1 + f2 + f3 + f4)
                        {
                            ActiveMove = MoveSet[3];
                            if (ActiveMove.PerformInWorld(Main.projectile[ActivePetId],
                                (ParentPokemon)Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this))
                                Cooldown = ActiveMove.Cooldown;
                            else
                                ActiveMove = null;
                        }

                    }
                    if (mod.FirstPKMAbility.JustPressed && MoveSet[0] != null && ActiveMove == null)
                    {
                        ActiveMove = MoveSet[0];
                        if (ActiveMove.PerformInWorld(Main.projectile[ActivePetId],
                            (ParentPokemon) Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this))
                            Cooldown = ActiveMove.Cooldown;
                        else
                            ActiveMove = null;
                    }
                    else if (mod.SecondPKMAbility.JustPressed && MoveSet[1] != null && ActiveMove == null)
                    {
                        ActiveMove = MoveSet[1];
                        if (ActiveMove.PerformInWorld(Main.projectile[ActivePetId],
                            (ParentPokemon) Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this))
                            Cooldown = ActiveMove.Cooldown;
                        else
                            ActiveMove = null;
                    }
                    else if (mod.ThirdPKMAbility.JustPressed && MoveSet[2] != null && ActiveMove == null)
                    {
                        ActiveMove = MoveSet[2];
                        if (ActiveMove.PerformInWorld(Main.projectile[ActivePetId],
                            (ParentPokemon) Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this))
                            Cooldown = ActiveMove.Cooldown;
                        else
                            ActiveMove = null;
                    }
                    else if (mod.FourthPKMAbility.JustPressed && MoveSet[3] != null && ActiveMove == null)
                    {
                        ActiveMove = MoveSet[3];
                        if (ActiveMove.PerformInWorld(Main.projectile[ActivePetId],
                            (ParentPokemon) Main.projectile[ActivePetId].modProjectile, Main.MouseWorld, this))
                            Cooldown = ActiveMove.Cooldown;
                        else
                            ActiveMove = null;
                    }
                }
            }
        }

        public BaseMove ActiveMove;

        public override void UpdateAutopause()
        {
            base.UpdateAutopause();
            if (StarterChosen)
            {
                if (Main.playerInventory)
                {
                    if (player.chest != -1 || Main.npcShop != 0 || EvolveUI.Visible)
                        PartySlots.Visible = false;
                    else
                        PartySlots.Visible = true;
                    UISidebar.Visible = false;
                }
                else
                {
                    EvolveUI.Visible = false;
                    UISidebar.Visible = true;
                    PartySlots.Visible = false;
                }
            }

            if (ChooseStarter.Visible || ChooseStarterBulbasaur.Visible || ChooseStarterCharmander.Visible ||
                ChooseStarterSquirtle.Visible) ClearNPCs();
        }

        public static void ClearNPCs()
        {
            for (int i = 0; i < Main.npc.Length; i++)
                if (Main.npc[i] != null && !Main.npc[i].townNPC)
                {
                    Main.npc[i].life = 0;
                    if (Main.netMode == 2) NetMessage.SendData(23, -1, -1, null, i);
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
            if (vendor.type == NPCType<PokemonTrainer>() && item.type == ItemType<PokeballItem>())
            {
                TerramonPlayer p = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
                p.premierBallRewardCounter++;
                if (p.premierBallRewardCounter == 10)
                {
                    p.premierBallRewardCounter = 0;
                    player.QuickSpawnItem(ItemType<PremierBallItem>());
                }
            }
        }


        public override TagCompound Save()
        {
            PartySlots partySlots = GetInstance<TerramonMod>().PartySlots;
            if (partySlots.partyslot1.Item.IsAir)
            {
                firstslotname = "*";
                GetInstance<TerramonMod>().UISidebar.firstpkmn.SetImage(GetTexture("Terraria/Item_0"));
                GetInstance<TerramonMod>().UISidebar.firstpkmn.HoverText = "";
                GetInstance<TerramonMod>().UISidebar.firstpkmn.Recalculate();
            }

            if (partySlots.partyslot2.Item.IsAir)
            {
                secondslotname = "*";
                GetInstance<TerramonMod>().UISidebar.secondpkmn.SetImage(GetTexture("Terraria/Item_0"));
                GetInstance<TerramonMod>().UISidebar.secondpkmn.HoverText = "";
                GetInstance<TerramonMod>().UISidebar.secondpkmn.Recalculate();
            }

            if (partySlots.partyslot3.Item.IsAir)
            {
                thirdslotname = "*";
                GetInstance<TerramonMod>().UISidebar.thirdpkmn.SetImage(GetTexture("Terraria/Item_0"));
                GetInstance<TerramonMod>().UISidebar.thirdpkmn.HoverText = "";
                GetInstance<TerramonMod>().UISidebar.thirdpkmn.Recalculate();
            }

            if (partySlots.partyslot4.Item.IsAir)
            {
                fourthslotname = "*";
                GetInstance<TerramonMod>().UISidebar.fourthpkmn.SetImage(GetTexture("Terraria/Item_0"));
                GetInstance<TerramonMod>().UISidebar.fourthpkmn.HoverText = "";
                GetInstance<TerramonMod>().UISidebar.fourthpkmn.Recalculate();
            }

            if (partySlots.partyslot5.Item.IsAir)
            {
                fifthslotname = "*";
                GetInstance<TerramonMod>().UISidebar.fifthpkmn.SetImage(GetTexture("Terraria/Item_0"));
                GetInstance<TerramonMod>().UISidebar.fifthpkmn.HoverText = "";
                GetInstance<TerramonMod>().UISidebar.fifthpkmn.Recalculate();
            }

            if (partySlots.partyslot6.Item.IsAir)
            {
                sixthslotname = "*";
                GetInstance<TerramonMod>().UISidebar.sixthpkmn.SetImage(GetTexture("Terraria/Item_0"));
                GetInstance<TerramonMod>().UISidebar.sixthpkmn.HoverText = "";
                GetInstance<TerramonMod>().UISidebar.sixthpkmn.Recalculate();
            }

            TagCompound tag = new TagCompound
            {
                [nameof(StarterChosen)] = StarterChosen
            };

            if (PartySlot1 != null && PartySlot1.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
                tag.Add(nameof(PartySlot1), PartySlot1);
            if (PartySlot2 != null && PartySlot2.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
                tag.Add(nameof(PartySlot2), PartySlot2);
            if (PartySlot3 != null && PartySlot3.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
                tag.Add(nameof(PartySlot3), PartySlot3);
            if (PartySlot4 != null && PartySlot4.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
                tag.Add(nameof(PartySlot4), PartySlot4);
            if (PartySlot5 != null && PartySlot5.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
                tag.Add(nameof(PartySlot5), PartySlot5);
            if (PartySlot6 != null && PartySlot6.GetByte(BaseCaughtClass.POKEBAL_PROPERTY) != 0)
                tag.Add(nameof(PartySlot6), PartySlot6);


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


        //public bool StarterPackageBought { get; } //Unused

        public int whoAmI => player.whoAmI;
    }
}