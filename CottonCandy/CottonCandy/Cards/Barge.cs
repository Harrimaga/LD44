using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Barge : Card
    {
        public Barge() : base(true, "Barge", " Barge into an enemy ("+ CardBalance.BARGE_RANGE + "sq range) and hit them for " + CardBalance.BARGE_DAMAGE + " damage", CardBalance.BARGE_COST, PurchaseMode.ALLIES, "Cards/PlatinumEnemyCastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            Vector2 playerPos = new Vector2(Globals.CurrentPlayer.x / Globals.tileW, Globals.CurrentPlayer.y / Globals.tileH);
            Vector2 blinkPos = new Vector2(x, y);

            float d = Vector2.Distance(playerPos, blinkPos);

            if (d > CardBalance.BARGE_RANGE)
            {
                return false;
            }

            if (Globals.grid.grid[x, y].unit != null || Globals.grid.grid[x, y].colidable != TileType.NOTSOLID)
            {
                return false;
            }

            Globals.grid.AddUnitAtPosition(Globals.CurrentPlayer,x,y);
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (i >= Globals.grid.w || i < 0 || j >= Globals.grid.h || j < 0 || Globals.grid.grid[i,j].unit == null || Globals.grid.grid[i, j].unit == Globals.CurrentPlayer) { continue; }
                    Globals.grid.DealDamage(i, j, CardBalance.BARGE_DAMAGE, "Player");
                    return true;
                }
            }

            return true;
        }

    }
}
