using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Bulbasaur
{
    public class BulbasaurNPC : ParentPokemonNPC
    {
        public override Type HomeClass()
        {
            return typeof(Bulbasaur);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
            npc.scale = 1f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.gfxOffY = 6;
            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            if (spawnInfo.player.ZoneOverworldHeight && spawnInfo.player.ZoneJungle)
                return 0.04f;
            return 0f;
        }
    }
}