using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Explosion : Card
    {
        public Explosion() : base(true, "Explosion", "Deal between " + CardBalance.EXPLOSION_DICEAMOUNT + " and " + CardBalance.EXPLOSION_DICEAMOUNT * CardBalance.EXPLOSION_DICESIDES + " damage to any unit in a " + CardBalance.EXPLOSION_RANGE + "x" + CardBalance.EXPLOSION_RANGE + " square", CardBalance.EXPLOSION_COST, PurchaseMode.ALLIES, "Cards/PlatinumEnemyAoECastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            int range = CardBalance.EXPLOSION_RANGE / 2;
            for (int i = x - range; i <= x + range; i++)
            {
                for (int j = y - range; j <= y + range; j++)
                {
                    if (i <= Globals.grid.w && i > 0 && j <= Globals.grid.h && j > 0)
                    {
                        int dicerolls = 0;
                        for (int k = 0; k < CardBalance.EXPLOSION_DICEAMOUNT; k++)
                        {
                            dicerolls += Globals.rng.Next(1, CardBalance.EXPLOSION_DICESIDES + 1);
                        }
                        Globals.grid.DealDamage(i, j, dicerolls, "Player");
                    }
                }
            }
            return true;
        }

    }
}
