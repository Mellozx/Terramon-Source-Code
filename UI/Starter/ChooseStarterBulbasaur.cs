using Microsoft.Xna.Framework.Graphics;
using Terramon.Players;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terramon.UI.Starter
{
    // ExampleUIs visibility is toggled by typing "/coin" in chat. (See CoinCommand.cs)
    // ExampleUI is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
    internal class ChooseStarterBulbasaur : UIState
    {
        public NonDragableUIPanel mainPanel;
        public static bool Visible;

        // In OnInitialize, we place various UIElements onto our UIState (this class).
        // UIState classes have width and height equal to the full screen, because of this, usually we first define a UIElement that will act as the container for our UI.
        // We then place various other UIElement onto that container UIElement positioned relative to the container UIElement.
        public override void OnInitialize()
        {
            // Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
            mainPanel = new NonDragableUIPanel();
            mainPanel.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            mainPanel.Left.Set(0f, 0f);
            mainPanel.Top.Set(0f, 0f);
            mainPanel.Width.Set(0f, 1f);
            mainPanel.Height.Set(0f, 1f);

            //pokemon icons



            // Next, we create another UIElement that we will place. Since we will be calling `mainPanel.Append(playButton);`, Left and Top are relative to the top left of the mainPanel UIElement. 
            // By properly nesting UIElements, we can position things relatively to each other easily.




            Texture2D starterselect = ModContent.GetTexture("Terramon/UI/PossibleAssets/StarterMenuNew");
            UIImagez starterselectmenu = new UIImagez(starterselect);
            starterselectmenu.Left.Set(0, 0);
            starterselectmenu.Top.Set(0, 0);
            starterselectmenu.Width.Set(1, 0);
            starterselectmenu.Height.Set(1, 0);
            mainPanel.Append(starterselectmenu);

            Texture2D test = ModContent.GetTexture("Terramon/UI/PossibleAssets/Text");
             UIImagez testmenu = new UIImagez(test);
             testmenu.HAlign = 0.5f; // 1
             testmenu.VAlign = 0.3f; // 1
              testmenu.Width.Set(391, 0);
              testmenu.Height.Set(99, 0);
             mainPanel.Append(testmenu);

            Texture2D bulbasaurTexture = ModContent.GetTexture("Terramon/UI/PossibleAssets/Bulbasaur");
            UIImagez bulbasaurTextureButton = new UIImagez(bulbasaurTexture); // Localized text for "Close"
            bulbasaurTextureButton.HAlign = 0.35f; // 1
            bulbasaurTextureButton.VAlign = 0.5f; // 1bulbasaurTextureButton.Left.Set(63, 0f);
            bulbasaurTextureButton.Width.Set(100, 0f);
            bulbasaurTextureButton.Height.Set(92, 0f);
            mainPanel.Append(bulbasaurTextureButton);

            Texture2D charmanderTexture = ModContent.GetTexture("Terramon/UI/PossibleAssets/Charmander");
            UIHoverImageButton charmanderTextureButton = new UIHoverImageButton(charmanderTexture, "Charmander"); // Localized text for "Close"
            charmanderTextureButton.HAlign = 0.5f; // 1
            charmanderTextureButton.VAlign = 0.5f; // 1bulbasaurTextureButton.Left.Set(63, 0f);
            charmanderTextureButton.Width.Set(100, 0f);
            charmanderTextureButton.Height.Set(92, 0f);
            charmanderTextureButton.OnClick += new MouseEvent(charmanderTextureButtonClicked);
            mainPanel.Append(charmanderTextureButton);

            Texture2D squirtleTexture = ModContent.GetTexture("Terramon/UI/PossibleAssets/Squirtle");
            UIHoverImageButton squirtleTextureButton = new UIHoverImageButton(squirtleTexture, "Squirtle"); // Localized text for "Close"
            squirtleTextureButton.HAlign = 0.65f; // 1
            squirtleTextureButton.VAlign = 0.5f; // 1bulbasaurTextureButton.Left.Set(63, 0f);
            squirtleTextureButton.Width.Set(100, 0f);
            squirtleTextureButton.Height.Set(92, 0f);
            squirtleTextureButton.OnClick += new MouseEvent(squirtleTextureButtonClicked);
            mainPanel.Append(squirtleTextureButton);

            Texture2D bulbasaurTextTexture = ModContent.GetTexture("Terramon/UI/PossibleAssets/BulbasaurText");
            UIImagez bulbasaurText = new UIImagez(bulbasaurTextTexture);
            bulbasaurText.HAlign = 0.5f; // 1
            bulbasaurText.VAlign = 0.69f; // 1
            bulbasaurText.Width.Set(307, 0);
            bulbasaurText.Height.Set(57, 0);
            mainPanel.Append(bulbasaurText);

            Texture2D chooseTexture = ModContent.GetTexture("Terramon/UI/PossibleAssets/Choose");
            UIHoverImageButton choose = new UIHoverImageButton(chooseTexture, "Choose Bulbasaur!");
            choose.HAlign = 0.5f; // 1
            choose.VAlign = 0.8f; // 1
            choose.Width.Set(153, 0);
            choose.Height.Set(43, 0);
            choose.OnClick += new MouseEvent(Chosen);
            mainPanel.Append(choose);



            Append(mainPanel);

            // As a recap, ExampleUI is a UIState, meaning it covers the whole screen. We attach mainPanel to ExampleUI some distance from the top left corner.
            // We then place playButton, closeButton, and moneyDiplay onto mainPanel so we can easily place these UIElements relative to mainPanel.
            // Since mainPanel will move, this proper organization will move playButton, closeButton, and moneyDiplay properly when mainPanel moves.
        }

        
        Player player = Main.LocalPlayer;
       
        private void charmanderTextureButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuOpen);
            ModContent.GetInstance<TerramonMod>()._exampleUserInterface.SetState(new ChooseStarterCharmander());
        }
        private void squirtleTextureButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuOpen);
            ModContent.GetInstance<TerramonMod>()._exampleUserInterface.SetState(new ChooseStarterSquirtle());
        }
        private void Chosen(UIMouseEvent evt, UIElement listeningElement)
        {
            TerramonPlayer TerramonPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            Mod mod = ModContent.GetInstance<TerramonMod>();
            Player player = Main.LocalPlayer;
            Main.PlaySound(SoundID.MenuOpen);
            ModContent.GetInstance<TerramonMod>()._exampleUserInterface.SetState(null);
            Main.PlaySound(SoundID.Coins);
            TerramonPlayer.StarterChosen = true;
            Item.NewItem(Main.LocalPlayer.getRect(), mod.ItemType("BulbasaurBall"));
            Main.NewText("You chose [c/33FF33:Bulbasaur, the Seed Pokemon.] Great choice!");
            ChooseStarter.Visible = false;
        }


    }
}
