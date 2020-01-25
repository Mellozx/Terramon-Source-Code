using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Terramon.Pokemon.FirstGeneration.Normal.Pidgey
{
    public class PidgeyNPC : ParentPokemonNPC_Caterpie
    {
        public override Type HomeClass() => typeof(Pidgey);

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
            npc.scale = 1f;
            npc.friendly = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.gfxOffY = 6;
            return true;
        }
    }
}