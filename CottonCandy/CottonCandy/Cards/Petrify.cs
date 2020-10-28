using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Petrify : Card
    {
        public Petrify() : base(true,"Petrify","Stuns all enemies in a " + CardBalance.PETRIFY_RANGE + "x" + CardBalance.PETRIFY_RANGE + " square", CardBalance.PETRIFY_COST, PurchaseMode.HP, "Cards/GoldEnemyAoECastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            int range = CardBalance.PETRIFY_RANGE / 2;
            for (int i = x - range; i < x + range; i++)
            {
                for (int j = y - range; j < y + range; j++)
                {
                    if (i <= Globals.grid.w && i > 0 && j <= Globals.grid.h && j > 0)
                    {
                        if(Globals.grid.GetTile(i,j).unit!=null)
                        {
                            Globals.grid.GetTile(i, j).unit.Stun(CardBalance.PETRIFY_DURATION);
                        }
                    }
                }
            }
            return true;
        }

    }
}
