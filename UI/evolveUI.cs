using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terramon.Items.MiscItems;
using Terramon.Players;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terramon.Pokemon.FirstGeneration.Normal.Blastoise;
using Terramon.Pokemon.FirstGeneration.Normal.Bulbasaur;
using Terramon.Pokemon.FirstGeneration.Normal.Charizard;
using Terramon.Pokemon.FirstGeneration.Normal.Charmeleon;
using Terramon.Pokemon.FirstGeneration.Normal.Gengar;
using Terramon.Pokemon.FirstGeneration.Normal.Haunter;
using Terramon.Pokemon.FirstGeneration.Normal.Ivysaur;
using Terramon.Pokemon.FirstGeneration.Normal.Venusaur;
using Terramon.Pokemon.FirstGeneration.Normal.Wartortle;
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
    public class EvolveUI : UIState
    {
        private DragableUIPanel mainPanel;
        public static bool Visible;
        public bool lightmode = true;

        public Texture2D test;

        private UIText PokemonGoesHere;
        private UIText RareCandiesGoHere;

        private UIHoverImageButton SaveButton;

        public VanillaItemSlotWrapper partyslot1;
        public VanillaItemSlotWrapper partyslot2;

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
            mainPanel.Width.Set(180, 0f);
            mainPanel.Height.Set(70f, 0f);

            partyslot1 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 1f);
            partyslot1.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot1.HAlign = 0.15f;
            partyslot1.VAlign = 0.5f;
            partyslot1.ValidItemFunc = item => item.IsAir || item.modItem is PokeballCaught || item.modItem is GreatBallCaught || item.modItem is UltraBallCaught || item.modItem is DuskBallCaught || item.modItem is PremierBallCaught;
            mainPanel.Append(partyslot1);

            partyslot2 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 1f);
            partyslot2.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot2.HAlign = 0.65f;
            partyslot2.VAlign = 0.5f;
            partyslot2.ValidItemFunc = item => item.IsAir || item.modItem is RareCandy;
            mainPanel.Append(partyslot2);

            PokemonGoesHere = new UIText("0/0");
            PokemonGoesHere.HAlign = 0.5f;
            PokemonGoesHere.VAlign = 1.5f;
            PokemonGoesHere.SetText("Place a Pokémon in the first slot.");
            mainPanel.Append(PokemonGoesHere);

            RareCandiesGoHere = new UIText("0/0");
            RareCandiesGoHere.HAlign = 0.5f;
            RareCandiesGoHere.VAlign = 1.7f;
            RareCandiesGoHere.SetText("");
            mainPanel.Append(RareCandiesGoHere);

            Texture2D buttonSaveTexture = ModContent.GetTexture("Terraria/UI/ButtonPlay");
            SaveButton = new UIHoverImageButton(buttonSaveTexture, "Evolve!"); // Localized text for "Close"
            SaveButton.HAlign = 0.95f;
            SaveButton.VAlign = 0.53f;
            SaveButton.Width.Set(30, 0f);
            SaveButton.Height.Set(30, 0f);
            SaveButton.OnClick += new MouseEvent(EvolveButtonClicked);

            //Texture2D buttonSaveTexture = ModContent.GetTexture("Terraria/UI/ButtonPlay");
            //UIHoverImageButton SaveButton = new UIHoverImageButton(buttonSaveTexture, "Evolve"); // Localized text for "Close"
            //SaveButton.Left.Set(33, 0f);
            //SaveButton.Top.Set(7, 0f);
            //SaveButton.Width.Set(30, 0f);
            //SaveButton.Height.Set(30, 0f);
            //SaveButton.OnClick += new MouseEvent();
            //mainPanel.Append(SaveButton);

            Append(mainPanel);



            // As a recap, ExampleUI is a UIState, meaning it covers the whole screen. We attach mainPanel to ExampleUI some distance from the top left corner.
            // We then place playButton, closeButton, and moneyDiplay onto mainPanel so we can easily place these UIElements relative to mainPanel.
            // Since mainPanel will move, this proper organization will move playButton, closeButton, and moneyDiplay properly when mainPanel moves.
        }

        public override void Update(GameTime gameTime)
        {
            // Don't delete this or the UIElements attached to this UIState will cease to function.
            base.Update(gameTime);
            if (partyslot1.Item.IsAir)
            {
                PokemonGoesHere.SetText("Place a Pokémon in the first slot.");
                mainPanel.RemoveChild(partyslot2);
            }
            else
            {
                if (partyslot1.Item.modItem is PokeballCaught pokeball)
                {
                    if (pokeball.PokemonName == "Bulbasaur" || pokeball.PokemonName == "Charmander" || pokeball.PokemonName == "Squirtle")
                    {
                        PokemonGoesHere.SetText("Place 11 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 11)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else
                    if (pokeball.PokemonName == "Wartortle" || pokeball.PokemonName == "Charmeleon")
                    {
                        PokemonGoesHere.SetText("Place 20 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 20)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else
                    if (pokeball.PokemonName == "Ivysaur")
                    {
                        PokemonGoesHere.SetText("Place 16 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 16)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (pokeball.PokemonName == "Gastly")
                    {
                        PokemonGoesHere.SetText("Place 20 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 20)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (pokeball.PokemonName == "Haunter")
                    {
                        PokemonGoesHere.SetText("Place 10 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 10)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else
                    {
                        PokemonGoesHere.SetText("This Pokémon cannot evolve!");
                    }
                }
                if (partyslot1.Item.modItem is GreatBallCaught greatball)
                {
                    if (greatball.PokemonNameGreat == "Bulbasaur" || greatball.PokemonNameGreat == "Charmander" || greatball.PokemonNameGreat == "Squirtle")
                    {
                        PokemonGoesHere.SetText("Place 11 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 11)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    if (greatball.PokemonNameGreat == "Wartortle" || greatball.PokemonNameGreat == "Charmeleon")
                    {
                        PokemonGoesHere.SetText("Place 20 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 20)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    if (greatball.PokemonNameGreat == "Ivysaur")
                    {
                        PokemonGoesHere.SetText("Place 16 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 16)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (greatball.PokemonNameGreat == "Gastly")
                    {
                        PokemonGoesHere.SetText("Place 20 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 20)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (greatball.PokemonNameGreat == "Haunter")
                    {
                        PokemonGoesHere.SetText("Place 10 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 10)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else
                    {
                        PokemonGoesHere.SetText("This Pokémon cannot evolve!");
                    }
                }
                if (partyslot1.Item.modItem is UltraBallCaught ultraball)
                {
                    if (ultraball.PokemonNameUltra == "Bulbasaur" || ultraball.PokemonNameUltra == "Charmander" || ultraball.PokemonNameUltra == "Squirtle")
                    {
                        PokemonGoesHere.SetText("Place 11 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 11)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    if (ultraball.PokemonNameUltra == "Wartortle" || ultraball.PokemonNameUltra == "Charmeleon")
                    {
                        PokemonGoesHere.SetText("Place 20 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 20)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    if (ultraball.PokemonNameUltra == "Ivysaur")
                    {
                        PokemonGoesHere.SetText("Place 16 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 16)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (ultraball.PokemonNameUltra == "Gastly")
                    {
                        PokemonGoesHere.SetText("Place 20 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 20)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (ultraball.PokemonNameUltra == "Haunter")
                    {
                        PokemonGoesHere.SetText("Place 10 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 10)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else
                    {
                        PokemonGoesHere.SetText("This Pokémon cannot evolve!");
                    }
                }
                if (partyslot1.Item.modItem is DuskBallCaught duskball)
                {
                    if (duskball.PokemonNameDusk == "Bulbasaur" || duskball.PokemonNameDusk == "Charmander" || duskball.PokemonNameDusk == "Squirtle")
                    {
                        PokemonGoesHere.SetText("Place 11 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 11)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    if (duskball.PokemonNameDusk == "Wartortle" || duskball.PokemonNameDusk == "Charmeleon")
                    {
                        PokemonGoesHere.SetText("Place 20 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 20)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    if (duskball.PokemonNameDusk == "Ivysaur")
                    {
                        PokemonGoesHere.SetText("Place 16 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 16)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (duskball.PokemonNameDusk == "Gastly")
                    {
                        PokemonGoesHere.SetText("Place 20 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 20)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (duskball.PokemonNameDusk == "Haunter")
                    {
                        PokemonGoesHere.SetText("Place 10 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 10)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else
                    {
                        PokemonGoesHere.SetText("This Pokémon cannot evolve!");
                    }
                }
                if (partyslot1.Item.modItem is PremierBallCaught premierball)
                {
                    if (premierball.PokemonNamePremier == "Bulbasaur" || premierball.PokemonNamePremier == "Charmander" || premierball.PokemonNamePremier == "Squirtle")
                    {
                        PokemonGoesHere.SetText("Place 11 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 11)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    if (premierball.PokemonNamePremier == "Wartortle" || premierball.PokemonNamePremier == "Charmeleon")
                    {
                        PokemonGoesHere.SetText("Place 20 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 20)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    if (premierball.PokemonNamePremier == "Ivysaur")
                    {
                        PokemonGoesHere.SetText("Place 16 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 16)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (premierball.PokemonNamePremier == "Gastly")
                    {
                        PokemonGoesHere.SetText("Place 20 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 20)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (premierball.PokemonNamePremier == "Haunter")
                    {
                        PokemonGoesHere.SetText("Place 10 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 10)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else
                    {
                        PokemonGoesHere.SetText("This Pokémon cannot evolve!");
                    }
                }
            }
        }
        private void EvolveButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (partyslot1.Item.modItem is PokeballCaught pokeball)
            {
                if (pokeball.PokemonName == "Bulbasaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<IvysaurNPC>();
                    (Main.item[index].modItem as PokeballCaught).PokemonName = "Ivysaur";
                    (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniIvysaur";
                    Main.playerInventory = false;
                    Main.NewText("Your Bulbasaur evolved into Ivysaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Charmander")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<CharmeleonNPC>();
                    (Main.item[index].modItem as PokeballCaught).PokemonName = "Charmeleon";
                    (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmeleon";
                    Main.playerInventory = false;
                    Main.NewText("Your Charmander evolved into Charmeleon!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Squirtle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<WartortleNPC>();
                    (Main.item[index].modItem as PokeballCaught).PokemonName = "Wartortle";
                    (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniWartortle";
                    Main.playerInventory = false;
                    Main.NewText("Your Squirtle evolved into Wartortle!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Ivysaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<VenusaurNPC>();
                    (Main.item[index].modItem as PokeballCaught).PokemonName = "Venusaur";
                    (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniVenusaur";
                    Main.playerInventory = false;
                    Main.NewText("Your Ivysaur evolved into Venusaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Charmeleon")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<CharizardNPC>();
                    (Main.item[index].modItem as PokeballCaught).PokemonName = "Charizard";
                    (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharizard";
                    Main.playerInventory = false;
                    Main.NewText("Your Charmeleon evolved into Charizard!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Wartortle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<BlastoiseNPC>();
                    (Main.item[index].modItem as PokeballCaught).PokemonName = "Blastoise";
                    (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBlastoise";
                    Main.playerInventory = false;
                    Main.NewText("Your Wartortle evolved into Blastoise!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Gastly")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<HaunterNPC>();
                    (Main.item[index].modItem as PokeballCaught).PokemonName = "Haunter";
                    (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniHaunter";
                    Main.playerInventory = false;
                    Main.NewText("Your Gastly evolved into Haunter!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Haunter")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<GengarNPC>();
                    (Main.item[index].modItem as PokeballCaught).PokemonName = "Gengar";
                    (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniGengar";
                    Main.playerInventory = false;
                    Main.NewText("Your Haunter evolved into Gengar!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
            }
            if (partyslot1.Item.modItem is GreatBallCaught greatball)
            {
                if (greatball.PokemonNameGreat == "Bulbasaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<GreatBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as GreatBallCaught).PokemonNPCGreat = ModContent.NPCType<IvysaurNPC>();
                    (Main.item[index].modItem as GreatBallCaught).PokemonNameGreat = "Ivysaur";
                    (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniIvysaur";
                    Main.playerInventory = false;
                    Main.NewText("Your Bulbasaur evolved into Ivysaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (greatball.PokemonNameGreat == "Charmander")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<GreatBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as GreatBallCaught).PokemonNPCGreat = ModContent.NPCType<CharmeleonNPC>();
                    (Main.item[index].modItem as GreatBallCaught).PokemonNameGreat = "Charmeleon";
                    (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmeleon";
                    Main.playerInventory = false;
                    Main.NewText("Your Charmander evolved into Charmeleon!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (greatball.PokemonNameGreat == "Squirtle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<GreatBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as GreatBallCaught).PokemonNPCGreat = ModContent.NPCType<WartortleNPC>();
                    (Main.item[index].modItem as GreatBallCaught).PokemonNameGreat = "Wartortle";
                    (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniWartortle";
                    Main.playerInventory = false;
                    Main.NewText("Your Squirtle evolved into Wartortle!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (greatball.PokemonNameGreat == "Gastly")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<GreatBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as GreatBallCaught).PokemonNPCGreat = ModContent.NPCType<HaunterNPC>();
                    (Main.item[index].modItem as GreatBallCaught).PokemonNameGreat = "Haunter";
                    (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniHaunter";
                    Main.playerInventory = false;
                    Main.NewText("Your Gastly evolved into Haunter!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (greatball.PokemonNameGreat == "Haunter")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<GreatBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as GreatBallCaught).PokemonNPCGreat = ModContent.NPCType<GengarNPC>();
                    (Main.item[index].modItem as GreatBallCaught).PokemonNameGreat = "Gengar";
                    (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniGengar";
                    Main.playerInventory = false;
                    Main.NewText("Your Haunter evolved into Gengar!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
            }
            if (partyslot1.Item.modItem is UltraBallCaught ultraball)
            {
                if (ultraball.PokemonNameUltra == "Bulbasaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<UltraBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as UltraBallCaught).PokemonNPCUltra = ModContent.NPCType<IvysaurNPC>();
                    (Main.item[index].modItem as UltraBallCaught).PokemonNameUltra = "Ivysaur";
                    (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniIvysaur";
                    Main.playerInventory = false;
                    Main.NewText("Your Bulbasaur evolved into Ivysaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (ultraball.PokemonNameUltra == "Charmander")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<UltraBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as UltraBallCaught).PokemonNPCUltra = ModContent.NPCType<CharmeleonNPC>();
                    (Main.item[index].modItem as UltraBallCaught).PokemonNameUltra = "Charmeleon";
                    (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmeleon";
                    Main.playerInventory = false;
                    Main.NewText("Your Charmander evolved into Charmeleon!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (ultraball.PokemonNameUltra == "Squirtle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<UltraBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as UltraBallCaught).PokemonNPCUltra = ModContent.NPCType<WartortleNPC>();
                    (Main.item[index].modItem as UltraBallCaught).PokemonNameUltra = "Wartortle";
                    (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniWartortle";
                    Main.playerInventory = false;
                    Main.NewText("Your Squirtle evolved into Wartortle!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (ultraball.PokemonNameUltra == "Ivysaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<UltraBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as UltraBallCaught).PokemonNPCUltra = ModContent.NPCType<VenusaurNPC>();
                    (Main.item[index].modItem as UltraBallCaught).PokemonNameUltra = "Venusaur";
                    (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniVenusaur";
                    Main.playerInventory = false;
                    Main.NewText("Your Ivysaur evolved into Venusaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (ultraball.PokemonNameUltra == "Charmeleon")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<UltraBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as UltraBallCaught).PokemonNPCUltra = ModContent.NPCType<CharizardNPC>();
                    (Main.item[index].modItem as UltraBallCaught).PokemonNameUltra = "Charizard";
                    (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharizard";
                    Main.playerInventory = false;
                    Main.NewText("Your Charmeleon evolved into Charizard!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (ultraball.PokemonNameUltra == "Wartortle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<UltraBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as UltraBallCaught).PokemonNPCUltra = ModContent.NPCType<BlastoiseNPC>();
                    (Main.item[index].modItem as UltraBallCaught).PokemonNameUltra = "Blastoise";
                    (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBlastoise";
                    Main.playerInventory = false;
                    Main.NewText("Your Wartortle evolved into Blastoise!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (ultraball.PokemonNameUltra == "Gastly")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<UltraBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as UltraBallCaught).PokemonNPCUltra = ModContent.NPCType<HaunterNPC>();
                    (Main.item[index].modItem as UltraBallCaught).PokemonNameUltra = "Haunter";
                    (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniHaunter";
                    Main.playerInventory = false;
                    Main.NewText("Your Gastly evolved into Haunter!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (ultraball.PokemonNameUltra == "Haunter")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<UltraBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as UltraBallCaught).PokemonNPCUltra = ModContent.NPCType<GengarNPC>();
                    (Main.item[index].modItem as UltraBallCaught).PokemonNameUltra = "Gengar";
                    (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniGengar";
                    Main.playerInventory = false;
                    Main.NewText("Your Haunter evolved into Gengar!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
            }
            if (partyslot1.Item.modItem is DuskBallCaught duskball)
            {
                if (duskball.PokemonNameDusk == "Bulbasaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<DuskBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as DuskBallCaught).PokemonNPCDusk = ModContent.NPCType<IvysaurNPC>();
                    (Main.item[index].modItem as DuskBallCaught).PokemonNameDusk = "Ivysaur";
                    (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniIvysaur";
                    Main.playerInventory = false;
                    Main.NewText("Your Bulbasaur evolved into Ivysaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (duskball.PokemonNameDusk == "Charmander")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<DuskBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as DuskBallCaught).PokemonNPCDusk = ModContent.NPCType<CharmeleonNPC>();
                    (Main.item[index].modItem as DuskBallCaught).PokemonNameDusk = "Charmeleon";
                    (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmeleon";
                    Main.playerInventory = false;
                    Main.NewText("Your Charmander evolved into Charmeleon!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (duskball.PokemonNameDusk == "Squirtle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<DuskBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as DuskBallCaught).PokemonNPCDusk = ModContent.NPCType<WartortleNPC>();
                    (Main.item[index].modItem as DuskBallCaught).PokemonNameDusk = "Wartortle";
                    (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniWartortle";
                    Main.playerInventory = false;
                    Main.NewText("Your Squirtle evolved into Wartortle!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (duskball.PokemonNameDusk == "Ivysaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<DuskBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as DuskBallCaught).PokemonNPCDusk = ModContent.NPCType<VenusaurNPC>();
                    (Main.item[index].modItem as DuskBallCaught).PokemonNameDusk = "Venusaur";
                    (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniVenusaur";
                    Main.playerInventory = false;
                    Main.NewText("Your Ivysaur evolved into Venusaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (duskball.PokemonNameDusk == "Charmeleon")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<DuskBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as DuskBallCaught).PokemonNPCDusk = ModContent.NPCType<CharmeleonNPC>();
                    (Main.item[index].modItem as DuskBallCaught).PokemonNameDusk = "Charizard";
                    (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharizard";
                    Main.playerInventory = false;
                    Main.NewText("Your Charmeleon evolved into Charizard!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (duskball.PokemonNameDusk == "Wartortle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<DuskBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as DuskBallCaught).PokemonNPCDusk = ModContent.NPCType<BlastoiseNPC>();
                    (Main.item[index].modItem as DuskBallCaught).PokemonNameDusk = "Blastoise";
                    (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBlastoise";
                    Main.playerInventory = false;
                    Main.NewText("Your Wartortle evolved into Blastoise!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (duskball.PokemonNameDusk == "Gastly")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<DuskBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as DuskBallCaught).PokemonNPCDusk = ModContent.NPCType<HaunterNPC>();
                    (Main.item[index].modItem as DuskBallCaught).PokemonNameDusk = "Haunter";
                    (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniHaunter";
                    Main.playerInventory = false;
                    Main.NewText("Your Gastly evolved into Haunter!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (duskball.PokemonNameDusk == "Haunter")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<DuskBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as DuskBallCaught).PokemonNPCDusk = ModContent.NPCType<GengarNPC>();
                    (Main.item[index].modItem as DuskBallCaught).PokemonNameDusk = "Gengar";
                    (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniGengar";
                    Main.playerInventory = false;
                    Main.NewText("Your Haunter evolved into Gengar!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
            }
            if (partyslot1.Item.modItem is PremierBallCaught premierball)
            {
                if (premierball.PokemonNamePremier == "Bulbasaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PremierBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PremierBallCaught).PokemonNPCPremier = ModContent.NPCType<IvysaurNPC>();
                    (Main.item[index].modItem as PremierBallCaught).PokemonNamePremier = "Ivysaur";
                    (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniIvysaur";
                    Main.playerInventory = false;
                    Main.NewText("Your Bulbasaur evolved into Ivysaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (premierball.PokemonNamePremier == "Charmander")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PremierBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PremierBallCaught).PokemonNPCPremier = ModContent.NPCType<CharmeleonNPC>();
                    (Main.item[index].modItem as PremierBallCaught).PokemonNamePremier = "Charmeleon";
                    (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmeleon";
                    Main.playerInventory = false;
                    Main.NewText("Your Charmander evolved into Charmeleon!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (premierball.PokemonNamePremier == "Squirtle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PremierBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PremierBallCaught).PokemonNPCPremier = ModContent.NPCType<WartortleNPC>();
                    (Main.item[index].modItem as PremierBallCaught).PokemonNamePremier = "Wartortle";
                    (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniWartortle";
                    Main.playerInventory = false;
                    Main.NewText("Your Squirtle evolved into Wartortle!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (premierball.PokemonNamePremier == "Ivysaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PremierBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PremierBallCaught).PokemonNPCPremier = ModContent.NPCType<VenusaurNPC>();
                    (Main.item[index].modItem as PremierBallCaught).PokemonNamePremier = "Venusaur";
                    (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniVenusaur";
                    Main.playerInventory = false;
                    Main.NewText("Your Ivysaur evolved into Venusaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (premierball.PokemonNamePremier == "Charmeleon")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PremierBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PremierBallCaught).PokemonNPCPremier = ModContent.NPCType<CharizardNPC>();
                    (Main.item[index].modItem as PremierBallCaught).PokemonNamePremier = "Charizard";
                    (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharizard";
                    Main.playerInventory = false;
                    Main.NewText("Your Charmeleon evolved into Charizard!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (premierball.PokemonNamePremier == "Wartortle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PremierBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PremierBallCaught).PokemonNPCPremier = ModContent.NPCType<BlastoiseNPC>();
                    (Main.item[index].modItem as PremierBallCaught).PokemonNamePremier = "Blastoise";
                    (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBlastoise";
                    Main.playerInventory = false;
                    Main.NewText("Your Wartortle evolved into Blastoise!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (premierball.PokemonNamePremier == "Gastly")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PremierBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PremierBallCaught).PokemonNPCPremier = ModContent.NPCType<HaunterNPC>();
                    (Main.item[index].modItem as PremierBallCaught).PokemonNamePremier = "Haunter";
                    (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniHaunter";
                    Main.playerInventory = false;
                    Main.NewText("Your Gastly evolved into Haunter!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (premierball.PokemonNamePremier == "Haunter")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), ModContent.ItemType<PremierBallCaught>());
                    if (index >= 400)
                        return;
                    (Main.item[index].modItem as PremierBallCaught).PokemonNPCPremier = ModContent.NPCType<GengarNPC>();
                    (Main.item[index].modItem as PremierBallCaught).PokemonNamePremier = "Gengar";
                    (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniGengar";
                    Main.playerInventory = false;
                    Main.NewText("Your Haunter evolved into Gengar!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
            }
            Visible = false;
            partyslot1.Item.TurnToAir();
            partyslot2.Item.TurnToAir();
            mainPanel.RemoveChild(SaveButton);
            Main.LocalPlayer.talkNPC = 0;
        }
    }
}
