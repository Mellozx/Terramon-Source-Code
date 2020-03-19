using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terramon.Pokemon.FirstGeneration.Normal.Gengar
{
    public class GengarNPC : NotCatchablePKMN
    {
        public override Type HomeClass()
        {
            return typeof(Gengar);
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
    }
}