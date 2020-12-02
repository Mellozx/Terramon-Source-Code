using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Razorwing.Framework.Graphics;
using Razorwing.Framework.Graphics.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terramon.Pokemon;
using Terramon.UI.SidebarParty;
using Terraria;
using Terraria.ModLoader;
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
        public float ScreenPosXTarget
        {
            get => Main.screenPosition.X + (Main.screenWidth / 2);
            set => ModContent.GetInstance<TerramonMod>().battleCamera.X = value;
        }
        public float ScreenPosYTarget
        {
            get => Main.screenPosition.Y + (Main.screenHeight / 2);
            set => ModContent.GetInstance<TerramonMod>().battleCamera.Y = value;
        }

        public Vector2 ScreenPos
        {
            get => Main.screenPosition;
            set => TerramonMod.Instance.battleCamera = value;
        }
        
        public float ButtonMenuPanelX
        {
            get => BattleMode.UI.ButtonMenuPanel.Top.Pixels;
            set => BattleMode.UI.ButtonMenuPanel.Top.Pixels = value;
        }
    }
    public static class AnimatorExtensions
    {
        public static TransformSequence<T> GameZoom<T>(this T drawable, float newValue, double duration = 0, Easing easing = Easing.None) where T : Animator =>
            drawable.TransformTo(nameof(drawable.ZoomTarget), newValue, duration, easing);
        public static TransformSequence<T> GameZoom<T>(this TransformSequence<T> t, float newValue, double duration = 0, Easing easing = Easing.None)
                  where T : Animator =>
                  t.Append(o => o.GameZoom(newValue, duration, easing));
        public static TransformSequence<T> ScreenPosX<T>(this T drawable, float newValue, double duration = 0, Easing easing = Easing.None) where T : Animator =>
            drawable.TransformTo(nameof(drawable.ScreenPosXTarget), newValue, duration, easing);
        public static TransformSequence<T> ScreenPosX<T>(this TransformSequence<T> t, float newValue, double duration = 0, Easing easing = Easing.None)
                  where T : Animator =>
                  t.Append(o => o.ScreenPosX(newValue, duration, easing));
        public static TransformSequence<T> ScreenPosY<T>(this T drawable, float newValue, double duration = 0, Easing easing = Easing.None) where T : Animator =>
            drawable.TransformTo(nameof(drawable.ScreenPosYTarget), newValue, duration, easing);
        public static TransformSequence<T> ScreenPosY<T>(this TransformSequence<T> t, float newValue, double duration = 0, Easing easing = Easing.None)
                  where T : Animator =>
                  t.Append(o => o.ScreenPosY(newValue, duration, easing));

        public static TransformSequence<T> ScreenPos<T>(this T drawable, Vector2 newValue, double duration = 0, Easing easing = Easing.None) where T : Animator =>
            drawable.TransformTo(nameof(drawable.ScreenPos), newValue, duration, easing);
        public static TransformSequence<T> ScreenPos<T>(this TransformSequence<T> t, Vector2 newValue, double duration = 0, Easing easing = Easing.None)
            where T : Animator =>
            t.Append(o => o.ScreenPos(newValue, duration, easing));

        public static TransformSequence<T> ButtonMenuPanelX<T>(this T drawable, float newValue, double duration = 0, Easing easing = Easing.None) where T : Animator =>
            drawable.TransformTo(nameof(drawable.ButtonMenuPanelX), newValue, duration, easing);
        public static TransformSequence<T> ButtonMenuPanelX<T>(this TransformSequence<T> t, float newValue, double duration = 0, Easing easing = Easing.None)
                  where T : Animator =>
                  t.Append(o => o.ButtonMenuPanelX(newValue, duration, easing));
    }
}
