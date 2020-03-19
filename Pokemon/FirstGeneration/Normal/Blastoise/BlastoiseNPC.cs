using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terramon.Pokemon.FirstGeneration.Normal.Blastoise
{
    public class BlastoiseNPC : NotCatchablePKMN
    {
        public override Type HomeClass()
        {
            return typeof(Blastoise);
        }

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
    }
}