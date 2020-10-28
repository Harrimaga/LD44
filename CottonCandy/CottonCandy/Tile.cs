using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CottonCandy
{
    [Serializable]
    class Tile
    {

        private string tex;
        private int x, y;
        public TileType colidable;
        public IUnit unit = null;

        public Tile() { }

        public Tile(string texture, int x, int y, TileType colidable)
        {
            this.tex = texture;
            this.x = x;
            this.y = y;
            this.colidable = colidable;
        }

        public void Draw()
        {
            Globals.sb.Draw(LoadTex.LoadTexture(tex), new Vector2(x - Camera.camX, y - Camera.camY), Color.White);
            if (unit != null)
            {
                unit.Draw();
            }
        }

    }

    public enum TileType {
        SOLID,
        NOTSOLID,
        FINISH
    }

}
