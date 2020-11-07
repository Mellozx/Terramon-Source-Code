using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System;
using Razorwing.Framework.Utils;
using Razorwing.Framework.Graphics;
using Terramon.UI.SidebarParty;
using Terramon.Network.Starter;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Players;

namespace Terramon.UI.Starter
{
    // ExampleUIs visibility is toggled by typing "/coin" in chat. (See CoinCommand.cs)
    // ExampleUI is a simple UI example showing how to use UIPanel, UIImageButton, and even a custom UIElement.
    internal class ChooseStarter : UIState
    {
        public NonDragableUIPanel mainPanel;
        public static bool Visible;

        private UIText testmenu;
        private UIText testmenu2;

        // POKeMON BTN DEFINITIONS

        public UIHoverImageButton bulbasaurTextureButton;
        public UIHoverImageButtonDisabled chikoritaTextureButton;
        public UIHoverImageButtonDisabled treeckoTextureButton;
        public UIHoverImageButtonDisabled turtwigTextureButton;
        public UIHoverImageButtonDisabled snivyTextureButton;
        public UIHoverImageButtonDisabled chespinTextureButton;
        public UIHoverImageButtonDisabled rowletTextureButton;

        public UIHoverImageButton squirtleTextureButton;
        public UIHoverImageButtonDisabled totodileTextureButton;
        public UIHoverImageButtonDisabled mudkipTextureButton;
        public UIHoverImageButtonDisabled piplupTextureButton;
        public UIHoverImageButtonDisabled oshawottTextureButton;
        public UIHoverImageButtonDisabled froakieTextureButton;
        public UIHoverImageButtonDisabled popplioTextureButton;

        public UIHoverImageButton charmanderTextureButton;
        public UIHoverImageButtonDisabled cyndaquilTextureButton;
        public UIHoverImageButtonDisabled torchicTextureButton;
        public UIHoverImageButtonDisabled chimcharTextureButton;
        public UIHoverImageButtonDisabled tepigTextureButton;
        public UIHoverImageButtonDisabled fennekinTextureButton;
        public UIHoverImageButtonDisabled littenTextureButton;

        private UIText pokemonNameText;
        private UIText pokemonDescText;

        private UIImagez starterselectmenu;

        private UIImagez bartop;
        private UIImagez barbottom;

        private UIImagez shaderBar1;
        private UIImagez shaderBar2;
        private UIImagez shaderBar3;

