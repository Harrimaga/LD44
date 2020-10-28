using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CottonCandy.Cards
{
    [Serializable]
    class Blink : Card
    {
        public Blink() : base(true, "Blink", "Teleport a short distance (up to "+CardBalance.BLINK_RANGE+" squares)", CardBalance.BLINK_COST, PurchaseMode.HP, "Cards/GoldUtilityCard")
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
            if (d > CardBalance.BLINK_RANGE)
            {
                return false;
            }
            if (Globals.grid.grid[x, y].unit != null || Globals.grid.grid[x, y].colidable != TileType.NOTSOLID)
            {
                return false;
            }
            Globals.grid.AddUnitAtPosition(Globals.CurrentPlayer, x, y);
            return true;
        }
    }
}
