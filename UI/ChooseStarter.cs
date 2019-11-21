using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terramon.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terramon.UI
{
    // ExampleUIs visibility is toggled by typing "/coin" in chat. (See CoinCommand.cs)
    // ExampleUI is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
    internal class ChooseStarter : UIState
    {
        public DragableUIPanel mainPanel;
        public static bool Visible;

        // In OnInitialize, we place various UIElements onto our UIState (this class).
        // UIState classes have width and height equal to the full screen, because of this, usually we first define a UIElement that will act as the container for our UI.
        // We then place various other UIElement onto that container UIElement positioned relative to the container UIElement.
        public override void OnInitialize()
        {
            // Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
            mainPanel = new DragableUIPanel();
            mainPanel.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            mainPanel.Left.Set(75f, 0f);
            mainPanel.Top.Set(100f, 0f);
            mainPanel.Width.Set(296, 0f);
            mainPanel.Height.Set(275f, 0f);

            //pokemon icons



            // Next, we create another UIElement that we will place. Since we will be calling `mainPanel.Append(playButton);`, Left and Top are relative to the top left of the mainPanel UIElement. 
            // By properly nesting UIElements, we can position things relatively to each other easily.


            Texture2D buttonDeleteTexture = ModContent.GetTexture("Terraria/UI/ButtonDelete");
            UIHoverImageButton closeButton = new UIHoverImageButton(buttonDeleteTexture, Language.GetTextValue("LegacyInterface.52")); // Localized text for "Close"
            closeButton.Left.Set(10, 0f);
            closeButton.Top.Set(10, 0f);
            closeButton.Width.Set(30, 0f);
            closeButton.Height.Set(30, 0f);
            closeButton.OnClick += new MouseEvent(CloseButtonClicked);
            mainPanel.Append(closeButton);

            Texture2D starterselect = ModContent.GetTexture("Terramon/UI/starterselect");
            UIImagez starterselectmenu = new UIImagez(starterselect);
            starterselectmenu.Left.Set(0, 0f);
            starterselectmenu.Top.Set(0, 0f);
            starterselectmenu.Width.Set(1, 0f);
            starterselectmenu.Height.Set(1, 0f);
            mainPanel.Append(starterselectmenu);

            Texture2D bulbasaur = ModContent.GetTexture("Terramon/Achievements/dex001");
            UIHoverImageButton bulbasauricon = new UIHoverImageButton(bulbasaur, "Choose Bulbasaur");
            bulbasauricon.Left.Set(25, 0f);
            bulbasauricon.Top.Set(170, 0f);
            bulbasauricon.Width.Set(72, 0f);
            bulbasauricon.Height.Set(72, 0f);
            bulbasauricon.OnClick += new MouseEvent(ChooseBulbasaur);
            mainPanel.Append(bulbasauricon);

            Texture2D charmander = ModContent.GetTexture("Terramon/Achievements/dex004");
            UIHoverImageButton charmandericon = new UIHoverImageButton(charmander, "Choose Charmander");
            charmandericon.Left.Set(111, 0f);
            charmandericon.Top.Set(185, 0f);
            charmandericon.Width.Set(72, 0f);
            charmandericon.Height.Set(72, 0f);
            charmandericon.OnClick += new MouseEvent(ChooseCharmander);
            mainPanel.Append(charmandericon);

            Texture2D squirtle = ModContent.GetTexture("Terramon/Achievements/dex007");
            UIHoverImageButton squirtleicon = new UIHoverImageButton(squirtle, "Choose Squirtle");
            squirtleicon.Left.Set(202, 0f);
            squirtleicon.Top.Set(170, 0f);
            squirtleicon.Width.Set(72, 0f);
            squirtleicon.Height.Set(72, 0f);
            squirtleicon.OnClick += new MouseEvent(ChooseSquirtle);
            mainPanel.Append(squirtleicon);

            Append(mainPanel);

            // As a recap, ExampleUI is a UIState, meaning it covers the whole screen. We attach mainPanel to ExampleUI some distance from the top left corner.
            // We then place playButton, closeButton, and moneyDiplay onto mainPanel so we can easily place these UIElements relative to mainPanel.
            // Since mainPanel will move, this proper organization will move playButton, closeButton, and moneyDiplay properly when mainPanel moves.
        }

        Mod achLib = ModLoader.GetMod("AchievementLib");
        Player player = Main.LocalPlayer;
        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuOpen);
            Visible = false;
        }

        private void ChooseBulbasaur(UIMouseEvent evt, UIElement listeningElement)
        {
            TerramonPlayer TerramonPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            Player player = Main.LocalPlayer;
            Mod achLib = ModLoader.GetMod("AchievementLib");
            Mod mod = ModContent.GetInstance<TerramonMod>();
            Main.PlaySound(SoundID.Coins);
            TerramonPlayer.StarterChosen = true;
            Item.NewItem(Main.LocalPlayer.getRect(), mod.ItemType("BulbasaurBall"));
            Main.NewText("You chose [c/33FF33:Bulbasaur, the Seed Pokemon.] Great choice!");
            achLib.Call("UnlockLocal", "Terramon", "Just the Beginning", player);
            Visible = false;
        }

        private void ChooseCharmander(UIMouseEvent evt, UIElement listeningElement)
        {
            TerramonPlayer TerramonPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            Player player = Main.LocalPlayer;
            Mod achLib = ModLoader.GetMod("AchievementLib");
            Mod mod = ModContent.GetInstance<TerramonMod>();
            Main.PlaySound(SoundID.Coins);
            TerramonPlayer.StarterChosen = true;
            Item.NewItem(Main.LocalPlayer.getRect(), mod.ItemType("CharmanderBall"));
            Main.NewText("You chose [c/FF8C00:Charmander, the Fire Lizard Pokemon.] Great choice!");
            achLib.Call("UnlockLocal", "Terramon", "Just the Beginning", player);
            Visible = false;
        }

        private void ChooseSquirtle(UIMouseEvent evt, UIElement listeningElement)
        {
            TerramonPlayer TerramonPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            Player player = Main.LocalPlayer;
            Mod achLib = ModLoader.GetMod("AchievementLib");
            Mod mod = ModContent.GetInstance<TerramonMod>();
            Main.PlaySound(SoundID.Coins);
            TerramonPlayer.StarterChosen = true;
            Item.NewItem(Main.LocalPlayer.getRect(), mod.ItemType("SquirtleBall"));
            Main.NewText("You chose [c/00FFFF:Squirtle, the Tiny Turtle Pokemon.] Great choice!");
            achLib.Call("UnlockLocal", "Terramon", "Just the Beginning", player);
            Visible = false;
        }


    }
}
