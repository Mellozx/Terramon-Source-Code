using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Weedle
{
    public class WeedleNPC : ParentPokemonNPC
    {
        public override Type HomeClass() => typeof(Weedle);

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
            if (spawnInfo.player.ZoneOverworldHeight)
            {
                return 0.08f;
            }
            else
            {
                return 0f;
            }
        }
    }
}