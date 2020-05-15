using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Pokemon.FirstGeneration.Normal.Charmander
{
    public class CharmanderNPC : ParentPokemonNPC
    {
        public int timer;
        public int shinynum;
        public override Type HomeClass()
        {
            return typeof(Charmander);
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

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Width = 76 / 2;
            if (shiny)
            {
                npc.frame.X = 76 / 2;
            }
            else
            {
                npc.frame.X = 0;
            }
        }
        public override void AI()
        {
            Random rnd = new Random();
            timer++;
            if (timer == 1)
            {
                shinynum = rnd.Next(1, 1); // Shiny Chance, to-do later
                if (shinynum == 69420)
                {
                    shiny = true;
                }
                else
                {
                    shiny = false;
                }
            }
            if (Main.rand.Next(9) == 0)
                Dust.NewDust(npc.position, npc.width, npc.height, 55, 0f, 0f, 100, new Color(255, 148, 41));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.LocalPlayer;
            if (spawnInfo.player.ZoneRockLayerHeight)
                return 0.04f;
            return 0f;
        }
    }
}