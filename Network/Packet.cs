using System.Diagnostics.Eventing.Reader;
using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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

        public static TagCompound ReadTagCompound(BinaryReader r)
        {
            var c = r.ReadInt32();
            var tag = new TagCompound();
            for (int i = 0; i < c; i++)
            {
                var t = r.ReadByte();
                switch (t)
                {
                    case 0://bool
                        tag.Add(r.ReadString(), r.ReadBoolean());
                        break;
                    case 1://int
                        tag.Add(r.ReadString(), r.ReadInt32());
                        break;
                    case 2://float
                        tag.Add(r.ReadString(), r.ReadSingle());
                        break;
                    case 3://double
                        tag.Add(r.ReadString(), r.ReadDouble());
                        break;
                    case 4://string
                        tag.Add(r.ReadString(), r.ReadString());
                        break;
                    case 5://byte
                        tag.Add(r.ReadString(), r.ReadByte());
                        break;
                }
            }

            return tag;
        }

        public static void WriteTagCompound(ModPacket r, TagCompound tag)
        {
            r.Write(tag.Count);
            foreach (var it in tag)
            {
                switch (it.Value)
                {
                    case bool b:
                        r.Write((byte)0);
                        r.Write(it.Key);
                        r.Write(b);
                        break;
                    case int i:
                        r.Write((byte)1);
                        r.Write(it.Key);
                        r.Write(i);
                        break;
                    case float f:
                        r.Write((byte)2);
                        r.Write(it.Key);
                        r.Write(f);
                        break;
                    case double d:
                        r.Write((byte)3);
                        r.Write(it.Key);
                        r.Write(d);
                        break;
                    case string s:
                        r.Write((byte)4);
                        r.Write(it.Key);
                        r.Write(s);
                        break;
                    case byte bt:
                        r.Write((byte)5);
                        r.Write(it.Key);
                        r.Write(bt);
                        break;
                }
            }
        }
    }
}