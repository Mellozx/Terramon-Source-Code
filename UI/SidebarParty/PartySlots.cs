using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terramon.Players;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace Terramon.UI.SidebarParty
{
    // ExampleUIs visibility is toggled by typing "/coin" in chat. (See CoinCommand.cs)
    // ExampleUI is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
    public class PartySlots : UIState
    {
        private DragableUIPanel mainPanel;
        public static bool Visible;
        public bool lightmode = true;

        public Texture2D test;

        public VanillaItemSlotWrapper partyslot1;
        public VanillaItemSlotWrapper partyslot2;
        public VanillaItemSlotWrapper partyslot3;
        public VanillaItemSlotWrapper partyslot4;
        public VanillaItemSlotWrapper partyslot5;
        public VanillaItemSlotWrapper partyslot6;

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
            mainPanel = new DragableUIPanel();
            mainPanel.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            mainPanel.HAlign = 0.4f;
            mainPanel.VAlign = 0.65f;
            mainPanel.Width.Set(210, 0f);
            mainPanel.Height.Set(180f, 0f);

            partyslot1 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 1f);
            partyslot1.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot1.HAlign = 0.15f;
            partyslot1.VAlign = 0.25f;
            partyslot1.ValidItemFunc = item => item.IsAir || item.modItem is PokeballCaught || item.modItem is GreatBallCaught || item.modItem is UltraBallCaught || item.modItem is DuskBallCaught || item.modItem is PremierBallCaught;
            mainPanel.Append(partyslot1);

            partyslot2 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 1f);
            partyslot2.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot2.HAlign = 0.5f;
            partyslot2.VAlign = 0.25f;
            partyslot2.ValidItemFunc = item => item.IsAir || item.modItem is PokeballCaught || item.modItem is GreatBallCaught || item.modItem is UltraBallCaught || item.modItem is DuskBallCaught || item.modItem is PremierBallCaught;
            mainPanel.Append(partyslot2);

            partyslot3 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 1f);
            partyslot3.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot3.HAlign = 0.85f;
            partyslot3.VAlign = 0.25f;
            partyslot3.ValidItemFunc = item => item.IsAir || item.modItem is PokeballCaught || item.modItem is GreatBallCaught || item.modItem is UltraBallCaught || item.modItem is DuskBallCaught || item.modItem is PremierBallCaught;
            mainPanel.Append(partyslot3);

            partyslot4 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 1f);
            partyslot4.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot4.HAlign = 0.15f;
            partyslot4.VAlign = 0.85f;
            partyslot4.ValidItemFunc = item => item.IsAir || item.modItem is PokeballCaught || item.modItem is GreatBallCaught || item.modItem is UltraBallCaught || item.modItem is DuskBallCaught || item.modItem is PremierBallCaught;
            mainPanel.Append(partyslot4);

            partyslot5 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 1f);
            partyslot5.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot5.HAlign = 0.5f;
            partyslot5.VAlign = 0.85f;
            partyslot5.ValidItemFunc = item => item.IsAir || item.modItem is PokeballCaught || item.modItem is GreatBallCaught || item.modItem is UltraBallCaught || item.modItem is DuskBallCaught || item.modItem is PremierBallCaught;
            mainPanel.Append(partyslot5);

            partyslot6 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 1f);
            partyslot6.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot6.HAlign = 0.85f;
            partyslot6.VAlign = 0.85f;
            partyslot6.ValidItemFunc = item => item.IsAir || item.modItem is PokeballCaught || item.modItem is GreatBallCaught || item.modItem is UltraBallCaught || item.modItem is DuskBallCaught || item.modItem is PremierBallCaught;
            mainPanel.Append(partyslot6);

            Texture2D buttonDeleteTexture = ModContent.GetTexture("Terraria/UI/ButtonDelete");
            UIHoverImageButton closeButton = new UIHoverImageButton(buttonDeleteTexture, "Clear Party"); // Localized text for "Close"
            closeButton.Left.Set(7, 0f);
            closeButton.Top.Set(7, 0f);
            closeButton.Width.Set(30, 0f);
            closeButton.Height.Set(30, 0f);
            closeButton.OnClick += new MouseEvent(CloseButtonClicked);
            mainPanel.Append(closeButton);

            Texture2D buttonSaveTexture = ModContent.GetTexture("Terraria/UI/ButtonPlay");
            UIHoverImageButton SaveButton = new UIHoverImageButton(buttonSaveTexture, "Save Party"); // Localized text for "Close"
            SaveButton.Left.Set(33, 0f);
            SaveButton.Top.Set(7, 0f);
            SaveButton.Width.Set(30, 0f);
            SaveButton.Height.Set(30, 0f);
            SaveButton.OnClick += new MouseEvent(SaveButtonClicked);
            mainPanel.Append(SaveButton);

            Append(mainPanel);



            // As a recap, ExampleUI is a UIState, meaning it covers the whole screen. We attach mainPanel to ExampleUI some distance from the top left corner.
            // We then place playButton, closeButton, and moneyDiplay onto mainPanel so we can easily place these UIElements relative to mainPanel.
            // Since mainPanel will move, this proper organization will move playButton, closeButton, and moneyDiplay properly when mainPanel moves.
        }
        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuOpen);
            ModContent.GetInstance<TerramonMod>().UISidebar.CycleIndex = 0;
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            Mod mod = ModContent.GetInstance<TerramonMod>();
            Player player = Main.LocalPlayer;
            // clear buffs
            player.ClearBuff(mod.BuffType(modPlayer.firstslotname + "Buff"));
            player.ClearBuff(mod.BuffType(modPlayer.secondslotname + "Buff"));
            player.ClearBuff(mod.BuffType(modPlayer.thirdslotname + "Buff"));
            player.ClearBuff(mod.BuffType(modPlayer.fourthslotname + "Buff"));
            player.ClearBuff(mod.BuffType(modPlayer.fifthslotname + "Buff"));
            player.ClearBuff(mod.BuffType(modPlayer.sixthslotname + "Buff"));
            if (partyslot1.Item.IsAir)
            {
                modPlayer.firstslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.Recalculate();
            }
            if (partyslot2.Item.IsAir)
            {
                modPlayer.secondslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.Recalculate();
            }
            if (partyslot3.Item.IsAir)
            {
                modPlayer.thirdslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.Recalculate();
            }
            if (partyslot4.Item.IsAir)
            {
                modPlayer.fourthslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.Recalculate();
            }
            if (partyslot5.Item.IsAir)
            {
                modPlayer.fifthslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.Recalculate();
            }
            if (partyslot6.Item.IsAir)
            {
                modPlayer.sixthslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.Recalculate();
            }
            Main.NewText("Party Cleared.");
        }

        private void SaveButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuOpen);
            TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            ModContent.GetInstance<TerramonMod>().UISidebar.CycleIndex = 0;
            Mod mod = ModContent.GetInstance<TerramonMod>();
            Player player = Main.LocalPlayer;
            modPlayer.CatchIndex = 0;
            // clear buffs
            player.ClearBuff(mod.BuffType(modPlayer.firstslotname + "Buff"));
            player.ClearBuff(mod.BuffType(modPlayer.secondslotname + "Buff"));
            player.ClearBuff(mod.BuffType(modPlayer.thirdslotname + "Buff"));
            player.ClearBuff(mod.BuffType(modPlayer.fourthslotname + "Buff"));
            player.ClearBuff(mod.BuffType(modPlayer.fifthslotname + "Buff"));
            player.ClearBuff(mod.BuffType(modPlayer.sixthslotname + "Buff"));
            if (partyslot1.Item.IsAir)
            {
                modPlayer.firstslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.Recalculate();
            }
            if (partyslot2.Item.IsAir)
            {
                modPlayer.secondslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.Recalculate();
            }
            if (partyslot3.Item.IsAir)
            {
                modPlayer.thirdslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.Recalculate();
            }
            if (partyslot4.Item.IsAir)
            {
                modPlayer.fourthslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.Recalculate();
            }
            if (partyslot5.Item.IsAir)
            {
                modPlayer.fifthslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.Recalculate();
            }
            if (partyslot6.Item.IsAir)
            {
                modPlayer.sixthslotname = "*";
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.Recalculate();
            }

            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.IsAir)
            {
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.modItem is PokeballCaught)
                {
                    var pokeballCaught1 = (PokeballCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.modItem;
                    modPlayer.firstslottype = pokeballCaught1.PokemonNPC;
                    modPlayer.firstslotname = pokeballCaught1.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.modItem is GreatBallCaught)
                {
                    var greatBallCaught1 = (GreatBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.modItem;
                    modPlayer.firstslottype = greatBallCaught1.PokemonNPC;
                    modPlayer.firstslotname = greatBallCaught1.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.modItem is UltraBallCaught)
                {
                    var ultraBallCaught1 = (UltraBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.modItem;
                    modPlayer.firstslottype = ultraBallCaught1.PokemonNPC;
                    modPlayer.firstslotname = ultraBallCaught1.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.modItem is DuskBallCaught)
                {
                    var duskBallCaught1 = (DuskBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.modItem;
                    modPlayer.firstslottype = duskBallCaught1.PokemonNPC;
                    modPlayer.firstslotname = duskBallCaught1.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.modItem is PremierBallCaught)
                {
                    var premierBallCaught1 = (PremierBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.modItem;
                    modPlayer.firstslottype = premierBallCaught1.PokemonNPC;
                    modPlayer.firstslotname = premierBallCaught1.PokemonName;
                }
            }
            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.IsAir)
            {
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.modItem is PokeballCaught)
                {
                    var pokeballCaught2 = (PokeballCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.modItem;
                    modPlayer.secondslottype = pokeballCaught2.PokemonNPC;
                    modPlayer.secondslotname = pokeballCaught2.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.modItem is GreatBallCaught)
                {
                    var greatBallCaught2 = (GreatBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.modItem;
                    modPlayer.secondslottype = greatBallCaught2.PokemonNPC;
                    modPlayer.secondslotname = greatBallCaught2.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.modItem is UltraBallCaught)
                {
                    var ultraBallCaught2 = (UltraBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.modItem;
                    modPlayer.secondslottype = ultraBallCaught2.PokemonNPC;
                    modPlayer.secondslotname = ultraBallCaught2.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.modItem is DuskBallCaught)
                {
                    var duskBallCaught2 = (DuskBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.modItem;
                    modPlayer.secondslottype = duskBallCaught2.PokemonNPC;
                    modPlayer.secondslotname = duskBallCaught2.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.modItem is PremierBallCaught)
                {
                    var premierBallCaught2 = (PremierBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.modItem;
                    modPlayer.secondslottype = premierBallCaught2.PokemonNPC;
                    modPlayer.secondslotname = premierBallCaught2.PokemonName;
                }
            }
            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.IsAir)
            {
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.modItem is PokeballCaught)
                {
                    var pokeballCaught3 = (PokeballCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.modItem;
                    modPlayer.thirdslottype = pokeballCaught3.PokemonNPC;
                    modPlayer.thirdslotname = pokeballCaught3.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.modItem is GreatBallCaught)
                {
                    var greatBallCaught3 = (GreatBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.modItem;
                    modPlayer.thirdslottype = greatBallCaught3.PokemonNPC;
                    modPlayer.thirdslotname = greatBallCaught3.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.modItem is UltraBallCaught)
                {
                    var ultraBallCaught3 = (UltraBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.modItem;
                    modPlayer.thirdslottype = ultraBallCaught3.PokemonNPC;
                    modPlayer.thirdslotname = ultraBallCaught3.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.modItem is DuskBallCaught)
                {
                    var duskBallCaught3 = (DuskBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.modItem;
                    modPlayer.thirdslottype = duskBallCaught3.PokemonNPC;
                    modPlayer.thirdslotname = duskBallCaught3.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.modItem is PremierBallCaught)
                {
                    var premierBallCaught3 = (PremierBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.modItem;
                    modPlayer.thirdslottype = premierBallCaught3.PokemonNPC;
                    modPlayer.thirdslotname = premierBallCaught3.PokemonName;
                }
            }
            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.IsAir)
            {
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.modItem is PokeballCaught)
                {
                    var pokeballCaught4 = (PokeballCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.modItem;
                    modPlayer.fourthslottype = pokeballCaught4.PokemonNPC;
                    modPlayer.fourthslotname = pokeballCaught4.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.modItem is GreatBallCaught)
                {
                    var greatBallCaught4 = (GreatBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.modItem;
                    modPlayer.fourthslottype = greatBallCaught4.PokemonNPC;
                    modPlayer.fourthslotname = greatBallCaught4.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.modItem is UltraBallCaught)
                {
                    var ultraBallCaught4 = (UltraBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.modItem;
                    modPlayer.fourthslottype = ultraBallCaught4.PokemonNPC;
                    modPlayer.fourthslotname = ultraBallCaught4.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.modItem is DuskBallCaught)
                {
                    var duskBallCaught4 = (DuskBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.modItem;
                    modPlayer.fourthslottype = duskBallCaught4.PokemonNPC;
                    modPlayer.fourthslotname = duskBallCaught4.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.modItem is PremierBallCaught)
                {
                    var premierBallCaught4 = (PremierBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.modItem;
                    modPlayer.fourthslottype = premierBallCaught4.PokemonNPC;
                    modPlayer.fourthslotname = premierBallCaught4.PokemonName;
                }
            }
            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.IsAir)
            {
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.modItem is PokeballCaught)
                {
                    var pokeballCaught5 = (PokeballCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.modItem;
                    modPlayer.fifthslottype = pokeballCaught5.PokemonNPC;
                    modPlayer.fifthslotname = pokeballCaught5.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.modItem is GreatBallCaught)
                {
                    var greatBallCaught5 = (GreatBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.modItem;
                    modPlayer.fifthslottype = greatBallCaught5.PokemonNPC;
                    modPlayer.fifthslotname = greatBallCaught5.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.modItem is UltraBallCaught)
                {
                    var ultraBallCaught5 = (UltraBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.modItem;
                    modPlayer.fifthslottype = ultraBallCaught5.PokemonNPC;
                    modPlayer.fifthslotname = ultraBallCaught5.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.modItem is DuskBallCaught)
                {
                    var duskBallCaught5 = (DuskBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.modItem;
                    modPlayer.fifthslottype = duskBallCaught5.PokemonNPC;
                    modPlayer.fifthslotname = duskBallCaught5.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.modItem is PremierBallCaught)
                {
                    var premierBallCaught5 = (PremierBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.modItem;
                    modPlayer.fifthslottype = premierBallCaught5.PokemonNPC;
                    modPlayer.fifthslotname = premierBallCaught5.PokemonName;
                }
            }
            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.IsAir)
            {
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem is PokeballCaught)
                {
                    var pokeballCaught6 = (PokeballCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem;
                    modPlayer.sixthslottype = pokeballCaught6.PokemonNPC;
                    modPlayer.sixthslotname = pokeballCaught6.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem is GreatBallCaught)
                {
                    var greatBallCaught6 = (GreatBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem;
                    modPlayer.sixthslottype = greatBallCaught6.PokemonNPC;
                    modPlayer.sixthslotname = greatBallCaught6.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem is UltraBallCaught)
                {
                    var ultraBallCaught6 = (UltraBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem;
                    modPlayer.sixthslottype = ultraBallCaught6.PokemonNPC;
                    modPlayer.sixthslotname = ultraBallCaught6.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem is DuskBallCaught)
                {
                    var duskBallCaught6 = (DuskBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem;
                    modPlayer.sixthslottype = duskBallCaught6.PokemonNPC;
                    modPlayer.sixthslotname = duskBallCaught6.PokemonName;
                }
                if (ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem is PremierBallCaught)
                {
                    var premierBallCaught6 = (PremierBallCaught)ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.modItem;
                    modPlayer.sixthslottype = premierBallCaught6.PokemonNPC;
                    modPlayer.sixthslotname = premierBallCaught6.PokemonName;
                }
            }

            Main.NewText("Party Saved!");
            if (!partyslot1.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partyslot1.Item, partyslot1.Item.stack);
                partyslot1.Item.TurnToAir();
            }
            if (!partyslot2.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partyslot2.Item, partyslot2.Item.stack);
                partyslot2.Item.TurnToAir();
            }
            if (!partyslot3.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partyslot3.Item, partyslot3.Item.stack);
                partyslot3.Item.TurnToAir();
            }
            if (!partyslot4.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partyslot4.Item, partyslot4.Item.stack);
                partyslot4.Item.TurnToAir();
            }
            if (!partyslot5.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partyslot5.Item, partyslot5.Item.stack);
                partyslot5.Item.TurnToAir();
            }
            if (!partyslot6.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(partyslot6.Item, partyslot6.Item.stack);
                partyslot6.Item.TurnToAir();
            }
        }
    }
}