        float shaderBar1Speed;
        float shaderBar2Speed;
        float shaderBar3Speed;
        public override void OnInitialize()
        {
            // Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
            mainPanel = new NonDragableUIPanel();
            mainPanel.SetPadding(0);
            mainPanel.Left.Set(0f, 0f);
            mainPanel.Top.Set(0f, 0f);
            mainPanel.Width.Set(0f, 1f);
            mainPanel.Height.Set(0f, 1f);

            Texture2D starterselect = ModContent.GetTexture("Terramon/UI/Starter/CheckeredBackground");
            starterselectmenu = new UIImagez(starterselect);
            starterselectmenu.Left.Set(0, 0);
            starterselectmenu.Top.Set(0, 0);
            starterselectmenu.Width.Set(1, 0);
            starterselectmenu.Height.Set(1, 0);
            mainPanel.Append(starterselectmenu);

            Texture2D shaderBarTexture = ModContent.GetTexture("Terramon/UI/Starter/ShaderBar");

                    shaderBar1 =
                        new UIImagez(shaderBarTexture);
                    shaderBar2 =
                        new UIImagez(shaderBarTexture);
                    shaderBar3 =
                        new UIImagez(shaderBarTexture);

            shaderBar1.HAlign = -0.5f;
            shaderBar1.VAlign = 0.1f;
            shaderBar1.Width.Set(1, 0f);
            shaderBar1.Height.Set(1, 0f);
            mainPanel.Append(shaderBar1);

            shaderBar2.HAlign = -0.5f;
            shaderBar2.VAlign = 0.1f;
            shaderBar2.Width.Set(1, 0f);
            shaderBar2.Height.Set(1, 0f);
            mainPanel.Append(shaderBar2);

            shaderBar3.HAlign = -0.5f;
            shaderBar3.VAlign = 0.1f;
            shaderBar3.Width.Set(1, 0f);
            shaderBar3.Height.Set(1, 0f);
            mainPanel.Append(shaderBar3);

            Texture2D bartoptexture = ModContent.GetTexture("Terramon/UI/Starter/BarTop");
            bartop = new UIImagez(bartoptexture);
            bartop.HAlign = 0f;
            bartop.VAlign = 0f;
            bartop.Top.Set(0, 0);
            bartop.Width.Set(1, 0);
            bartop.Height.Set(1, 0);
            mainPanel.Append(bartop);

            Texture2D barbottomtexture = ModContent.GetTexture("Terramon/UI/Starter/BarBottom");
            barbottom = new UIImagez(barbottomtexture);
            barbottom.HAlign = 0f;
            barbottom.VAlign = 1f;
            barbottom.Width.Set(1792, 0);
            barbottom.Height.Set(100, 0);
            mainPanel.Append(barbottom);

            testmenu = new UIText("0/0");
            testmenu.HAlign = 0.5f; // 1
            testmenu.VAlign = 0.2f; // 1
            testmenu.Width.Set(1, 0);
            testmenu.Height.Set(1, 0);
            testmenu.SetText("Welcome to the world of Pokémon! Thank you for downloading this mod!", 1.1f, false);
            mainPanel.Append(testmenu);

            testmenu2 = new UIText("0/0");
            testmenu2.HAlign = 0.5f;
            testmenu2.Top.Set(30, 0);
            testmenu2.Width.Set(1, 0);
            testmenu2.Height.Set(1, 0);
            testmenu2.SetText("Now, please choose your desired starter Pokémon!", 1.1f, false);
            testmenu.Append(testmenu2);

            pokemonNameText = new UIText("0/0");
            pokemonNameText.HAlign = 0.5f; // 1
            pokemonNameText.VAlign = 0.725f; // 1
            pokemonNameText.Width.Set(1, 0);
            pokemonNameText.Height.Set(1, 0);
            pokemonNameText.SetText("", 1.1f, false);
            mainPanel.Append(pokemonNameText);

            pokemonDescText = new UIText("0/0");
            pokemonDescText.HAlign = 0.5f; // 1
            pokemonDescText.VAlign = 0.8f; // 1
            pokemonDescText.Width.Set(1, 0);
            pokemonDescText.Height.Set(1, 0);
            pokemonDescText.SetText("", 1.1f, false);
            mainPanel.Append(pokemonDescText);

            // Grass types

            Texture2D bulbasaurTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Bulbasaur");
                bulbasaurTextureButton =
                    new UIHoverImageButton(bulbasaurTexture, "Bulbasaur");
            bulbasaurTextureButton.HAlign = 0.265f;
            bulbasaurTextureButton.VAlign = 0.35f;
            bulbasaurTextureButton.Width.Set(50, 0f);
            bulbasaurTextureButton.Height.Set(46, 0f);
            bulbasaurTextureButton.OnMouseOver += bulbasaurHovered;
            bulbasaurTextureButton.OnMouseOut += unHovered;
            bulbasaurTextureButton.OnClick += bulbasaurTextureButtonClicked;
            mainPanel.Append(bulbasaurTextureButton);

            Texture2D chikoritaTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Chikorita");
                chikoritaTextureButton =
                    new UIHoverImageButtonDisabled(chikoritaTexture, "Coming Soon...");
            chikoritaTextureButton.HAlign = 0.345f;
            chikoritaTextureButton.VAlign = 0.35f;
            chikoritaTextureButton.Width.Set(50, 0f);
            chikoritaTextureButton.Height.Set(46, 0f);
            //chikoritaTextureButton.OnClick += bulbasaurTextureButtonClicked;
            mainPanel.Append(chikoritaTextureButton);

            Texture2D treeckoTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Treecko");
                treeckoTextureButton =
                    new UIHoverImageButtonDisabled(treeckoTexture, "Coming Soon...");
            treeckoTextureButton.HAlign = 0.425f;
            treeckoTextureButton.VAlign = 0.35f;
            treeckoTextureButton.Width.Set(50, 0f);
            treeckoTextureButton.Height.Set(46, 0f);
            //treeckoTextureButton.OnClick += bulbasaurTextureButtonClicked;
            mainPanel.Append(treeckoTextureButton);

            Texture2D turtwigTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Turtwig");
                turtwigTextureButton =
                    new UIHoverImageButtonDisabled(turtwigTexture, "Coming Soon...");
            turtwigTextureButton.HAlign = 0.505f;
            turtwigTextureButton.VAlign = 0.35f;
            turtwigTextureButton.Width.Set(50, 0f);
            turtwigTextureButton.Height.Set(46, 0f);
            //turtwigTextureButton.OnClick += bulbasaurTextureButtonClicked;
            mainPanel.Append(turtwigTextureButton);

            Texture2D snivyTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Snivy");
                snivyTextureButton =
                    new UIHoverImageButtonDisabled(snivyTexture, "Coming Soon...");
            snivyTextureButton.HAlign = 0.585f;
            snivyTextureButton.VAlign = 0.35f;
            snivyTextureButton.Width.Set(50, 0f);
            snivyTextureButton.Height.Set(46, 0f);
            //snivyTextureButton.OnClick += bulbasaurTextureButtonClicked;
            mainPanel.Append(snivyTextureButton);

            Texture2D chespinTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Chespin");
                chespinTextureButton =
                    new UIHoverImageButtonDisabled(chespinTexture, "Coming Soon...");
            chespinTextureButton.HAlign = 0.665f;
            chespinTextureButton.VAlign = 0.35f;
            chespinTextureButton.Width.Set(50, 0f);
            chespinTextureButton.Height.Set(46, 0f);
            //chespinTextureButton.OnClick += bulbasaurTextureButtonClicked;
            mainPanel.Append(chespinTextureButton);

            Texture2D rowletTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Rowlet");
                rowletTextureButton =
                    new UIHoverImageButtonDisabled(rowletTexture, "Coming Soon...");
            rowletTextureButton.HAlign = 0.745f;
            rowletTextureButton.VAlign = 0.35f;
            rowletTextureButton.Width.Set(50, 0f);
            rowletTextureButton.Height.Set(46, 0f);
            //rowletTextureButton.OnClick += bulbasaurTextureButtonClicked;
            mainPanel.Append(rowletTextureButton);

            // Water types

            Texture2D squirtleTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Squirtle");
                squirtleTextureButton =
                    new UIHoverImageButton(squirtleTexture, "Squirtle");
            squirtleTextureButton.HAlign = 0.265f;
            squirtleTextureButton.VAlign = 0.45f;
            squirtleTextureButton.Width.Set(50, 0f);
            squirtleTextureButton.Height.Set(46, 0f);
            squirtleTextureButton.OnMouseOver += squirtleHovered;
            squirtleTextureButton.OnMouseOut += unHovered;
            squirtleTextureButton.OnClick += squirtleTextureButtonClicked;
            mainPanel.Append(squirtleTextureButton);

            Texture2D totodileTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Totodile");
                totodileTextureButton =
                    new UIHoverImageButtonDisabled(totodileTexture, "Coming Soon...");
            totodileTextureButton.HAlign = 0.345f;
            totodileTextureButton.VAlign = 0.45f;
            totodileTextureButton.Width.Set(50, 0f);
            totodileTextureButton.Height.Set(46, 0f);
            //totodileTextureButton.OnClick += squirtleTextureButtonClicked;
            mainPanel.Append(totodileTextureButton);

            Texture2D mudkipTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Mudkip");
                mudkipTextureButton =
                    new UIHoverImageButtonDisabled(mudkipTexture, "Coming Soon...");
            mudkipTextureButton.HAlign = 0.425f;
            mudkipTextureButton.VAlign = 0.45f;
            mudkipTextureButton.Width.Set(50, 0f);
            mudkipTextureButton.Height.Set(46, 0f);
            //mudkipTextureButton.OnClick += squirtleTextureButtonClicked;
            mainPanel.Append(mudkipTextureButton);

            Texture2D piplupTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Piplup");
                piplupTextureButton =
                    new UIHoverImageButtonDisabled(piplupTexture, "Coming Soon...");
            piplupTextureButton.HAlign = 0.505f;
            piplupTextureButton.VAlign = 0.45f;
            piplupTextureButton.Width.Set(50, 0f);
            piplupTextureButton.Height.Set(46, 0f);
            //piplupTextureButton.OnClick += squirtleTextureButtonClicked;
            mainPanel.Append(piplupTextureButton);

            Texture2D oshawottTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Oshawott");
                oshawottTextureButton =
                    new UIHoverImageButtonDisabled(oshawottTexture, "Coming Soon...");
            oshawottTextureButton.HAlign = 0.585f;
            oshawottTextureButton.VAlign = 0.45f;
            oshawottTextureButton.Width.Set(50, 0f);
            oshawottTextureButton.Height.Set(46, 0f);
            //oshawottTextureButton.OnClick += squirtleTextureButtonClicked;
            mainPanel.Append(oshawottTextureButton);

            Texture2D froakieTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Froakie");
                froakieTextureButton =
                    new UIHoverImageButtonDisabled(froakieTexture, "Coming Soon...");
            froakieTextureButton.HAlign = 0.665f;
            froakieTextureButton.VAlign = 0.45f;
            froakieTextureButton.Width.Set(50, 0f);
            froakieTextureButton.Height.Set(46, 0f);
            //froakieTextureButton.OnClick += squirtleTextureButtonClicked;
            mainPanel.Append(froakieTextureButton);

            Texture2D popplioTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Popplio");
                popplioTextureButton =
                    new UIHoverImageButtonDisabled(popplioTexture, "Coming Soon...");
            popplioTextureButton.HAlign = 0.745f;
            popplioTextureButton.VAlign = 0.45f;
            popplioTextureButton.Width.Set(50, 0f);
            popplioTextureButton.Height.Set(46, 0f);
            //popplioTextureButton.OnClick += squirtleTextureButtonClicked;
            mainPanel.Append(popplioTextureButton);

            // Fire types

            Texture2D charmanderTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Charmander");
                charmanderTextureButton =
                    new UIHoverImageButton(charmanderTexture, "Charmander");
            charmanderTextureButton.HAlign = 0.265f;
            charmanderTextureButton.VAlign = 0.55f;
            charmanderTextureButton.Width.Set(50, 0f);
            charmanderTextureButton.Height.Set(46, 0f);
            charmanderTextureButton.OnMouseOver += charmanderHovered;
            charmanderTextureButton.OnMouseOut += unHovered;
            charmanderTextureButton.OnClick += charmanderTextureButtonClicked;
            mainPanel.Append(charmanderTextureButton);

            Texture2D cyndaquilTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Cyndaquil");
                cyndaquilTextureButton =
                    new UIHoverImageButtonDisabled(cyndaquilTexture, "Coming Soon...");
            cyndaquilTextureButton.HAlign = 0.345f;
            cyndaquilTextureButton.VAlign = 0.55f;
            cyndaquilTextureButton.Width.Set(50, 0f);
            cyndaquilTextureButton.Height.Set(46, 0f);
            //cyndaquilTextureButton.OnClick += charmanderTextureButtonClicked;
            mainPanel.Append(cyndaquilTextureButton);

            Texture2D torchicTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Torchic");
                torchicTextureButton =
                    new UIHoverImageButtonDisabled(torchicTexture, "Coming Soon...");
            torchicTextureButton.HAlign = 0.425f;
            torchicTextureButton.VAlign = 0.55f;
            torchicTextureButton.Width.Set(50, 0f);
            torchicTextureButton.Height.Set(46, 0f);
            //torchicTextureButton.OnClick += charmanderTextureButtonClicked;
            mainPanel.Append(torchicTextureButton);

            Texture2D chimcharTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Chimchar");
                chimcharTextureButton =
                    new UIHoverImageButtonDisabled(chimcharTexture, "Coming Soon...");
            chimcharTextureButton.HAlign = 0.505f;
            chimcharTextureButton.VAlign = 0.55f;
            chimcharTextureButton.Width.Set(50, 0f);
            chimcharTextureButton.Height.Set(46, 0f);
            //chimcharTextureButton.OnClick += charmanderTextureButtonClicked;
            mainPanel.Append(chimcharTextureButton);

            Texture2D tepigTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Tepig");
                tepigTextureButton =
                    new UIHoverImageButtonDisabled(tepigTexture, "Coming Soon...");
            tepigTextureButton.HAlign = 0.585f;
            tepigTextureButton.VAlign = 0.55f;
            tepigTextureButton.Width.Set(50, 0f);
            tepigTextureButton.Height.Set(46, 0f);
            //tepigTextureButton.OnClick += charmanderTextureButtonClicked;
            mainPanel.Append(tepigTextureButton);

            Texture2D fennekinTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Fennekin");
                fennekinTextureButton =
                    new UIHoverImageButtonDisabled(fennekinTexture, "Coming Soon...");
            fennekinTextureButton.HAlign = 0.665f;
            fennekinTextureButton.VAlign = 0.55f;
            fennekinTextureButton.Width.Set(50, 0f);
            fennekinTextureButton.Height.Set(46, 0f);
            //fennekinTextureButton.OnClick += charmanderTextureButtonClicked;
            mainPanel.Append(fennekinTextureButton);

            Texture2D littenTexture = ModContent.GetTexture("Terramon/UI/Starter/PKMN/Litten");
                littenTextureButton =
                    new UIHoverImageButtonDisabled(littenTexture, "Coming Soon...");
            littenTextureButton.HAlign = 0.745f;
            littenTextureButton.VAlign = 0.55f;
            littenTextureButton.Width.Set(50, 0f);
            littenTextureButton.Height.Set(46, 0f);
            //littenTextureButton.OnClick += charmanderTextureButtonClicked;
            mainPanel.Append(littenTextureButton);

            Append(mainPanel);

            double n = Main.rand.NextDouble() * (0.85 - 0.15) + 0.15;
            shaderBar1.VAlign = (float)n;
            n = Main.rand.NextDouble() * (0.85 - 0.15) + 0.1;
            shaderBar2.VAlign = (float)n;
            n = Main.rand.NextDouble() * (0.85 - 0.15) + 0.1;
            shaderBar3.VAlign = (float)n;

            double s = Main.rand.NextDouble() * (0.009 - 0.002) + 0.002;
            shaderBar1Speed = (float)s;
            s = Main.rand.NextDouble() * (0.008 - 0.008) + 0.002;
            shaderBar2Speed = (float)s;
            s = Main.rand.NextDouble() * (0.008 - 0.008) + 0.002;
            shaderBar3Speed = (float)s;
        }


