using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Endeavor : Card
    {
        public Endeavor() : base(true, "Endeavor", "Sets target hp to your current hp", CardBalance.ENDEAVOR_COST, PurchaseMode.ALLIES, "Cards/PlatinumEnemyCastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            if(Globals.grid.grid[x, y].unit == null || !(Globals.grid.grid[x, y].unit is Enemy))
            {
                return false;
            }
            ((Enemy)Globals.grid.grid[x, y].unit).Health = Globals.CurrentPlayer.Health;
            return true;
        }

    }
}
