﻿using Microsoft.Xna.Framework;
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

            Append(mainPanel);



            // As a recap, ExampleUI is a UIState, meaning it covers the whole screen. We attach mainPanel to ExampleUI some distance from the top left corner.
            // We then place playButton, closeButton, and moneyDiplay onto mainPanel so we can easily place these UIElements relative to mainPanel.
            // Since mainPanel will move, this proper organization will move playButton, closeButton, and moneyDiplay properly when mainPanel moves.
        }
        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuOpen);
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