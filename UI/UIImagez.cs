using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;

namespace Terramon.UI
{
    // This UIHoverImageButton class inherits from UIImageButton. 
    // Inheriting is a great tool for UI design. 
    // By inheriting, we get the Image drawing, MouseOver sound, and fading for free from UIImageButton
    // We've added some code to allow the Button to show a text tooltip while hovered. 
    internal class UIImagez : UIImage
    {
        public Texture2D test1;
        public Texture2D test2;
        public Texture2D test3;
        public Texture2D test4;
        public Texture2D test5;
        public Texture2D test6;

        public UIImagez(Texture2D texture) : base(texture)
        {

        }
    }
}
