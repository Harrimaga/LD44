using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    class Button
    {
        public int x, y, width, height;
        protected Texture2D texture = null;
        protected string text = null;
        protected GameStates doOnlyIn = GameStates.MAINMENU;

        public Button(int x, int y, int width, int height, string graphicName)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            texture = LoadTex.LoadTexture(graphicName);
        }

        public Button(string text, int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.text = text;
        }

        public void Draw()
        {
            if (Globals.gameState == doOnlyIn)
            {
                if (texture != null)
                {
                    Globals.sb.Draw(texture, new Rectangle(x, y, width, height), Color.White);
                }
                if (text != null)
                {
                    Globals.sb.DrawString(Globals.bigfont, text, new Vector2(x, y), Color.White);
                }
            }
        }

        public bool MouseOver()
        {
            return new Rectangle(x, y, width, height).Contains(Globals.mouse);
        }

        public virtual void Click()
        {

        }

        public virtual void Update()
        {

        }

    }
}
