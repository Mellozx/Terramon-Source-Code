using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terramon.Pokemon.FirstGeneration.Normal.Bulbasaur;
using Terramon.Pokemon.FirstGeneration.Normal.Charmander;
using Terramon.Pokemon.FirstGeneration.Normal.Squirtle;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Network.Starter
{
    public class SpawnStarterPacket : Packet
    {

        public const string NAME = "net_packet";
        public override string PacketName => NAME;

        public const string SQUIRTLE = "squ";
        public const string CHARMANDER = "cha";
        public const string BULBASAUR = "bul";

        public void Send(TerramonMod mod, string mon)
        {
            var packet = GetPacket(mod);
            packet.Write(mon);
            packet.Send(256);
        }

        public void HandleFromClient(BinaryReader reader, int whoAmI)
        {
            var data = reader.ReadString();
            switch (data)
            {
                case SQUIRTLE:
                {
                    if (!Main.player[whoAmI].active)
                        return;

                    PokeballCaught.det_PokemonNPC = ModContent.NPCType<SquirtleNPC>();
                    PokeballCaught.det_PokemonName = "Squirtle";
                    PokeballCaught.det_SmallSpritePath = "Terramon/Minisprites/Regular/miniSquirtle";
                    int index = Item.NewItem(Main.player[whoAmI].getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400 || !(Main.item[index].modItem is PokeballCaught modItem))
                        return;

                    
                }
                    break;
                case CHARMANDER:
                {
                    PokeballCaught.det_PokemonNPC = ModContent.NPCType<CharmanderNPC>();
                    PokeballCaught.det_PokemonName = "Charmander";
                    PokeballCaught.det_SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmander";
                        int index = Item.NewItem(Main.player[whoAmI].getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400 || !(Main.item[index].modItem is PokeballCaught modItem))
                        return;

                    //modItem.PokemonNPC = ModContent.NPCType<CharmanderNPC>();
                    //modItem.PokemonName = "Charmander";
                    //modItem.SmallSpritePath = "Terramon/Minisprites/Regular/miniCharmander";
                }
                    break;
                case BULBASAUR:
                {
                    PokeballCaught.det_PokemonNPC = ModContent.NPCType<BulbasaurNPC>();
                    PokeballCaught.det_PokemonName = "Bulbasaur";
                    PokeballCaught.det_SmallSpritePath = "Terramon/Minisprites/Regular/miniBulbasaur";
                        int index = Item.NewItem(Main.player[whoAmI].getRect(), ModContent.ItemType<PokeballCaught>());
                    if (index >= 400 || !(Main.item[index].modItem is PokeballCaught modItem))
                        return;

                    //modItem.PokemonNPC = ModContent.NPCType<BulbasaurNPC>();
                    //modItem.PokemonName = "Bulbasaur";
                    //modItem.SmallSpritePath = "Terramon/Minisprites/Regular/miniBulbasaur";
                }
                    break;
            }
            
        }

        public void HandleFromServer(BinaryReader reader) { return; }
    }
}
