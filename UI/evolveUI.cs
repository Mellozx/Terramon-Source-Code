using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terramon.UI
{
    // ExampleUIs visibility is toggled by typing "/coin" in chat. (See CoinCommand.cs)
    // ExampleUI is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
    internal class evolveUI : UIState
    {
        public DragableUIPanel mainPanel;
        private VanillaItemSlotWrapper pokeBallSlot;
        private VanillaItemSlotWrapper rareCandySlot;
        private VanillaItemSlotWrapper specialItemSlot;
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
            mainPanel.Left.Set(320f, 0f);
            mainPanel.Top.Set(125f, 0f);
            mainPanel.Width.Set(352, 0f);
            mainPanel.Height.Set(264f, 0f);
            mainPanel.BackgroundColor = new Color(121, 51, 101) * 0.7f;

            Texture2D starterselect = ModContent.GetTexture("Terramon/UI/EvolvePKMN"); // Evolve PKMN name
            UIImagez starterselectmenu = new UIImagez(starterselect);
            starterselectmenu.Left.Set(0, 0f);
            starterselectmenu.Top.Set(0, 0f);
            starterselectmenu.Width.Set(1, 0f);
            starterselectmenu.Height.Set(1, 0f);
            mainPanel.Append(starterselectmenu);

            Mod mod = ModContent.GetInstance<TerramonMod>();

            //item slots..?

            pokeBallSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
            {
                Left = { Pixels = 220 },
                Top = { Pixels = 210 },
                ValidItemFunc = item => item.IsAir || item.type == mod.ItemType("BlastoiseBall") || item.type == mod.ItemType("BulbasaurBall") || item.type == mod.ItemType("CaterpieBall") || item.type == mod.ItemType("CharizardBall") || item.type == mod.ItemType("CharmanderBall") || item.type == mod.ItemType("CharmeleonBall") || item.type == mod.ItemType("GastlyBall") || item.type == mod.ItemType("GengarBall") || item.type == mod.ItemType("HaunterBall") || item.type == mod.ItemType("IvysaurBall") || item.type == mod.ItemType("OddishBall") || item.type == mod.ItemType("PikachuBall") || item.type == mod.ItemType("RattataBall") || item.type == mod.ItemType("SquirtleBall") || item.type == mod.ItemType("VenusaurBall") || item.type == mod.ItemType("WartortleBall")
            };
            // Here we limit the items that can be placed in the slot. We are fine with placing an empty item in or a non-empty item that can be prefixed. Calling Prefix(-3) is the way to know if the item in question can take a prefix or not.
            mainPanel.Append(pokeBallSlot);

            // Next, we create another UIElement that we will place. Since we will be calling `mainPanel.Append(playButton);`, Left and Top are relative to the top left of the mainPanel UIElement. 
            // By properly nesting UIElements, we can position things relatively to each other easily.

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

        private void DiscButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuOpen);
            System.Diagnostics.Process.Start("https://discord.gg/MyeY4AM");
        }




    }
}
