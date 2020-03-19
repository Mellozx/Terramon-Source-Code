using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Gastly
{
    public class GastlyNPC : ParentPokemonNPCFlying
    {
        public override Type HomeClass()
        {
            return typeof(Gastly);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
            npc.alpha = 75;

            npc.defense = 0;
            npc.lifeMax = 1;
            npc.knockBackResist = 0.5f;

            npc.value = 0f;
            npc.scale = 1.2f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/capturepokemon");

            npc.noGravity = true;
            npc.aiStyle = 65;
            aiType = NPCID.Firefly;

            animationType = NPCID.Bunny;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(PokeName());
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Bunny];
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.gfxOffY = -4;
            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            if (spawnInfo.player.ZoneDungeon)
                return 0.07f;
            return 0f;
        }
    }
}