        private Player player = Main.LocalPlayer;

        float shaderBar1Timer = 0;
        float shaderBar1MoveTimer = 0;

        private Random rand = new Random();

        byte didSelectStarter = 0;
        bool render = true;

        double start;
        double end;

        double abc = 0;

        public override void Update(GameTime gameTime)
        {
            // Increment timers
            shaderBar1Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            shaderBar1MoveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (shaderBar1Timer >= 1)
            {
                shaderBar1Timer = 0;
            }

            // Check if pokemon was just selected
            if (didSelectStarter == 1)
            {
                start = gameTime.TotalGameTime.TotalSeconds;
                end = start + 2;
                didSelectStarter = 2;
            }

            if (gameTime.TotalGameTime.TotalSeconds > end && didSelectStarter == 2)
            {
                Visible = false;
                ModContent.GetInstance<TerramonMod>()._exampleUserInterface.SetState(null);
            }

            if (didSelectStarter == 2 && gameTime.TotalGameTime.TotalSeconds < end)
            {
                render = false;

                testmenu.SetText("", 1.1f, false);
                testmenu2.SetText("", 1.1f, false);
                pokemonNameText.SetText("", 1.1f, false);
                pokemonDescText.SetText("", 1.1f, false);

                bartop.VAlign = (float)Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0, -0.2, start, end, Easing.Out);
                barbottom.VAlign = (float)Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 1, 1.2, start, end, Easing.Out);
                mainPanel.BackgroundColor = new Color(97, 97, 97) * (float)Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.7, 0, start, end, Easing.None);
                
