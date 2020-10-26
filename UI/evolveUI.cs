using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Razorwing.Framework.Localisation;
using Terramon.Items.Evolution;
using Terramon.Items.MiscItems.LinkCables;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Network.Catching;
using Terramon.Pokemon;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
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

        public ILocalisedBindableString placeMonString =
            TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("evolveUI.placemon", "Place a Pokémon in the first slot.")));
        public ILocalisedBindableString placeCandyText = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("evolveUI.placeItem", "Place {0} {1} in the second slot.")));
        public ILocalisedBindableString pressEvolveText = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("evolveUI.pressEvolve", "Great! Press the evolve button!")));
        public ILocalisedBindableString cannotEvolveText = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("evolveUI.cannotEvolve", "This Pokémon cannot evolve!")));
        public ILocalisedBindableString rareCandyText = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("rareCandy", "Rare Candy")));
        public ILocalisedBindableString linkCableText = TerramonMod.Localisation.GetLocalisedString(new LocalisedString(("linkCable","Link Cable")));


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

            partyslot1 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem);
            partyslot1.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot1.HAlign = 0.15f;
            partyslot1.VAlign = 0.5f;
            partyslot1.ValidItemFunc = item => item.IsAir || TerramonMod.PokeballFactory.GetEnum(item.modItem) !=
                                               TerramonMod.PokeballFactory.Pokebals.Nothing;
            mainPanel.Append(partyslot1);

            partyslot2 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem);
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
            //PokemonGoesHere.SetText("Place a Pokémon in the first slot.");
            PokemonGoesHere.SetText(placeMonString.Value);
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
            SaveButton.OnClick += EvolveButtonClicked;

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
                //PokemonGoesHere.SetText("Place a Pokémon in the first slot.");
                PokemonGoesHere.SetText(placeMonString.Value);
                mainPanel.RemoveChild(partyslot2);
            }
            else
            {
                if (partyslot1.Item.modItem is BaseCaughtClass pokeball)
                {
                    var mon = TerramonMod.GetPokemon(pokeball.CapturedPokemon);
                    if (mon != null && mon.CanEvolve)
                    {
                        if (mon.EvolveItem == EvolveItem.RareCandy)
                        {
                            //Set preference to RareCandy
                            partyslot2.ValidItemFunc = item => item.IsAir || item.modItem is RareCandy;
                            //PokemonGoesHere.SetText($"Place {mon.EvolveCost} Rare Candies in the second slot.");
                            placeCandyText.Args = new object[] {mon.EvolveCost, rareCandyText.Value};
                            PokemonGoesHere.SetText(placeCandyText.Value);
                            mainPanel.Append(partyslot2);
                            if (!partyslot2.Item.IsAir && partyslot2.Item.modItem is RareCandy &&
                                partyslot2.Item.stack == mon.EvolveCost)
                            {
                                //PokemonGoesHere.SetText("Great! Press the evolve button!");
                                PokemonGoesHere.SetText(pressEvolveText.Value);

                                mainPanel.Append(SaveButton);
                            }
                            else
                            {
                                mainPanel.RemoveChild(SaveButton);
                            }
                        }
                        else if (mon.EvolveItem == EvolveItem.LinkCable)
                        {
                            //Set preference to LinkCable
                            partyslot2.ValidItemFunc = item => item.IsAir || item.modItem is LinkCable;

                            //PokemonGoesHere.SetText($"Place {mon.EvolveCost} Link Cable in the second slot.");

                            placeCandyText.Args = new object[] { mon.EvolveCost, linkCableText.Value };
                            PokemonGoesHere.SetText(placeCandyText.Value);
                            mainPanel.Append(partyslot2);
                            if (!partyslot2.Item.IsAir && partyslot2.Item.modItem is LinkCable &&
                                partyslot2.Item.stack == mon.EvolveCost)
                            {
                                //PokemonGoesHere.SetText("Great! Press the evolve button!");
                                PokemonGoesHere.SetText(pressEvolveText.Value);

                                mainPanel.Append(SaveButton);
                            }
                            else
                            {
                                mainPanel.RemoveChild(SaveButton);
                            }
                        }
                    }
                    else
                    {
                        PokemonGoesHere.SetText(cannotEvolveText.Value);
                    }
                }
            }
        }

        private void EvolveButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            int whicheverballtype = TerramonMod.PokeballFactory.GetPokeballType(partyslot1.Item.modItem);

            // stuff break
            if (partyslot1.Item.modItem is BaseCaughtClass pokeball)
            {
                var mon = TerramonMod.GetPokemon(pokeball.CapturedPokemon);
                if (mon == null)
                {
                    Main.NewText(cannotEvolveText.Value);
                    return;
                }

                var evolved = TerramonMod.GetPokemon(mon.EvolveTo.Name);
                if (evolved == null)
                {
                    Main.NewText(cannotEvolveText.Value);
                    return;
                }

                pokeball.PokeData.Pokemon = evolved.GetType().Name;

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    BaseCatchPacket packet = new BaseCatchPacket();
                    packet.Send(TerramonMod.Instance, evolved.GetType().Name, evolved.GetType().Name,
                        Main.LocalPlayer.getRect(), whicheverballtype, pokeball.PokeData);
                }
                else
                {
                    int index = Item.NewItem(Main.LocalPlayer.getRect(), whicheverballtype);
                    if (index >= 400)
                        return;
                    if (Main.item[index].modItem is BaseCaughtClass item)
                    {
                        item.PokemonName = evolved.GetType().Name;
                        item.CapturedPokemon = evolved.GetType().Name;
                        item.PokeData = pokeball.PokeData;
                    }
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
