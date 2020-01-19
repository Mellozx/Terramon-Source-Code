using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terramon.Items.MiscItems;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Players;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terramon.Pokemon.FirstGeneration.Normal.Beedrill;
using Terramon.Pokemon.FirstGeneration.Normal.Blastoise;
using Terramon.Pokemon.FirstGeneration.Normal.Bulbasaur;
using Terramon.Pokemon.FirstGeneration.Normal.Butterfree;
using Terramon.Pokemon.FirstGeneration.Normal.Charizard;
using Terramon.Pokemon.FirstGeneration.Normal.Charmeleon;
using Terramon.Pokemon.FirstGeneration.Normal.Dragonair;
using Terramon.Pokemon.FirstGeneration.Normal.Dragonite;
using Terramon.Pokemon.FirstGeneration.Normal.Gengar;
using Terramon.Pokemon.FirstGeneration.Normal.Haunter;
using Terramon.Pokemon.FirstGeneration.Normal.Ivysaur;
using Terramon.Pokemon.FirstGeneration.Normal.Kakuna;
using Terramon.Pokemon.FirstGeneration.Normal.Metapod;
using Terramon.Pokemon.FirstGeneration.Normal.Pidgeot;
using Terramon.Pokemon.FirstGeneration.Normal.Pidgeotto;
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
                if (partyslot1.Item.modItem is BaseCaughtClass pokeball)
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
                    else if (pokeball.PokemonName == "Caterpie")
                    {
                        PokemonGoesHere.SetText("Place 2 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 2)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (pokeball.PokemonName == "Metapod")
                    {
                        PokemonGoesHere.SetText("Place 3 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 3)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (pokeball.PokemonName == "Weedle")
                    {
                        PokemonGoesHere.SetText("Place 2 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 2)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (pokeball.PokemonName == "Beedrill")
                    {
                        PokemonGoesHere.SetText("Place 3 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 3)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (pokeball.PokemonName == "Pidgey")
                    {
                        PokemonGoesHere.SetText("Place 13 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 13)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (pokeball.PokemonName == "Pidgeotto")
                    {
                        PokemonGoesHere.SetText("Place 18 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 18)
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
                    else if (pokeball.PokemonName == "Dratini")
                    {
                        PokemonGoesHere.SetText("Place 25 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 25)
                        {
                            PokemonGoesHere.SetText("Great! Press the evolve button!");
                            mainPanel.Append(SaveButton);
                        }
                        else
                        {
                            mainPanel.RemoveChild(SaveButton);
                        }
                    }
                    else if (pokeball.PokemonName == "Dragonair")
                    {
                        PokemonGoesHere.SetText("Place 25 Rare Candies in the second slot.");
                        mainPanel.Append(partyslot2);
                        if (!partyslot2.Item.IsAir && partyslot2.Item.stack == 25)
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
            int whicheverballtype = 0;
            if (partyslot1.Item.modItem is PokeballCaught)
            {
                whicheverballtype = ModContent.ItemType<PokeballCaught>();
            }
            if (partyslot1.Item.modItem is GreatBallCaught)
            {
                whicheverballtype = ModContent.ItemType<GreatBallCaught>();
            }
            if (partyslot1.Item.modItem is UltraBallCaught)
            {
                whicheverballtype = ModContent.ItemType<UltraBallCaught>();
            }
            if (partyslot1.Item.modItem is DuskBallCaught)
            {
                whicheverballtype = ModContent.ItemType<DuskBallCaught>();
            }
            if (partyslot1.Item.modItem is PremierBallCaught)
            {
                whicheverballtype = ModContent.ItemType<PremierBallCaught>();
            }
            // stuff break
            if (partyslot1.Item.modItem is BaseCaughtClass pokeball)
            {
                if (pokeball.PokemonName == "Bulbasaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<IvysaurNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Ivysaur";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniIvysaur";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<IvysaurNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Ivysaur";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniIvysaur";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<IvysaurNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Ivysaur";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniIvysaur";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<IvysaurNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Ivysaur";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniIvysaur";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<IvysaurNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Ivysaur";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniIvysaur";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Bulbasaur evolved into Ivysaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Charmander")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<CharmeleonNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Charmeleon";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmeleon";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<CharmeleonNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Charmeleon";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmeleon";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<CharmeleonNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Charmeleon";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmeleon";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<CharmeleonNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Charmeleon";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmeleon";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<CharmeleonNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Charmeleon";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmeleon";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Charmander evolved into Charmeleon!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Squirtle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<WartortleNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Wartortle";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniWartortle";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<WartortleNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Wartortle";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniWartortle";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<WartortleNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Wartortle";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniWartortle";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<WartortleNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Wartortle";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniWartortle";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<WartortleNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Wartortle";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniWartortle";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Squirtle evolved into Wartortle!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Ivysaur")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<VenusaurNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Venusaur";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniVenusaur";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<VenusaurNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Venusaur";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniVenusaur";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<VenusaurNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Venusaur";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniVenusaur";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<VenusaurNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Venusaur";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniVenusaur";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<VenusaurNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Venusaur";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniVenusaur";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Ivysaur evolved into Venusaur!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Charmeleon")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<CharizardNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Charizard";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharizard";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<CharizardNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Charizard";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharizard";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<CharizardNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Charizard";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharizard";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<CharizardNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Charizard";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharizard";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<CharizardNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Charizard";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniCharizard";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Charmeleon evolved into Charizard!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Wartortle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<BlastoiseNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Blastoise";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBlastoise";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<BlastoiseNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Blastoise";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBlastoise";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<BlastoiseNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Blastoise";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBlastoise";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<BlastoiseNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Blastoise";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBlastoise";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<BlastoiseNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Blastoise";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBlastoise";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Wartortle evolved into Blastoise!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Caterpie")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<MetapodNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Metapod";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniMetapod";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<MetapodNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Metapod";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniMetapod";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<MetapodNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Metapod";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniMetapod";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<MetapodNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Metapod";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniMetapod";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<MetapodNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Metapod";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniMetapod";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Caterpie evolved into Metapod!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Metapod")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<ButterfreeNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Butterfree";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniButterfree";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<ButterfreeNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Butterfree";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniButterfree";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<ButterfreeNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Butterfree";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniButterfree";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<ButterfreeNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Butterfree";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniButterfree";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<ButterfreeNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Butterfree";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniButterfree";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Metapod evolved into Butterfree!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Weedle")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<KakunaNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Kakuna";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniKakuna";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<KakunaNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Kakuna";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniKakuna";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<KakunaNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Kakuna";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniKakuna";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<KakunaNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Kakuna";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniKakuna";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<KakunaNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Kakuna";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniKakuna";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Weedle evolved into Kakuna!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Kakuna")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<BeedrillNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Beedrill";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBeedrill";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<BeedrillNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Beedrill";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBeedrill";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<BeedrillNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Beedrill";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBeedrill";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<BeedrillNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Beedrill";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBeedrill";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<BeedrillNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Beedrill";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniBeedrill";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Kakuna evolved into Beedrill!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Pidgey")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<PidgeottoNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Pidgeotto";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniPidgeotto";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<PidgeottoNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Pidgeotto";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniPidgeotto";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<PidgeottoNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Pidgeotto";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniPidgeotto";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<PidgeottoNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Pidgeotto";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniPidgeotto";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<PidgeottoNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Pidgeotto";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniPidgeotto";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Pidgey evolved into Pidgeotto!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Pidgeotto")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<PidgeotNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Pidgeot";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniPidgeot";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<PidgeotNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Pidgeot";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniPidgeot";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<PidgeotNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Pidgeot";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniPidgeot";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<PidgeotNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Pidgeot";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniPidgeot";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<PidgeotNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Pidgeot";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniPidgeot";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Pidgeotto evolved into Pidgeot!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Gastly")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<HaunterNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Haunter";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniHaunter";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<HaunterNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Haunter";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniHaunter";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<HaunterNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Haunter";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniHaunter";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<HaunterNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Haunter";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniHaunter";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<HaunterNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Haunter";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniHaunter";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Gastly evolved into Haunter!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Haunter")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<GengarNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Gengar";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniGengar";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<GengarNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Gengar";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniGengar";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<GengarNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Gengar";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniGengar";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<GengarNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Gengar";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniGengar";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<GengarNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Gengar";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniGengar";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Haunter evolved into Gengar!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Dratini")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<DragonairNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Dragonair";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniDragonair";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<DragonairNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Dragonair";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniDragonair";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<DragonairNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Dragonair";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniDragonair";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<DragonairNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Dragonair";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniDragonair";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<DragonairNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Dragonair";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniDragonair";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Dratini evolved into Dragonair!");
                    Main.PlaySound(ModContent.GetInstance<TerramonMod>().GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/evolve"));
                }
                if (pokeball.PokemonName == "Dragonair")
                {
                    Player player = Main.LocalPlayer;
                    int index = Item.NewItem(player.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (whicheverballtype == ModContent.ItemType<PokeballCaught>())
                    {
                        (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<DragoniteNPC>();
                        (Main.item[index].modItem as PokeballCaught).PokemonName = "Dragonite";
                        (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniDragonite";
                    }
                    if (whicheverballtype == ModContent.ItemType<GreatBallCaught>())
                    {
                        (Main.item[index].modItem as GreatBallCaught).PokemonNPC = ModContent.NPCType<DragoniteNPC>();
                        (Main.item[index].modItem as GreatBallCaught).PokemonName = "Dragonite";
                        (Main.item[index].modItem as GreatBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniDragonite";
                    }
                    if (whicheverballtype == ModContent.ItemType<UltraBallCaught>())
                    {
                        (Main.item[index].modItem as UltraBallCaught).PokemonNPC = ModContent.NPCType<DragoniteNPC>();
                        (Main.item[index].modItem as UltraBallCaught).PokemonName = "Dragonite";
                        (Main.item[index].modItem as UltraBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniDragonite";
                    }
                    if (whicheverballtype == ModContent.ItemType<DuskBallCaught>())
                    {
                        (Main.item[index].modItem as DuskBallCaught).PokemonNPC = ModContent.NPCType<DragoniteNPC>();
                        (Main.item[index].modItem as DuskBallCaught).PokemonName = "Dragonite";
                        (Main.item[index].modItem as DuskBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniDragonite";
                    }
                    if (whicheverballtype == ModContent.ItemType<PremierBallCaught>())
                    {
                        (Main.item[index].modItem as PremierBallCaught).PokemonNPC = ModContent.NPCType<DragoniteNPC>();
                        (Main.item[index].modItem as PremierBallCaught).PokemonName = "Dragonite";
                        (Main.item[index].modItem as PremierBallCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniDragonite";
                    }
                    Main.playerInventory = false;
                    Main.NewText("Your Dragonair evolved into Dragonite!");
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
