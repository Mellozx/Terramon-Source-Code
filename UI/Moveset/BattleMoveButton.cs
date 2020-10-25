using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Razorwing.Framework.Localisation;
using Terramon.Pokemon.Moves;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terramon.UI.Moveset
{
    public class BattleMoveButton : UIPanel
    {
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

        public BattleMoveButton(BaseMove move, Vector2 pos, Vector2 size, bool leftSide = true)
        {
            this.move = move;
            this.leftSide = leftSide;
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
            Width.Set(size.X, 0);
            Height.Set(size.Y, 0);
        }

        public override void OnInitialize()
        {
            text = new UIText(MoveName.Value);
            text.Top.Set(0, 0.5f);
            text.Left.Set(-text.Width.Pixels/2, 0.5f);
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
                Append(type);
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
                Append(type);
            }

            base.OnInitialize();
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

            if (needUpdate)
            {
                needUpdate = false;

                if (move != null)
                {
                    var texture = ModContent.FileExists($"Terramon/UI/Moveset/{move.MoveType}Type") ?
                        ModContent.GetTexture($"Terramon/UI/Moveset/{move.MoveType}Type") :
                        ModContent.GetTexture($"Terramon/UI/Moveset/EmptyType");
                    RemoveChild(type);
                    type = new SidebarClass(texture, TypeName.Value);
                    type.Top.Set(-texture.Height / 2, 0.5f);
                    if (leftSide)
                        type.Left.Set(-texture.Width - 20, 1f);
                    else
                        type.Left.Set(texture.Width + 20, 0f);
                    Append(type);
                }
                else
                {
                    var texture = ModContent.GetTexture($"Terramon/UI/Moveset/EmptyType");
                    RemoveChild(type);
                    type = new SidebarClass(texture, TypeName.Value);
                    type.Top.Set(-texture.Height / 2, 0.5f);
                    if (leftSide)
                        type.Left.Set(-texture.Width - 20, 1f);
                    else
                        type.Left.Set(texture.Width + 20, 0f);
                    Append(type);
                }

                type.HoverText = TypeName.Value;
                text.SetText(MoveName.Value);
                text.Left.Set(-text.Width.Pixels / 2, 0.5f);
            }

            type.HoverText = $"{TypeName.Value} PP: {PPLeft}/{move?.MaxPP}";
        }
    }
}