                starterselectmenu._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 1f, 0f, start, end, Easing.None);
                shaderBar1._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 1f, 0f, start, end, Easing.None);
                shaderBar2._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 1f, 0f, start, end, Easing.None);
                shaderBar3._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 1f, 0f, start, end, Easing.None);

                // fade out pkmn

                bulbasaurTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 1f, 0f, start, end - 1, Easing.None);
                chikoritaTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                treeckoTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                turtwigTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                snivyTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                chespinTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                rowletTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                
                squirtleTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 1f, 0f, start, end - 1, Easing.None);
                totodileTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                mudkipTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                piplupTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                oshawottTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                froakieTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                popplioTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);

                charmanderTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 1f, 0f, start, end - 1, Easing.None);
                cyndaquilTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                torchicTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                chimcharTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                tepigTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                fennekinTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
                littenTextureButton._visibilityActive = Interpolation.ValueAt(gameTime.TotalGameTime.TotalSeconds, 0.4f, 0f, start, end - 1, Easing.None);
            }

            // Move bars across screen

            if (shaderBar1MoveTimer > 0.01)
            {
                shaderBar1.HAlign += shaderBar1Speed;
                shaderBar2.HAlign += shaderBar2Speed;
                shaderBar3.HAlign += shaderBar3Speed;
                shaderBar1MoveTimer = 0;
            }

            if (shaderBar1.HAlign > 1.3f)
            {
                shaderBar1.HAlign = -0.9f;
                var n = Main.rand.NextDouble() * (0.85 - 0.05) + 0.1;
                shaderBar1.VAlign = (float)n;

                // recalculate the speed

                double s = Main.rand.NextDouble() * (0.008 - 0.003) + 0.003;
                shaderBar1Speed = (float)s;
            }

            if (shaderBar2.HAlign > 1.3f)
            {
                shaderBar2.HAlign = -0.9f;
                var n = Main.rand.NextDouble() * (0.85 - 0.05) + 0.1;
                shaderBar2.VAlign = (float)n;

                // recalculate the speed

                double s = Main.rand.NextDouble() * (0.008 - 0.003) + 0.003;
                shaderBar2Speed = (float)s;
            }

            if (shaderBar3.HAlign > 1.3f)
            {
                shaderBar3.HAlign = -0.9f;
                var n = Main.rand.NextDouble() * (0.85 - 0.05) + 0.1;
                shaderBar3.VAlign = (float)n;

                // recalculate the speed

                double s = Main.rand.NextDouble() * (0.008 - 0.003) + 0.003;
                shaderBar3Speed = (float)s;
            }

            // Don't delete this or the UIElements attached to this UIState will cease to function.
            base.Update(gameTime);
        }

        private void bulbasaurHovered(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!render) return;
            pokemonNameText.SetText("Bulbasaur", 1.1f, false);
            pokemonDescText.SetText("A strange seed was planted on its back at birth. The plant sprouts and grows with this Pokémon.", 1.1f, false);
        }
        private void squirtleHovered(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!render) return;
            pokemonNameText.SetText("Squirtle", 1.1f, false);
            pokemonDescText.SetText("It shelters itself in its shell, then strikes back with spouts of water at every opportunity.", 1.1f, false);
        }
        private void charmanderHovered(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!render) return;
            pokemonNameText.SetText("Charmander", 1.1f, false);
            pokemonDescText.SetText("The flame on its tail indicates Charmander’s life force. If it is healthy, the flame burns brightly.", 1.1f, false);
        }
        private void unHovered(UIMouseEvent evt, UIElement listeningElement)
        {
            pokemonNameText.SetText("", 1.1f, false);
            pokemonDescText.SetText("", 1.1f, false);
        }
        private void bulbasaurTextureButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            didSelectStarter = 1;

            TerramonPlayer p = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            Mod mod = ModContent.GetInstance<TerramonMod>();
            Player player = Main.LocalPlayer;
            Main.PlaySound(SoundID.MenuOpen);
            Main.PlaySound(SoundID.Coins);
            p.StarterChosen = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                SpawnStarterPacket packet = new SpawnStarterPacket();
                packet.Send((TerramonMod)mod, SpawnStarterPacket.BULBASAUR);
            }
            else
            {
                int index = Item.NewItem(player.getRect(), ModContent.ItemType<PokeballCaught>());
                if (index >= 400)
                    return;
                //(Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<BulbasaurNPC>();
                (Main.item[index].modItem as PokeballCaught).PokemonName = "Bulbasaur";
                (Main.item[index].modItem as PokeballCaught).SmallSpritePath =
                    "Terramon/Minisprites/Regular/miniBulbasaur";
            }
            UISidebar.Visible = true;
        }

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
    }
}