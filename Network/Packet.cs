using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Terramon.Network
{
    public abstract class Packet
    {
        public abstract string PacketName { get; }

        public ModPacket GetPacket(TerramonMod mod)
        {
            var packet = mod.GetPacket();
            packet.Write(PacketName);
            return packet;
        }

        public virtual void HandleFromClient(BinaryReader reader, int whoAmI)
        {

        }

        //We don't need whoAmI because server id all ways same and equal 256
        public virtual void HandleFromServer(BinaryReader reader)
        {

        }
    }
}
