using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Razorwing.Framework.Localisation;
using Terramon.Items.Pokeballs;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Players;
using Terramon.Pokemon;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terramon.UI.SidebarParty
{
    // ExampleUIs visibility is toggled by typing "/coin" in chat. (See CoinCommand.cs)
    // ExampleUI is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
    internal class UISidebar : UIState
    {
        public SidebarPanel mainPanel;
        public static bool Visible;
        public bool lightmode = true;

        public ILocalisedBindableString sendOutText = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("sidebar.sendOut", "Left click to send out!")));
        public ILocalisedBindableString goText = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("go", "Go {0}!")));
        public ILocalisedBindableString retire1Text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("retire1", "{0}, switch out!\nCome back!")));
        public ILocalisedBindableString retire2Text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("retire2", "{0}, return!")));
        public ILocalisedBindableString retire3Text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("retire3", "That's enough for now, {0}!")));
        public ILocalisedBindableString pokemonName1 = TerramonMod.Localisation.GetLocalisedString("*");
        public ILocalisedBindableString pokemonName2 = TerramonMod.Localisation.GetLocalisedString("*");
        public ILocalisedBindableString pokemonName3 = TerramonMod.Localisation.GetLocalisedString("*");
        public ILocalisedBindableString pokemonName4 = TerramonMod.Localisation.GetLocalisedString("*");
        public ILocalisedBindableString pokemonName5 = TerramonMod.Localisation.GetLocalisedString("*");
        public ILocalisedBindableString pokemonName6 = TerramonMod.Localisation.GetLocalisedString("*");
        public ILocalisedBindableString helpText = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("sidebar.help", "Terramon Help")));
        public ILocalisedBindableString help1Text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("sidebar.help1", $"(1/3) Welcome to Terramon {TerramonMod.Instance.Version}, where you can discover and catch Pokémon in Terraria! Keep pressing this button for more tips and tricks.")));
        public ILocalisedBindableString help2Text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("sidebar.help2", "(2/3) For support, join the official Discord server using the [c/f7e34d:/discord] command. Or, access our wiki with the [c/f7e34d:/wiki] command.")));
        public ILocalisedBindableString help3Text = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("sidebar.help3", "(3/3) Also, feel free to customize your experience with the Mod Config in [c/ff8f33:Settings > Mod Configuration] or from the Mods menu.")));


        public string p1 = "*", p2 = "*", p3 = "*", p4 = "*", p5 = "*", p6 = "*";


        public UIOpaqueButton choose;
        public UIOpaqueButton battle;

        //sidebar pkmn textures
        public Texture2D firstpkmntexture;
        public SidebarClass firstpkmn;

        public Texture2D secondpkmntexture;
        public SidebarClass secondpkmn;

        public Texture2D thirdpkmntexture;
        public SidebarClass thirdpkmn;

        public Texture2D fourthpkmntexture;
        public SidebarClass fourthpkmn;

        public Texture2D fifthpkmntexture;
        public SidebarClass fifthpkmn;

        public Texture2D sixthpkmntexture;
        public SidebarClass sixthpkmn;


        public int CycleIndex;
        public int HelpListCycler;

        // In OnInitialize, we place various UIElements onto our UIState (this class).
        // UIState classes have width and height equal to the full screen, because of this, usually we first define a UIElement that will act as the container for our UI.
        // We then place various other UIElement onto that container UIElement positioned relative to the container UIElement.
        public override void OnInitialize()
        {
            //pokemon icons

            // Next, we create another UIElement that we will place. Since we will be calling `mainPanel.Append(playButton);`, Left and Top are relative to the top left of the mainPanel UIElement. 
            // By properly nesting UIElements, we can position things relatively to each other easily.

            mainPanel = new SidebarPanel();
            mainPanel.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            mainPanel.HAlign = 0f - 0.01f;
            mainPanel.VAlign = 0.6f;
            mainPanel.Width.Set(95, 0f);
            mainPanel.Height.Set(385f, 0f);
            mainPanel.BackgroundColor = new Color(15, 20, 46) * 0.65f;

            Texture2D chooseTexture = ModContent.GetTexture("Terramon/UI/SidebarParty/Help");
            choose = new UIOpaqueButton(chooseTexture, helpText.Value);
            choose.HAlign = 0.007f; // 1
            choose.VAlign = 0.98f; // 1
            choose.Width.Set(20, 0);
            choose.Height.Set(32, 0);
            choose.OnClick += HelpClicked;
            Append(choose);

            chooseTexture = ModContent.GetTexture("Terramon/UI/SidebarParty/PremierBallSidebar");
            battle = new UIOpaqueButton(chooseTexture, "[PH] Start Battle");
            battle.HAlign = 0.007f; // 1
            battle.VAlign = 0.9f; // 1
            battle.Width.Set(20, 0);
            battle.Height.Set(32, 0);
            battle.OnClick += (e, x) =>
            {
                var player = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
                if (player.ActivePet.Fainted)
                {
                    Main.NewText($"Your {player.ActivePet.Pokemon} is fainted and can't fight!");
                    return;
                }

                var id = BaseMove.GetNearestPokemon(Main.LocalPlayer.position);
                if(id != null)
                    player.Battle = new BattleMode(player, BattleState.BattleWithWild, npc: (ParentPokemonNPC)id.modNPC, second: new PokemonData()
                    {
                        Pokemon = ((ParentPokemonNPC)id.modNPC).HomeClass().Name,
                    });
            };
            Append(battle);

            firstpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            firstpkmn = new SidebarClass(firstpkmntexture, "");
            firstpkmn.HAlign = 0.6f; // 1
            firstpkmn.VAlign = 0.08888f; // 1
            firstpkmn.Width.Set(40, 0);
            firstpkmn.Height.Set(40, 0);
            firstpkmn.OnClick += SpawnPKMN1;
            mainPanel.Append(firstpkmn);

            secondpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            secondpkmn = new SidebarClass(secondpkmntexture, "");
            secondpkmn.HAlign = 0.6f; // 1
            secondpkmn.VAlign = 0.25555f; // 1
            secondpkmn.Width.Set(40, 0);
            secondpkmn.Height.Set(40, 0);
            secondpkmn.OnClick += SpawnPKMN2;
            mainPanel.Append(secondpkmn);

            thirdpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            thirdpkmn = new SidebarClass(thirdpkmntexture, "");
            thirdpkmn.HAlign = 0.6f; // 1
            thirdpkmn.VAlign = 0.41111f; // 1
            thirdpkmn.Width.Set(40, 0);
            thirdpkmn.Height.Set(40, 0);
            thirdpkmn.OnClick += SpawnPKMN3;
            mainPanel.Append(thirdpkmn);

            fourthpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            fourthpkmn = new SidebarClass(fourthpkmntexture, "");
            fourthpkmn.HAlign = 0.6f; // 1
            fourthpkmn.VAlign = 0.58888f; // 1
            fourthpkmn.Width.Set(40, 0);
            fourthpkmn.Height.Set(40, 0);
            fourthpkmn.OnClick += SpawnPKMN4;
            mainPanel.Append(fourthpkmn);

            fifthpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            fifthpkmn = new SidebarClass(fifthpkmntexture, "");
            fifthpkmn.HAlign = 0.6f; // 1
            fifthpkmn.VAlign = 0.75555f; // 1
            fifthpkmn.Width.Set(40, 0);
            fifthpkmn.Height.Set(40, 0);
            fifthpkmn.OnClick += SpawnPKMN5;
            mainPanel.Append(fifthpkmn);

            sixthpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            sixthpkmn = new SidebarClass(sixthpkmntexture, "");
            sixthpkmn.HAlign = 0.6f; // 1
            sixthpkmn.VAlign = 0.91111f; // 1
            sixthpkmn.Width.Set(40, 0);
            sixthpkmn.Height.Set(40, 0);
            sixthpkmn.OnClick += SpawnPKMN6;
            mainPanel.Append(sixthpkmn);

            Append(mainPanel);
            // As a recap, ExampleUI is a UIState, meaning it covers the whole screen. We attach mainPanel to ExampleUI some distance from the top left corner.
            // We then place playButton, closeButton, and moneyDiplay onto mainPanel so we can easily place these UIElements relative to mainPanel.
            // Since mainPanel will move, this proper organization will move playButton, closeButton, and moneyDiplay properly when mainPanel moves.
        }

        public override void Update(GameTime gameTime)
        {
            // Don't delete this or the UIElements attached to this UIState will cease to function.
            base.Update(gameTime);
            if (TerramonMod.PartyUIAutoMode == false || TerramonMod.PartyUIReverseAutoMode == false)
            {
                if (TerramonMod.PartyUITheme == false)
                    mainPanel.BackgroundColor = new Color(255, 250, 250) * 0.5f;
                else
                {
                    mainPanel.BackgroundColor = new Color(44, 61, 158) * 0.5f;
                }
            }

            if (TerramonMod.PartyUIAutoMode)
            {
                if (!Main.dayTime)
                {
                    mainPanel.BackgroundColor = new Color(44, 61, 158) * 0.5f;
                }
                else
                    mainPanel.BackgroundColor = new Color(255, 250, 250) * 0.5f;
            }
            else if (TerramonMod.PartyUIReverseAutoMode)
            {
                if (!Main.dayTime)
                    mainPanel.BackgroundColor = new Color(255, 250, 250) * 0.5f;
                else
                {
                    mainPanel.BackgroundColor = new Color(44, 61, 158) * 0.5f;
                }
            }

            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            var slotName = modPlayer.firstslotname;
            var slotTag = modPlayer.PartySlot1;

            if (slotName != "*")
            {
                if (p1 != slotName)
                {
                    p1 = slotName;
                    pokemonName1 =
                        TerramonMod.Localisation.GetLocalisedString(new LocalisedString(slotName));
                }

                updateSlot(slotTag, firstpkmn, pokemonName1.Value);//move copypaste to method, so we can modify display data from one place
            }

            slotName = modPlayer.secondslotname;
            slotTag = modPlayer.PartySlot2;

            if (slotName != "*")
            {
                if (p2 != slotName)
                {
                    p2 = slotName;
                    pokemonName2 =
                        TerramonMod.Localisation.GetLocalisedString(new LocalisedString(slotName));
                }

                updateSlot(slotTag, secondpkmn, pokemonName2.Value);
            }

            slotName = modPlayer.thirdslotname;
            slotTag = modPlayer.PartySlot3;

            if (slotName != "*")
            {
                if (p3 != slotName)
                {
                    p3 = slotName;
                    pokemonName3 =
                        TerramonMod.Localisation.GetLocalisedString(new LocalisedString(slotName));
                }

                updateSlot(slotTag, thirdpkmn, pokemonName3.Value);
            }

            slotName = modPlayer.fourthslotname;
            slotTag = modPlayer.PartySlot4;

            if (slotName != "*")
            {
                if (p4 != slotName)
                {
                    p4 = slotName;
                    pokemonName4 =
                        TerramonMod.Localisation.GetLocalisedString(new LocalisedString(slotName));
                }

                updateSlot(slotTag, fourthpkmn, pokemonName4.Value);
            }

            slotName = modPlayer.fifthslotname;
            slotTag = modPlayer.PartySlot5;

            if (slotName != "*")
            {
                if (p5 != slotName)
                {
                    p5 = slotName;
                    pokemonName5 =
                        TerramonMod.Localisation.GetLocalisedString(new LocalisedString(slotName));
                }

                updateSlot(slotTag, fifthpkmn, pokemonName5.Value);

            }

            slotName = modPlayer.sixthslotname;
            slotTag = modPlayer.PartySlot6;

            if (slotName != "*")
            {
                if (p6 != slotName)
                {
                    p6 = slotName;
                    pokemonName6 =
                        TerramonMod.Localisation.GetLocalisedString(new LocalisedString(slotName));
                }

                updateSlot(slotTag, sixthpkmn, pokemonName6.Value);
            }
        }

        private void updateSlot(PokemonData slot, SidebarClass side, string name)
        {
            side.SetImage(
                    ModContent.GetTexture("Terramon/Minisprites/Regular/SidebarSprites/" + slot.Pokemon));
            side.HoverText = name + $"[i:{ModContent.ItemType<SidebarPKBALL>()}]" +
                                      $"\nLVL: {slot.Level}" +
                                      $"\nEXP: {slot.Exp}" +
                                      $"\nHP: {slot.HP}/{slot.MaxHP}" +
                                      $"\n{sendOutText.Value}";
            side.Recalculate();
        }

        private void HelpClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            HelpListCycler++;
            if (HelpListCycler == 1)
            {
                Main.NewText(help1Text.Value);
            }
            if (HelpListCycler == 2)
            {
                Main.NewText(help2Text.Value);
            }
            if (HelpListCycler == 3)
            {
                Main.NewText(help3Text.Value);
                HelpListCycler = 0;
            }
        }

        private void SpawnPKMN1(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.firstslotname == "*")
                return;

            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            var pet = modPlayer.PartySlot1.Pokemon;
            if (modPlayer.ActivePetName != pet)
            {
                if (!string.IsNullOrEmpty(modPlayer.ActivePetName) && modPlayer.ActivePetName != "*")
                    PrintSwitch(player, modPlayer);
                if (!player.HasBuff(pokeBuff)) player.AddBuff(pokeBuff, 2);
                modPlayer.ActivePetName = pet;
                goText.Args = new object[] {pokemonName1.Value};
                CombatText.NewText(player.Hitbox, Color.White, goText.Value, true);
                Main.PlaySound(ModContent.GetInstance<TerramonMod>()
                    .GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
                modPlayer.ActivePartySlot = 1;
            }
            else
            {
                player.ClearBuff(pokeBuff);
                PrintSwitch(player, modPlayer);
                modPlayer.ActivePetName = string.Empty;
                modPlayer.ActivePartySlot = -1;
            }
        }

        private void SpawnPKMN2(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.secondslotname == "*")
                return;

            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            var pet = modPlayer.PartySlot2.Pokemon;
            if (modPlayer.ActivePetName != pet)
            {
                if (!string.IsNullOrEmpty(modPlayer.ActivePetName) && modPlayer.ActivePetName != "*")
                    PrintSwitch(player, modPlayer);
                if (!player.HasBuff(pokeBuff)) player.AddBuff(pokeBuff, 2);
                modPlayer.ActivePetName = pet;
                goText.Args = new object[] { pokemonName2.Value };
                CombatText.NewText(player.Hitbox, Color.White, goText.Value, true);
                Main.PlaySound(ModContent.GetInstance<TerramonMod>()
                    .GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
                modPlayer.ActivePartySlot = 2;
            }
            else
            {
                player.ClearBuff(pokeBuff);
                PrintSwitch(player, modPlayer);
                modPlayer.ActivePetName = string.Empty;
                modPlayer.ActivePartySlot = -1;
            }
        }

        private void SpawnPKMN3(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.thirdslotname == "*")
                return;

            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            var pet = modPlayer.PartySlot3.Pokemon;
            if (modPlayer.ActivePetName != pet)
            {
                if (!string.IsNullOrEmpty(modPlayer.ActivePetName) && modPlayer.ActivePetName != "*")
                    PrintSwitch(player, modPlayer);
                if (!player.HasBuff(pokeBuff)) player.AddBuff(pokeBuff, 2);
                modPlayer.ActivePetName = pet;
                goText.Args = new object[] { pokemonName3.Value };
                CombatText.NewText(player.Hitbox, Color.White, goText.Value, true);
                Main.PlaySound(ModContent.GetInstance<TerramonMod>()
                    .GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
                modPlayer.ActivePartySlot = 3;
            }
            else
            {
                player.ClearBuff(pokeBuff);
                PrintSwitch(player, modPlayer);
                modPlayer.ActivePetName = string.Empty;
                modPlayer.ActivePartySlot = -1;
            }
        }

        private void SpawnPKMN4(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.fourthslotname == "*")
                return;

            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            var pet = modPlayer.PartySlot4.Pokemon;
            if (modPlayer.ActivePetName != pet)
            {
                if (!string.IsNullOrEmpty(modPlayer.ActivePetName) && modPlayer.ActivePetName != "*")
                    PrintSwitch(player, modPlayer);
                if (!player.HasBuff(pokeBuff)) player.AddBuff(pokeBuff, 2);
                modPlayer.ActivePetName = pet;
                goText.Args = new object[] { pokemonName4.Value };
                CombatText.NewText(player.Hitbox, Color.White, goText.Value, true);
                Main.PlaySound(ModContent.GetInstance<TerramonMod>()
                    .GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
                modPlayer.ActivePartySlot = 4;
            }
            else
            {
                player.ClearBuff(pokeBuff);
                PrintSwitch(player, modPlayer);
                modPlayer.ActivePetName = string.Empty;
                modPlayer.ActivePartySlot = -1;
            }
        }

        private void SpawnPKMN5(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.fifthslotname == "*")
                return;

            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            var pet = modPlayer.PartySlot5.Pokemon;
            if (modPlayer.ActivePetName != pet)
            {
                if (!string.IsNullOrEmpty(modPlayer.ActivePetName) && modPlayer.ActivePetName != "*")
                    PrintSwitch(player, modPlayer);
                if (!player.HasBuff(pokeBuff)) player.AddBuff(pokeBuff, 2);
                modPlayer.ActivePetName = pet;
                goText.Args = new object[] { pokemonName5.Value };
                CombatText.NewText(player.Hitbox, Color.White, goText.Value, true);
                Main.PlaySound(ModContent.GetInstance<TerramonMod>()
                    .GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
                modPlayer.ActivePartySlot = 5;
            }
            else
            {
                player.ClearBuff(pokeBuff);
                PrintSwitch(player, modPlayer);
                modPlayer.ActivePetName = string.Empty;
                modPlayer.ActivePartySlot = -1;
            }
        }

        private void SpawnPKMN6(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.sixthslotname == "*")
                return;

            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            var pet = modPlayer.PartySlot6.Pokemon;
            if (modPlayer.ActivePetName != pet)
            {
                if (!string.IsNullOrEmpty(modPlayer.ActivePetName) && modPlayer.ActivePetName != "*")
                    PrintSwitch(player, modPlayer);
                if (!player.HasBuff(pokeBuff)) player.AddBuff(pokeBuff, 2);
                modPlayer.ActivePetName = pet;
                goText.Args = new object[] { pokemonName6.Value };
                CombatText.NewText(player.Hitbox, Color.White, goText.Value, true);
                Main.PlaySound(ModContent.GetInstance<TerramonMod>()
                    .GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
                modPlayer.ActivePartySlot = 6;
            }
            else
            {
                player.ClearBuff(pokeBuff);
                PrintSwitch(player, modPlayer);
                modPlayer.ActivePetName = string.Empty;
                modPlayer.ActivePartySlot = -1;
            }
        }

        protected void PrintSwitch(Player player, TerramonPlayer modPlayer)
        {
            var rect = new Rectangle(player.Hitbox.X, player.Hitbox.Y, player.Hitbox.Width, player.Hitbox.Height);
            rect.Y -= 35;
            var pet = "*";
            switch (modPlayer.ActivePartySlot)
            {
                case 1:
                    pet = pokemonName1.Value;
                    break;
                case 2:
                    pet = pokemonName2.Value;
                    break;
                case 3:
                    pet = pokemonName3.Value;
                    break;
                case 4:
                    pet = pokemonName4.Value;
                    break;
                case 5:
                    pet = pokemonName5.Value;
                    break;
                case 6:
                    pet = pokemonName6.Value;
                    break;
            }
            switch (Main.rand.Next(3))
            {
                case 0:
                    retire1Text.Args = new object[]{pet};
                    CombatText.NewText(rect, Color.White, retire1Text.Value, true);
                    break;
                case 1:
                    retire2Text.Args = new object[] { pet };
                    CombatText.NewText(rect, Color.White, retire2Text.Value, true);
                    break;
                default:
                    retire3Text.Args = new object[] { pet };
                    CombatText.NewText(rect, Color.White, retire3Text.Value, true);
                    break;
            }
        }
    }
}