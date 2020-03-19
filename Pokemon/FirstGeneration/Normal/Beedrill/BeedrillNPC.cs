using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terramon.Pokemon.FirstGeneration.Normal.Beedrill
{
    public class BeedrillNPC : NotCatchablePKMNBirdFlying
    {
        public override Type HomeClass()
        {
            return typeof(Beedrill);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 30;
            npc.height = 28;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.gfxOffY = 0;
            return true;
        }
    }
}