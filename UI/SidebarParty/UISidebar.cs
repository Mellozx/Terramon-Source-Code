using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Players;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
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

        public UIOpaqueButton choose;

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

        // In OnInitialize, we place various UIElements onto our UIState (this class).
        // UIState classes have width and height equal to the full screen, because of this, usually we first define a UIElement that will act as the container for our UI.
        // We then place various other UIElement onto that container UIElement positioned relative to the container UIElement.
        public override void OnInitialize()
        {
            // Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
            // Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.


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
            mainPanel.BackgroundColor = new Color(15, 20, 46) * 0.5f;

            Texture2D chooseTexture = ModContent.GetTexture("Terramon/UI/SidebarParty/Help");
            choose = new UIOpaqueButton(chooseTexture, "Show Terramon Help");
            choose.HAlign = 0.007f; // 1
            choose.VAlign = 0.98f; // 1
            choose.Width.Set(20, 0);
            choose.Height.Set(32, 0);
            choose.OnClick += new MouseEvent(HelpClicked);
            Append(choose);

            firstpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            firstpkmn = new SidebarClass(firstpkmntexture, "");
            firstpkmn.test1 = firstpkmntexture;
            firstpkmn.HAlign = 0.6f; // 1
            firstpkmn.VAlign = 0.08888f; // 1
            firstpkmn.Width.Set(40, 0);
            firstpkmn.Height.Set(40, 0);
            firstpkmn.OnClick += new MouseEvent(SpawnPKMN1);
            mainPanel.Append(firstpkmn);

            secondpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            secondpkmn = new SidebarClass(secondpkmntexture, "");
            secondpkmn.test2 = secondpkmntexture;
            secondpkmn.HAlign = 0.6f; // 1
            secondpkmn.VAlign = 0.25555f; // 1
            secondpkmn.Width.Set(40, 0);
            secondpkmn.Height.Set(40, 0);
            secondpkmn.OnClick += new MouseEvent(SpawnPKMN2);
            mainPanel.Append(secondpkmn);

            thirdpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            thirdpkmn = new SidebarClass(thirdpkmntexture, "");
            thirdpkmn.test3 = thirdpkmntexture;
            thirdpkmn.HAlign = 0.6f; // 1
            thirdpkmn.VAlign = 0.41111f; // 1
            thirdpkmn.Width.Set(40, 0);
            thirdpkmn.Height.Set(40, 0);
            thirdpkmn.OnClick += new MouseEvent(SpawnPKMN3);
            mainPanel.Append(thirdpkmn);

            fourthpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            fourthpkmn = new SidebarClass(fourthpkmntexture, "");
            fourthpkmn.test4 = fourthpkmntexture;
            fourthpkmn.HAlign = 0.6f; // 1
            fourthpkmn.VAlign = 0.58888f; // 1
            fourthpkmn.Width.Set(40, 0);
            fourthpkmn.Height.Set(40, 0);
            fourthpkmn.OnClick += new MouseEvent(SpawnPKMN4);
            mainPanel.Append(fourthpkmn);

            fifthpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            fifthpkmn = new SidebarClass(fifthpkmntexture, "");
            fifthpkmn.test5 = fifthpkmntexture;
            fifthpkmn.HAlign = 0.6f; // 1
            fifthpkmn.VAlign = 0.75555f; // 1
            fifthpkmn.Width.Set(40, 0);
            fifthpkmn.Height.Set(40, 0);
            fifthpkmn.OnClick += new MouseEvent(SpawnPKMN5);
            mainPanel.Append(fifthpkmn);

            sixthpkmntexture = ModContent.GetTexture("Terraria/Item_0");
            sixthpkmn = new SidebarClass(sixthpkmntexture, "");
            sixthpkmn.test6 = sixthpkmntexture;
            sixthpkmn.HAlign = 0.6f; // 1
            sixthpkmn.VAlign = 0.91111f; // 1
            sixthpkmn.Width.Set(40, 0);
            sixthpkmn.Height.Set(40, 0);
            sixthpkmn.OnClick += new MouseEvent(SpawnPKMN6);
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
                {
                    mainPanel.BackgroundColor = new Color(255, 250, 250) * 0.5f;
                }
                else
                {
                    mainPanel.BackgroundColor = new Color(15, 20, 46) * 0.5f;
                }
            }
            if (TerramonMod.PartyUIAutoMode == true)
            {
                if (!Main.dayTime)
                {
                    mainPanel.BackgroundColor = new Color(15, 20, 46) * 0.5f;
                }
                else
                {
                    mainPanel.BackgroundColor = new Color(255, 250, 250) * 0.5f;
                }
            }
            else if (TerramonMod.PartyUIReverseAutoMode == true)
            {
                if (!Main.dayTime)
                {
                    mainPanel.BackgroundColor = new Color(255, 250, 250) * 0.5f;
                }
                else
                {
                    mainPanel.BackgroundColor = new Color(15, 20, 46) * 0.5f;
                }
            }

            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.firstslotname != "*")
            {
                firstpkmn.SetImage(ModContent.GetTexture("Terramon/Minisprites/Regular/SidebarSprites/" + modPlayer.firstslotname));
                firstpkmn.test1 = firstpkmntexture;
                firstpkmn.HoverText = modPlayer.firstslotname + $"[i:{ModContent.ItemType<Items.Pokeballs.SidebarPKBALL>()}]" +
                                      $"\nLVL: {modPlayer.PartySlot1.GetInt(nameof(BaseCaughtClass.Level))}" +
                                      $"\nEXP: {modPlayer.PartySlot1.GetInt(nameof(BaseCaughtClass.Exp))}" +
                                      $"\nLeft click to send out!";
                firstpkmn.Recalculate();
            }
            if (modPlayer.secondslotname != "*")
            {
                secondpkmn.SetImage(ModContent.GetTexture("Terramon/Minisprites/Regular/SidebarSprites/" + modPlayer.secondslotname));
                secondpkmn.test2 = secondpkmntexture;
                secondpkmn.HoverText = modPlayer.secondslotname + $"[i:{ModContent.ItemType<Items.Pokeballs.SidebarPKBALL>()}]" +
                                       $"\nLVL: {modPlayer.PartySlot2.GetInt(nameof(BaseCaughtClass.Level))}" +
                                       $"\nEXP: {modPlayer.PartySlot2.GetInt(nameof(BaseCaughtClass.Exp))}" +
                                       $"\nLeft click to send out!";
                secondpkmn.Recalculate();
            }
            if (modPlayer.thirdslotname != "*")
            {
                thirdpkmn.SetImage(ModContent.GetTexture("Terramon/Minisprites/Regular/SidebarSprites/" + modPlayer.thirdslotname));
                thirdpkmn.test3 = thirdpkmntexture;
                thirdpkmn.HoverText = modPlayer.thirdslotname + $"[i:{ModContent.ItemType<Items.Pokeballs.SidebarPKBALL>()}]" +
                                      $"\nLVL: {modPlayer.PartySlot3.GetInt(nameof(BaseCaughtClass.Level))}" +
                                      $"\nEXP: {modPlayer.PartySlot3.GetInt(nameof(BaseCaughtClass.Exp))}" +
                                      $"\nLeft click to send out!";
                thirdpkmn.Recalculate();
            }
            if (modPlayer.fourthslotname != "*")
            {
                fourthpkmn.SetImage(ModContent.GetTexture("Terramon/Minisprites/Regular/SidebarSprites/" + modPlayer.fourthslotname));
                fourthpkmn.test4 = fourthpkmntexture;
                fourthpkmn.HoverText = modPlayer.fourthslotname + $"[i:{ModContent.ItemType<Items.Pokeballs.SidebarPKBALL>()}]" +
                                       $"\nLVL: {modPlayer.PartySlot4.GetInt(nameof(BaseCaughtClass.Level))}" +
                                       $"\nEXP: {modPlayer.PartySlot4.GetInt(nameof(BaseCaughtClass.Exp))}" +
                                       $"\nLeft click to send out!";
                fourthpkmn.Recalculate();
            }
            if (modPlayer.fifthslotname != "*")
            {
                fifthpkmn.SetImage(ModContent.GetTexture("Terramon/Minisprites/Regular/SidebarSprites/" + modPlayer.fifthslotname));
                fifthpkmn.test5 = fifthpkmntexture;
                fifthpkmn.HoverText = modPlayer.fifthslotname + $"[i:{ModContent.ItemType<Items.Pokeballs.SidebarPKBALL>()}]" +
                                      $"\nLVL: {modPlayer.PartySlot5.GetInt(nameof(BaseCaughtClass.Level))}" +
                                      $"\nEXP: {modPlayer.PartySlot5.GetInt(nameof(BaseCaughtClass.Exp))}" +
                                      $"\nLeft click to send out!";
                fifthpkmn.Recalculate();
            }
            if (modPlayer.sixthslotname != "*")
            {
                sixthpkmn.SetImage(ModContent.GetTexture("Terramon/Minisprites/Regular/SidebarSprites/" + modPlayer.sixthslotname));
                sixthpkmn.test6 = sixthpkmntexture;
                sixthpkmn.HoverText = modPlayer.sixthslotname + $"[i:{ModContent.ItemType<Items.Pokeballs.SidebarPKBALL>()}]" +
                                      $"\nLVL: {modPlayer.PartySlot6.GetInt(nameof(BaseCaughtClass.Level))}" +
                                      $"\nEXP: {modPlayer.PartySlot6.GetInt(nameof(BaseCaughtClass.Exp))}" +
                                      $"\nLeft click to send out!";
                sixthpkmn.Recalculate();
            }
        }

        private void HelpClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.NewText("Welcome to Terramon v0.1.2, where you can discover and catch Pokémon in Terraria! For support, join the official Discord server using the [c/f7e34d:/discord] command, or open up the online wiki with the [c/f7e34d:/wiki] command.");
            Main.NewText("Check out the Mod Config from [c/ff8f33:Settings > Mod Configuration] or from the Mods menu. You can customize various aspects of the mod there.");
        }
        private void Null(UIMouseEvent evt, UIElement listeningElement)
        {
            // doesnt do anything
        }

        private void SpawnPKMN1(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.firstslotname == "*")
                return;

            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            if (!player.HasBuff(pokeBuff))
            {
                player.AddBuff(pokeBuff, 2);
            }

            modPlayer.ActivePetName = modPlayer.PartySlot1.GetString(nameof(BaseCaughtClass.PokemonName));
            CombatText.NewText(player.Hitbox, Color.White, "Go! " + modPlayer.firstslotname + "!", true);
            Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
        }
        private void SpawnPKMN2(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.secondslotname == "*")
                return;
            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            if (!player.HasBuff(pokeBuff))
            {
                player.AddBuff(pokeBuff, 2);
            }

            modPlayer.ActivePetName = modPlayer.PartySlot2.GetString(nameof(BaseCaughtClass.PokemonName));
            CombatText.NewText(player.Hitbox, Color.White, "Go! " + modPlayer.secondslotname + "!", true);
            Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
        }
        private void SpawnPKMN3(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.thirdslotname == "*")
                return;
            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            if (!player.HasBuff(pokeBuff))
            {
                player.AddBuff(pokeBuff, 2);
            }

            modPlayer.ActivePetName = modPlayer.PartySlot3.GetString(nameof(BaseCaughtClass.PokemonName));
            CombatText.NewText(player.Hitbox, Color.White, "Go! " + modPlayer.thirdslotname + "!", true);
            Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
        }
        private void SpawnPKMN4(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.fourthslotname == "*")
                return;
            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            if (!player.HasBuff(pokeBuff))
            {
                player.AddBuff(pokeBuff, 2);
            }

            modPlayer.ActivePetName = modPlayer.PartySlot4.GetString(nameof(BaseCaughtClass.PokemonName));
            CombatText.NewText(player.Hitbox, Color.White, "Go! " + modPlayer.fourthslotname + "!", true);
            Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
        }
        private void SpawnPKMN5(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.fifthslotname == "*")
                return;
            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            if (!player.HasBuff(pokeBuff))
            {
                player.AddBuff(pokeBuff, 2);
            }

            modPlayer.ActivePetName = modPlayer.PartySlot5.GetString(nameof(BaseCaughtClass.PokemonName));
            CombatText.NewText(player.Hitbox, Color.White, "Go! " + modPlayer.fifthslotname + "!", true);
            Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
        }
        private void SpawnPKMN6(UIMouseEvent evt, UIElement listeningElement)
        {
            Player player = Main.LocalPlayer;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            if (modPlayer.sixthslotname == "*")
                return;
            var pokeBuff = ModContent.GetInstance<TerramonMod>().BuffType(nameof(PokemonBuff));
            if (!player.HasBuff(pokeBuff))
            {
                player.AddBuff(pokeBuff, 2);
            }

            modPlayer.ActivePetName = modPlayer.PartySlot6.GetString(nameof(BaseCaughtClass.PokemonName));
            CombatText.NewText(player.Hitbox, Color.White, "Go! " + modPlayer.sixthslotname + "!", true);
            Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/sendout"));
        }




    }
}
