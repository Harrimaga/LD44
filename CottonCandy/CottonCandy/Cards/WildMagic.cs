using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class WildMagic : Card
    {
        public WildMagic() : base(true, "Wild Magic", "Hit an enemy within a " + CardBalance.WILDMAGIC_RANGE + " square range with 2 unstable magic bolts, each dealing " + CardBalance.WILDMAGIC_DAMAGE_LOW + "-" + CardBalance.WILDMAGIC_DAMAGE_HIGH + " damage", 5, PurchaseMode.ENEMIES, "Cards/EnemyCastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            if(Globals.grid.grid[x, y].unit == null)
            {
                return false;
            }
            if (x > Globals.CurrentPlayer.X - (CardBalance.WILDMAGIC_RANGE + 1) && x < Globals.CurrentPlayer.X + (CardBalance.WILDMAGIC_RANGE + 1) && y > Globals.CurrentPlayer.Y - (CardBalance.WILDMAGIC_RANGE + 1) && y < Globals.CurrentPlayer.Y + (CardBalance.WILDMAGIC_RANGE + 1))
            {
                Globals.grid.grid[x, y].unit.Damage(Globals.rng.Next(CardBalance.WILDMAGIC_DAMAGE_LOW, CardBalance.WILDMAGIC_DAMAGE_HIGH), "Player");
                Globals.grid.grid[x, y].unit.Damage(Globals.rng.Next(CardBalance.WILDMAGIC_DAMAGE_LOW, CardBalance.WILDMAGIC_DAMAGE_HIGH), "Player");
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
