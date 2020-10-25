using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.Originals.Normal.Parax
{
    public class ParaxNPC : ParentPokemonNPC
    { public override string Texture => "Terramon/Pokemon/Originals/Normal/Parax/Parax";
        public override Type HomeClass()
        {
            return typeof(Parax);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
            npc.scale = 1f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            if (Main.halloween && spawnInfo.player.ZoneOverworldHeight && Main.dayTime == false)
                return 0.03f;
            return 0f;
        }
    }
}
