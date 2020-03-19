using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Squirtle
{
    public class SquirtleNPC : ParentPokemonNPC
    {
        public override Type HomeClass()
        {
            return typeof(Squirtle);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.gfxOffY = 6;
            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            if (spawnInfo.player.ZoneRockLayerHeight && spawnInfo.player.ZoneSnow)
                return 0.04f;
            return 0f;
        }
    }
}