using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Charizard
{
    public class CharizardNPC : NotCatchablePKMN
    {
        public override Type HomeClass() => typeof(Charizard);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 24;
            npc.height = 24;
            npc.scale = 1f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.gfxOffY = 6;
            return true;
        }

        public override void AI()
        {
            if (Main.rand.Next(9) == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 55, 0f, 0f, 100, new Color(255, 148, 41), 1f);
            }
        }
    }
}