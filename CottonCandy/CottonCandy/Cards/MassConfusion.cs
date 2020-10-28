using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class MassConfusion : Card
    {
        public MassConfusion() : base(true, "Mass Confusion", "All enemies in a " + CardBalance.MASSCONFUSION_RANGE + "x" + CardBalance.MASSCONFUSION_RANGE + " square will move a random direction instead of their normal movement the next turn", CardBalance.MASSCONFUSION_COST, PurchaseMode.ENEMIES, "Cards/AoECastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            int range = CardBalance.MASSCONFUSION_RANGE / 2;
            for (int i = x - range; i <= x + range; i++)
            {
                for (int j = y - range; j <= y + range; j++)
                {
                    if (i >= Globals.grid.w || i < 0 || j >= Globals.grid.h || j < 0) { continue; }
                    if(Globals.grid.grid[i, j].unit != null && Globals.grid.grid[i, j].unit is Enemy)
                    {
                        ((Enemy)Globals.grid.grid[i, j].unit).SetConfused();
                    } 
                }
            }
            return true;
        }

    }
}
