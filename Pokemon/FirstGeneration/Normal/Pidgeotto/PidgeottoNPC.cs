using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Pidgeotto
{
    public class PidgeottoNPC : ParentPokemonNPCFlyingBird
    {
        public override Type HomeClass()
        {
            return typeof(Pidgeotto);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 30;
            npc.height = 28;
            npc.scale = 1f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.gfxOffY = -4;
            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.ZoneOverworldHeight)
                return 0.045f;
            return 0f;
        }
    }
}