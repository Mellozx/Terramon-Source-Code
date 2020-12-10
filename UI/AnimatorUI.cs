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
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terramon.UI
{
    public class AnimatorUI : UIState
    {
        public static bool Visible = false;

        public UIPanel headingPanel, mainPanel, spriteContainer;
        public UIImage caughtBall, gender;
        public SummarySprite overworldSprite;

        public UIText lv, name;

        private string target = "Bulbasaur";

        public override void OnInitialize()
        {
            mainPanel = new UIPanel();
            mainPanel.BackgroundColor = new Color(28, 36, 66) * 0.95f;
            mainPanel.Width.Set(540, 0f);
            mainPanel.Height.Set(310, 0f);
            mainPanel.HAlign = 0.5f;
            mainPanel.VAlign = 0.5f;

            Append(mainPanel);

            spriteContainer = new UIPanel();
            spriteContainer.BackgroundColor = new Color(20, 25, 46) * 1f;
            spriteContainer.Width.Set(148, 0f);
            spriteContainer.Height.Set(110, 0f);
            spriteContainer.HAlign = 0.1f;
            spriteContainer.VAlign = 0.475f;

            Texture2D overworldTexture = ModContent.GetTexture($"Terramon/Pokemon/FirstGeneration/Normal/{target}/{target}");
            overworldSprite = new SummarySprite(overworldTexture);
            overworldSprite.HAlign = 0.5f;
            overworldSprite.VAlign = 0.5f;

            spriteContainer.Append(overworldSprite);

            name = new UIText("Charmeleon", 0.55f, true);
            name.Top.Set(-40, 0f);
            name.Left.Set(-2, 0f);

            lv = new UIText("Lv 16", 0.75f, false);

            spriteContainer.Append(name);
            spriteContainer.Append(lv);

            mainPanel.Append(spriteContainer);

            headingPanel = new UIPanel();
            headingPanel.BackgroundColor = new Color(63, 82, 151) * 1f;
            headingPanel.Width.Set(340, 0f);
            headingPanel.Height.Set(64, 0f);
            headingPanel.HAlign = 0.5f;
            headingPanel.Top.Set(-36, 0f);

            UIText text = new UIText("Pokémon Summary", 0.8f, true);
            text.HAlign = 0.5f;
            text.VAlign = 0.5f;
            headingPanel.Append(text);

            mainPanel.Append(headingPanel);

            Append(TerramonMod.ZoomAnimator = new Animator());

            base.OnInitialize();
        }

        public void SummaryData(PokemonData data)
        {
            
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

        public float HPBar1Fill
        {
            get => BattleMode.UI.HP1.HPBar.fill;
            set => BattleMode.UI.HP1.HPBar.fill = value;
        }

        public float HPBar2Fill
        {
            get => BattleMode.UI.HP2.HPBar.fill;
            set => BattleMode.UI.HP2.HPBar.fill = value;
        }

        public int HPBar1DisplayNumber
        {
            get => BattleMode.UI.HP1.displayHpNumberLerp;
            set => BattleMode.UI.HP1.displayHpNumberLerp = value;
        }

        public int HPBar2DisplayNumber
        {
            get => BattleMode.UI.HP2.displayHpNumberLerp;
            set => BattleMode.UI.HP2.displayHpNumberLerp = value;
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

        public static TransformSequence<T> HPBar1Fill<T>(this T drawable, float newValue, double duration = 0, Easing easing = Easing.None) where T : Animator =>
            drawable.TransformTo(nameof(drawable.HPBar1Fill), newValue, duration, easing);
        public static TransformSequence<T> HPBar1Fill<T>(this TransformSequence<T> t, float newValue, double duration = 0, Easing easing = Easing.None)
                  where T : Animator =>
                  t.Append(o => o.HPBar1Fill(newValue, duration, easing));
        public static TransformSequence<T> HPBar2Fill<T>(this T drawable, float newValue, double duration = 0, Easing easing = Easing.None) where T : Animator =>
            drawable.TransformTo(nameof(drawable.HPBar2Fill), newValue, duration, easing);
        public static TransformSequence<T> HPBar2Fill<T>(this TransformSequence<T> t, float newValue, double duration = 0, Easing easing = Easing.None)
                  where T : Animator =>
                  t.Append(o => o.HPBar2Fill(newValue, duration, easing));

        public static TransformSequence<T> HPBar1DisplayNumber<T>(this T drawable, int newValue, double duration = 0, Easing easing = Easing.None) where T : Animator =>
            drawable.TransformTo(nameof(drawable.HPBar1DisplayNumber), newValue, duration, easing);
        public static TransformSequence<T> HPBar1DisplayNumber<T>(this TransformSequence<T> t, int newValue, double duration = 0, Easing easing = Easing.None)
                  where T : Animator =>
                  t.Append(o => o.HPBar1DisplayNumber(newValue, duration, easing));
        public static TransformSequence<T> HPBar2DisplayNumber<T>(this T drawable, int newValue, double duration = 0, Easing easing = Easing.None) where T : Animator =>
            drawable.TransformTo(nameof(drawable.HPBar2DisplayNumber), newValue, duration, easing);
        public static TransformSequence<T> HPBar2DisplayNumber<T>(this TransformSequence<T> t, int newValue, double duration = 0, Easing easing = Easing.None)
                  where T : Animator =>
                  t.Append(o => o.HPBar2DisplayNumber(newValue, duration, easing));
    }
}
