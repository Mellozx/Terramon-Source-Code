using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terramon.Pokemon.FirstGeneration.Normal.Pidgeot
{
    public class PidgeotNPC : NotCatchablePKMNBirdFlying
    {
        public override Type HomeClass()
        {
            return typeof(Pidgeot);
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
            npc.gfxOffY = 0;
            return true;
        }
    }
}