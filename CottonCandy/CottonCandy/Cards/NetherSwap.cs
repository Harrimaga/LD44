using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class NetherSwap : Card
    {
        public NetherSwap() : base(true, "Nether Swap", "Swaps the position of you and an enemy unit that is within " + CardBalance.NETHERSWAP_RANGE + " blocks", CardBalance.NETHERSWAP_COST, PurchaseMode.HP, "Cards/SilverEnemyCastCard")
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

            if (d > CardBalance.NETHERSWAP_RANGE)
            {
                return false;
            }

            if (Globals.grid.grid[x, y].unit == null || Globals.grid.grid[x, y].unit == Globals.CurrentPlayer)
            {
                return false;
            }

            //Globals.grid.grid[x, y].unit.ChangePosition((int)(playerPos.X * Globals.tileW), (int)(playerPos.Y * Globals.tileH));
            IUnit u = Globals.grid.grid[x, y].unit;
            Globals.grid.SwapPosition(u, Globals.CurrentPlayer);
            //Globals.CurrentPlayer.ChangePosition(x, y);
            //Globals.grid.grid[(int)playerPos.X, (int)playerPos.Y].unit = u;
            return true;
        }
    }
}
