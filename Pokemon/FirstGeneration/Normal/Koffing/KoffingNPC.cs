using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Koffing
{
    public class KoffingNPC : ParentPokemonNPCFlying
    { public override string Texture => "Terramon/Pokemon/FirstGeneration/Normal/Koffing/Koffing";
        public override Type HomeClass()
        {
            return typeof(Koffing);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
            npc.scale = 1f;
        }

public static bool PlayerIsInForest(Player player){
	return !player.ZoneJungle
		&& !player.ZoneDungeon
		&& !player.ZoneCorrupt
		&& !player.ZoneCrimson
		&& !player.ZoneHoly
		&& !player.ZoneSnow
		&& !player.ZoneUndergroundDesert
		&& !player.ZoneGlowshroom
		&& !player.ZoneMeteor
		&& !player.ZoneBeach
		&& !player.ZoneDesert
		&& player.ZoneOverworldHeight;
}

public static bool PlayerIsInEvils(Player player){
	return player.ZoneCrimson
		|| player.ZoneCorrupt;
}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            if (PlayerIsInEvils(player))
                return 0.05f;
            return 0f;
        }
    }
}
