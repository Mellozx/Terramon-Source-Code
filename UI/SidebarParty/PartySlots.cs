using Microsoft.Xna.Framework.Graphics;
using Terramon.Items.Pokeballs.Inventory;
using Terramon.Players;
using Terramon.Pokemon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
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
            mainPanel.Height.Set(150f, 0f);

            partyslot1 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem);
            partyslot1.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot1.HAlign = 0.10f;
            partyslot1.VAlign = 0.15f;
            partyslot1.ValidItemFunc = item => item.IsAir || TerramonMod.PokeballFactory.GetEnum(item.modItem) !=
                                               TerramonMod.PokeballFactory.Pokebals.Nothing;
            mainPanel.Append(partyslot1);

            partyslot2 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem);
            partyslot2.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot2.HAlign = 0.5f;
            partyslot2.VAlign = 0.15f;
            partyslot2.ValidItemFunc = item => item.IsAir || TerramonMod.PokeballFactory.GetEnum(item.modItem) !=
                                               TerramonMod.PokeballFactory.Pokebals.Nothing;
            mainPanel.Append(partyslot2);

            partyslot3 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem);
            partyslot3.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot3.HAlign = 0.90f;
            partyslot3.VAlign = 0.15f;
            partyslot3.ValidItemFunc = item => item.IsAir || TerramonMod.PokeballFactory.GetEnum(item.modItem) !=
                                               TerramonMod.PokeballFactory.Pokebals.Nothing;
            mainPanel.Append(partyslot3);

            partyslot4 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem);
            partyslot4.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot4.HAlign = 0.10f;
            partyslot4.VAlign = 0.85f;
            partyslot4.ValidItemFunc = item => item.IsAir || TerramonMod.PokeballFactory.GetEnum(item.modItem) !=
                                               TerramonMod.PokeballFactory.Pokebals.Nothing;
            mainPanel.Append(partyslot4);

            partyslot5 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem);
            partyslot5.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot5.HAlign = 0.5f;
            partyslot5.VAlign = 0.85f;
            partyslot5.ValidItemFunc = item => item.IsAir || TerramonMod.PokeballFactory.GetEnum(item.modItem) !=
                                               TerramonMod.PokeballFactory.Pokebals.Nothing;
            mainPanel.Append(partyslot5);

            partyslot6 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem);
            partyslot6.SetPadding(0);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(mainPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            partyslot6.HAlign = 0.90f;
            partyslot6.VAlign = 0.85f;
            partyslot6.ValidItemFunc = item => item.IsAir || TerramonMod.PokeballFactory.GetEnum(item.modItem) !=
                                               TerramonMod.PokeballFactory.Pokebals.Nothing;
            mainPanel.Append(partyslot6);

            Append(mainPanel);

            partyslot1.OnItemPlaced += UpdateUI;
            partyslot2.OnItemPlaced += UpdateUI;
            partyslot3.OnItemPlaced += UpdateUI;
            partyslot4.OnItemPlaced += UpdateUI;
            partyslot5.OnItemPlaced += UpdateUI;
            partyslot6.OnItemPlaced += UpdateUI;
        }

        internal void UpdateUI(Item item)
        {
            SaveButtonClicked();
        }

        //private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        //{
        //    Main.PlaySound(SoundID.MenuOpen);
        //    ModContent.GetInstance<TerramonMod>().UISidebar.CycleIndex = 0;
        //    TerramonPlayer modPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
        //    Mod mod = ModContent.GetInstance<TerramonMod>();
        //    Player player = Main.LocalPlayer;
        //    // clear buffs
        //    player.ClearBuff(mod.BuffType(modPlayer.firstslotname + "Buff"));
        //    player.ClearBuff(mod.BuffType(modPlayer.secondslotname + "Buff"));
        //    player.ClearBuff(mod.BuffType(modPlayer.thirdslotname + "Buff"));
        //    player.ClearBuff(mod.BuffType(modPlayer.fourthslotname + "Buff"));
        //    player.ClearBuff(mod.BuffType(modPlayer.fifthslotname + "Buff"));
        //    player.ClearBuff(mod.BuffType(modPlayer.sixthslotname + "Buff"));
        //    if (partyslot1.Item.IsAir)
        //    {
        //        modPlayer.firstslotname = "*";
        //        modPlayer.PartySlot1 = null;
        //        ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn
        //            .SetImage(ModContent.GetTexture("Terraria/Item_0"));
        //        ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.HoverText = "";
        //        ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.Recalculate();
        //    }

        //    if (partyslot2.Item.IsAir)
        //    {
        //        modPlayer.secondslotname = "*";
        //        modPlayer.PartySlot2 = null;
        //        ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn
        //            .SetImage(ModContent.GetTexture("Terraria/Item_0"));
        //        ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.HoverText = "";
        //        ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.Recalculate();
        //    }

        //    if (partyslot3.Item.IsAir)
        //    {
        //        modPlayer.thirdslotname = "*";
        //        modPlayer.PartySlot3 = null;
        //        ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn
        //            .SetImage(ModContent.GetTexture("Terraria/Item_0"));
        //        ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.HoverText = "";
        //        ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.Recalculate();
        //    }

        //    if (partyslot4.Item.IsAir)
        //    {
        //        modPlayer.fourthslotname = "*";
        //        modPlayer.PartySlot4 = null;
        //        ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn
        //            .SetImage(ModContent.GetTexture("Terraria/Item_0"));
        //        ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.HoverText = "";
        //        ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.Recalculate();
        //    }

        //    if (partyslot5.Item.IsAir)
        //    {
        //        modPlayer.fifthslotname = "*";
        //        modPlayer.PartySlot5 = null;
        //        ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn
        //            .SetImage(ModContent.GetTexture("Terraria/Item_0"));
        //        ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.HoverText = "";
        //        ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.Recalculate();
        //    }

        //    if (partyslot6.Item.IsAir)
        //    {
        //        modPlayer.sixthslotname = "*";
        //        modPlayer.PartySlot6 = null;
        //        ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn
        //            .SetImage(ModContent.GetTexture("Terraria/Item_0"));
        //        ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.HoverText = "";
        //        ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.Recalculate();
        //    }

        //    Main.NewText("Party Cleared.");
        //}

        private void SaveButtonClicked()
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
                //old_1 = "*";
                modPlayer.PartySlot1 = null;
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn
                    .SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.firstpkmn.Recalculate();
            }

            if (partyslot2.Item.IsAir)
            {
                modPlayer.secondslotname = "*";
                //old_2 = "*";
                modPlayer.PartySlot2 = null;
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn
                    .SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.secondpkmn.Recalculate();
            }

            if (partyslot3.Item.IsAir)
            {
                //old_3 = "*";
                modPlayer.thirdslotname = "*";
                modPlayer.PartySlot3 = null;
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn
                    .SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.thirdpkmn.Recalculate();
            }

            if (partyslot4.Item.IsAir)
            {
                //old_4 = "*";
                modPlayer.fourthslotname = "*";
                modPlayer.PartySlot4 = null;
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn
                    .SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.fourthpkmn.Recalculate();
            }

            if (partyslot5.Item.IsAir)
            {
                //old_5 = "*";
                modPlayer.fifthslotname = "*";
                modPlayer.PartySlot5 = null;
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn
                    .SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.fifthpkmn.Recalculate();
            }

            if (partyslot6.Item.IsAir)
            {
                //old_6 = "*";
                modPlayer.sixthslotname = "*";
                modPlayer.PartySlot6 = null;
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn
                    .SetImage(ModContent.GetTexture("Terraria/Item_0"));
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.HoverText = "";
                ModContent.GetInstance<TerramonMod>().UISidebar.sixthpkmn.Recalculate();
            }

            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot1.Item.IsAir)
            {
                //var type = TerramonMod.GetPokeballType(partyslot1.Item.modItem);
                var pokeballCaught = (BaseCaughtClass) partyslot1.Item.modItem;
                //modPlayer.firstslottype = pokeballCaught.PokemonNPC;
                //old_1 = pokeballCaught.PokemonName ;
                modPlayer.firstslotname = pokeballCaught.PokemonName;
                modPlayer.PartySlot1 =new PokemonData(pokeballCaught.Save());
            }

            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot2.Item.IsAir)
            {
                //var type = TerramonMod.GetPokeballType(partyslot1.Item.modItem);
                var pokeballCaught = (BaseCaughtClass) partyslot2.Item.modItem;
                //modPlayer.secondslottype = pokeballCaught.PokemonNPC;
                //old_2 = pokeballCaught.PokemonName;
                modPlayer.secondslotname = pokeballCaught.PokemonName;
                modPlayer.PartySlot2 =new PokemonData(pokeballCaught.Save());
            }

            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot3.Item.IsAir)
            {
                //var type = TerramonMod.GetPokeballType(partyslot1.Item.modItem);
                var pokeballCaught = (BaseCaughtClass) partyslot3.Item.modItem;
                //modPlayer.thirdslottype = pokeballCaught.PokemonNPC;
                //old_3 = pokeballCaught.PokemonName;
                modPlayer.thirdslotname = pokeballCaught.PokemonName;
                modPlayer.PartySlot3 =new PokemonData(pokeballCaught.Save());
            }

            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot4.Item.IsAir)
            {
                //var type = TerramonMod.GetPokeballType(partyslot1.Item.modItem);
                var pokeballCaught = (BaseCaughtClass) partyslot4.Item.modItem;
                //modPlayer.fourthslottype = pokeballCaught.PokemonNPC;
                //old_4 = pokeballCaught.PokemonName;
                modPlayer.fourthslotname = pokeballCaught.PokemonName;
                modPlayer.PartySlot4 =new PokemonData(pokeballCaught.Save());
            }

            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot5.Item.IsAir)
            {
                //var type = TerramonMod.GetPokeballType(partyslot1.Item.modItem);
                var pokeballCaught = (BaseCaughtClass) partyslot5.Item.modItem;
                //modPlayer.fifthslottype = pokeballCaught.PokemonNPC;
                //old_5 = pokeballCaught.PokemonName;
                modPlayer.fifthslotname = pokeballCaught.PokemonName;
                modPlayer.PartySlot5 =new PokemonData(pokeballCaught.Save());
            }

            if (!ModContent.GetInstance<TerramonMod>().PartySlots.partyslot6.Item.IsAir)
            {
                //var type = TerramonMod.GetPokeballType(partyslot1.Item.modItem);
                var pokeballCaught = (BaseCaughtClass) partyslot6.Item.modItem;
                //modPlayer.sixthslottype = pokeballCaught.PokemonNPC;
                //old_6 = pokeballCaught.PokemonName;
                modPlayer.sixthslotname = pokeballCaught.PokemonName;
                modPlayer.PartySlot6 =new PokemonData(pokeballCaught.Save());
            }

            //Main.NewText("Party Saved!");
        }
    }
}