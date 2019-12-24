using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public UIImagez testmenu;
        public Texture2D test;

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
            mainPanel.VAlign = 0.5f;
            mainPanel.Width.Set(70, 0f);
            mainPanel.Height.Set(300f, 0f);
            mainPanel.BackgroundColor = new Color(50, 50, 50) * 0.5f;
            Append(mainPanel);

            Texture2D chooseTexture = ModContent.GetTexture("Terramon/UI/SidebarParty/Help");
            UIHoverImageButton choose = new UIHoverImageButton(chooseTexture, "Show Terramon Help");
            choose.HAlign = 0.007f; // 1
            choose.VAlign = 0.98f; // 1
            choose.Width.Set(20, 0);
            choose.Height.Set(32, 0);
            choose.OnClick += new MouseEvent(HelpClicked);
            Append(choose);

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
                    mainPanel.BackgroundColor = new Color(50, 50, 50) * 0.5f;
                }
            }
            if (TerramonMod.PartyUIAutoMode == true)
            {
                if (!Main.dayTime)
                {
                    mainPanel.BackgroundColor = new Color(50, 50, 50) * 0.5f;
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
                    mainPanel.BackgroundColor = new Color(50, 50, 50) * 0.5f;
                }
            }

        }

        private void HelpClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.NewText("Welcome to Terramon v0.1.2, where you can discover and catch Pokémon in Terraria! For support, join the official Discord server using the [c/f7e34d:/discord] command, or open up the online wiki with the [c/f7e34d:/wiki] command.");
            Main.NewText("Check out the Mod Config from [c/ff8f33:Settings > Mod Configuration] or from the Mods menu. You can customize various aspects of the mod there.");
        }




    }
}
