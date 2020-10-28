using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class RayOfFrost : Card
    {

        public RayOfFrost() : base(true, "Ray of Frost", "Deal " + CardBalance.RAYOFFROST_DAMAGE + " damage to target creature and stuns them for " + CardBalance.RAYOFFROST_STUN_DURATION + " turn", CardBalance.RAYOFFROST_COST, PurchaseMode.ENEMIES, "Cards/EnemyCastCard")
        {

        }
        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            Globals.grid.DealDamage(x, y, CardBalance.RAYOFFROST_DAMAGE, "Player");
            if(Globals.grid.grid[x, y].unit != null)
            {
                Globals.grid.grid[x, y].unit.Stun(CardBalance.RAYOFFROST_STUN_DURATION);
            }
            return true;
        }


    }
}
