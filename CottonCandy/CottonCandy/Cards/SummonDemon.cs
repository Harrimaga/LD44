using CottonCandy.Allies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class SummonDemon : Card
    {
        public SummonDemon() : base(true,"Summon Demon", "Lose " + CardBalance.SUMMONDEMON_SELFCOST + "HP and summon a Demon!", CardBalance.SUMMONDEMON_COST, PurchaseMode.ALLIES, "Cards/PlatinumUtilityCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            Globals.CurrentPlayer.Damage(CardBalance.SUMMONDEMON_SELFCOST, "Player");
            if (Globals.grid.GetTile(x, y).unit != null || Globals.grid.GetTile(x, y).colidable != TileType.NOTSOLID)
            {
                return false;
            }
            Demon lc = new Demon();
            Globals.grid.AddUnitAtPosition(lc, x, y);
            return true;
        }

    }
}
