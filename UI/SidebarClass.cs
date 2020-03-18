using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace Terramon.UI
{
    // This UIHoverImageButton class inherits from UIImageButton. 
    // Inheriting is a great tool for UI design. 
    // By inheriting, we get the Image drawing, MouseOver sound, and fading for free from UIImageButton
    // We've added some code to allow the Button to show a text tooltip while hovered. 
    internal class SidebarClass : UIImage
    {
        public Texture2D test1;
        public Texture2D test2;
        public Texture2D test3;
        public Texture2D test4;
        public Texture2D test5;
        public Texture2D test6;

        public SidebarClass tooltip1;
        public Texture2D tooltip2;
        public Texture2D tooltip3;
        public Texture2D tooltip4;
        public Texture2D tooltip5;
        public Texture2D tooltip6;

        internal string HoverText;

        public SidebarClass(Texture2D texture, string hoverText) : base(texture)
        {
            HoverText = hoverText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (IsMouseHovering)
            {
                Main.hoverItemName = HoverText;
                ImageScale = 1.2f;
            }
            else
            {
                ImageScale = 1f;
            }
            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime); // don't remove.



            // Checking ContainsPoint and then setting mouseInterface to true is very common. This causes clicks on this UIElement to not cause the player to use current items. 

            if (ContainsPoint(Main.MouseScreen))
            {

                Main.LocalPlayer.mouseInterface = true;

            }
        }
    }
}
