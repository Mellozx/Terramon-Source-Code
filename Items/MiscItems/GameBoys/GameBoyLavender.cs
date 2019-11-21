using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items.MiscItems.GameBoys
{
    public class GameBoyLavender : GameBoy
    {
        public const string TOOLTIP = "It's an 8-bit handheld console." +
                                      "\n[c/33ceff:Equip this to listen to some eerily exclusive music...]" +
                                      "\n[c/FFFF66:Soundtrack: LAVENDER TOWN]";


        public GameBoyLavender() : base("Lavender", TOOLTIP, Item.sellPrice(gold: 6, silver: 6, copper: 6), ItemRarityID.Orange, "LAVENDER")
        {
        }
    }
}
