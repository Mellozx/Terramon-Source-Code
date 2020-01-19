using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terramon.Pokemon.FirstGeneration.Normal.Squirtle;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Network.Catching
{
    public class BaseCatchPacket : Packet
    {
        public const string NAME = "net_basecat";
        public override string PacketName => NAME;

        public void Send(TerramonMod mod, int type, string name, string icon, Rectangle rect, int pokeType)
        {
            try
            {
                var packet = GetPacket(mod);
                packet.Write(type);
                packet.Write(name);
                packet.Write(icon);
                packet.Write(rect.X);
                packet.Write(rect.Y);
                packet.Write(rect.Width);
                packet.Write(rect.Height);
                packet.Write(pokeType);
                packet.Send(256);
            }
            catch (Exception e)
            {
                mod.Logger.ErrorFormat("Please report this stacktrace to Terramon devs:\n\n{0}\n\n{1}", e.Message, e.StackTrace);
            }
        }

        public override void HandleFromClient(BinaryReader r, int whoAmI)
        {
            try
            {
                if (!Main.player[whoAmI].active)
                    return;

                PokeballCaught.det_PokemonNPC = r.ReadInt32();
                PokeballCaught.det_PokemonName = r.ReadString();
                PokeballCaught.det_SmallSpritePath = r.ReadString();

                var rect = new Rectangle(r.ReadInt32(), r.ReadInt32(), r.ReadInt32(), r.ReadInt32());

                int index = Item.NewItem(rect, r.ReadInt32());

                if (index >= 400 || !(Main.item[index].modItem is PokeballCaught modItem))
                    return;
            }
            catch (Exception e)
            {
                TerramonMod.Instance.Logger.ErrorFormat("Please report this stacktrace to Terramon devs:\n\n{0}\n\n{1}", e.Message, e.StackTrace);
            }
        }

    }
}
