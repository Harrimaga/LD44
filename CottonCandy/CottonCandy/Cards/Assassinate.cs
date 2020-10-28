using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Assassinate : Card
    {
        public Assassinate() : base(true,"Assassinate","Deal "+ CardBalance.ASSASINATE_DAMAGE + " damage to target creature", 10, PurchaseMode.ENEMIES, "Cards/SilverEnemyCastCard")
        {

        }
        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            Globals.grid.DealDamage(x, y, CardBalance.ASSASINATE_DAMAGE, "Player");
            return true;
        }
    }
}
