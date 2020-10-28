using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Fireball : Card
    {
        public Fireball() : base(true, "Fireball", "Deal "+CardBalance.FIREBALL_DAMAGE+" damage in a "+CardBalance.FIREBALL_RANGE+"x"+CardBalance.FIREBALL_RANGE+" square", CardBalance.FIREBALL_COST, PurchaseMode.HP, "Cards/GoldEnemyAoECastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            int range = CardBalance.FIREBALL_RANGE / 2;
            for (int i = x-range; i <= x+range; i++)
            {
                for (int j = y-range; j <= y+range; j++)
                {
                    if (i >= Globals.grid.w || i < 0 || j >= Globals.grid.h || j < 0) { continue; }
                    Globals.grid.DealDamage(i, j, CardBalance.FIREBALL_DAMAGE, "Player");
                }
            }
            return true;
        }
    }
}
