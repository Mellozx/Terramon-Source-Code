using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Razorwing.Framework.Localisation;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terramon.UI.Moveset
{
    public class BattleMoveButton : UIElement
    {
        private Texture2D _texture = ModContent.GetTexture("Terramon/UI/Battling/InterfaceButton");

        private Texture2D _texture_hovered = ModContent.GetTexture("Terramon/UI/Battling/InterfaceButton_Hover");

        public float ImageScale = 1f;

        public float _visibilityActive = 1f;

        private UIText text;
        private SidebarClass type;
        public int PPLeft;
        public BaseMove Move
        {
            get => move;
            set
            {
                if(value == move)
                    return;
                move = value;

                if (move != null)
                {
                    MoveName = TerramonMod.Localisation.GetLocalisedString(move.MoveName);
                    TypeName = TerramonMod.Localisation.GetLocalisedString(move.MoveType.ToString());
                }
                else
                {
                    MoveName = TerramonMod.Localisation.GetLocalisedString("-");
                    TypeName = TerramonMod.Localisation.GetLocalisedString("???");
                }

                needUpdate = true;
            }
        }

        private ILocalisedBindableString MoveName, TypeName;
        private readonly bool leftSide;
        private bool needUpdate;


        public new Action<BaseMove> OnClick;

        public BattleMoveButton(BaseMove move, Vector2 pos)
        {
            this.move = move;
            if (move != null)
            {
                MoveName = TerramonMod.Localisation.GetLocalisedString(move.MoveName);
                TypeName = TerramonMod.Localisation.GetLocalisedString(move.MoveType.ToString());
            }
            else
            {
                MoveName = TerramonMod.Localisation.GetLocalisedString("-");
                TypeName = TerramonMod.Localisation.GetLocalisedString("???");
            }

            Left.Set(pos.X, 0);
            Top.Set(pos.Y, 0);
            Width.Set(_texture.Width, 0f);
            Height.Set(_texture.Height, 0f);
        }

        public override void OnInitialize()
        {
            text = new UIText(MoveName.Value, 0.45f, true);
            text.VAlign = 0.5f;
            text.HAlign = 0.5f;
            //text.Left.Set(ChatManager.GetStringSize(Main.fontMouseText, MoveName.Value, 1.2f));
            Append(text);

            if (move != null)
            {
                var texture = ModContent.FileExists($"Terramon/UI/Moveset/{move.MoveType}Type") ?
                    ModContent.GetTexture($"Terramon/UI/Moveset/{move.MoveType}Type") :
                    ModContent.GetTexture($"Terramon/UI/Moveset/EmptyType");
                type = new SidebarClass(texture, TypeName.Value);
                type.Top.Set(-texture.Height / 2, 0.5f);
                if (leftSide)
                    type.Left.Set(-texture.Width - 20, 1f);
                else
                    type.Left.Set(texture.Width + 20, 0f);
                //Append(type);
            }
            else
            {
                var texture = ModContent.GetTexture($"Terramon/UI/Moveset/EmptyType");
                type = new SidebarClass(texture, TypeName.Value);
                type.Top.Set(-texture.Height / 2, 0.5f);
                if (leftSide)
                    type.Left.Set(-texture.Width - 20, 1f);
                else
                    type.Left.Set(texture.Width + 20, 0f);
                //Append(type);
            }

            base.OnInitialize();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (IsMouseHovering)
            {
                spriteBatch.Draw(position: GetDimensions().Position() + _texture.Size() * (1f - ImageScale) / 2f, texture: _texture_hovered, sourceRectangle: null, color: Color.White * _visibilityActive, rotation: 0f, origin: Vector2.Zero, scale: ImageScale, effects: SpriteEffects.None, layerDepth: 0f);
            }
            else
            {
                spriteBatch.Draw(position: GetDimensions().Position() + _texture.Size() * (1f - ImageScale) / 2f, texture: _texture, sourceRectangle: null, color: Color.White * _visibilityActive, rotation: 0f, origin: Vector2.Zero, scale: ImageScale, effects: SpriteEffects.None, layerDepth: 0f);
            }
        }

        public bool lk;
        private BaseMove move;


        public override void Click(UIMouseEvent evt)
        {
            base.Click(evt);
            if(ContainsPoint(Main.MouseScreen))
                OnClick?.Invoke(move);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen)) Main.LocalPlayer.mouseInterface = true;

            if (needUpdate)
            {
                needUpdate = false;

                if (move != null)
                {
                    var texture = ModContent.FileExists($"Terramon/UI/Moveset/{move.MoveType}Type") ?
                        ModContent.GetTexture($"Terramon/UI/Moveset/{move.MoveType}Type") :
                        ModContent.GetTexture($"Terramon/UI/Moveset/EmptyType");
                    //RemoveChild(type);
                    type = new SidebarClass(texture, TypeName.Value);
                    type.Top.Set(-texture.Height / 2, 0.5f);
                    if (leftSide)
                        type.Left.Set(-texture.Width - 20, 1f);
                    else
                        type.Left.Set(texture.Width + 20, 0f);
                    //Append(type);
                }
                else
                {
                    var texture = ModContent.GetTexture($"Terramon/UI/Moveset/EmptyType");
                    //RemoveChild(type);
                    type = new SidebarClass(texture, TypeName.Value);
                    type.Top.Set(-texture.Height / 2, 0.5f);
                    if (leftSide)
                        type.Left.Set(-texture.Width - 20, 1f);
                    else
                        type.Left.Set(texture.Width + 20, 0f);
                    //Append(type);
                }

                type.HoverText = TypeName.Value;
                text.SetText(MoveName.Value);
                text.VAlign = 0.5f;
                text.HAlign = 0.5f;
            }

            type.HoverText = $"{TypeName.Value} PP: {PPLeft}/{move?.MaxPP}";
        }
    }
}
