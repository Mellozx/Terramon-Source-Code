using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Razorwing.Framework.Graphics;
using Razorwing.Framework.Graphics.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terramon.UI.SidebarParty;
using Terraria;
using Terraria.UI;

namespace Terramon.UI
{
    public class AnimatorUI : UIState
    {
        public static bool Visible = true;

        public override void OnInitialize()
        {
            Append(TerramonMod.ZoomAnimator = new Animator());

            base.OnInitialize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;
            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }

    public class Animator : Drawable
    {
        public float ZoomTarget
        {
            get => Main.GameZoomTarget;
            set => Main.GameZoomTarget = value;
        }
    }
    public static class AnimatorExtensions
    {
        public static TransformSequence<T> GameZoom<T>(this T drawable, float newValue, double duration = 0, Easing easing = Easing.None) where T : Animator =>
            drawable.TransformTo(nameof(drawable.ZoomTarget), newValue, duration, easing);
        public static TransformSequence<T> GameZoom<T>(this TransformSequence<T> t, float newValue, double duration = 0, Easing easing = Easing.None)
                  where T : Animator =>
                  t.Append(o => o.GameZoom(newValue, duration, easing));
    }
}
