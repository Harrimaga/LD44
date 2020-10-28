using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Surge : Card
    {
        public Surge() : base(true, "Surge", "Move up to 1 square", CardBalance.SURGE_COST, PurchaseMode.ENEMIES, "Cards/UtilityCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            int playerY = Globals.CurrentPlayer.y / Globals.tileH;
            int playerX = Globals.CurrentPlayer.x / Globals.tileW;

            bool center = (x==playerX&&y==playerY);
            bool left = (x == playerX-1 && y == playerY);
            bool top = (x == playerX && y == playerY-1);
            bool right = (x == playerX+1 && y == playerY);
            bool bottom = (x == playerX && y == playerY+1);

            if((center||left||top||right||bottom) && Globals.grid.grid[x,y].unit == null)
            {
                Globals.grid.AddUnitAtPosition(Globals.CurrentPlayer, x, y);
                //Globals.CurrentPlayer.ChangePosition(x*Globals.tileW,y*Globals.tileH);
                return true;
            }
            return false;
        }
    }
}
