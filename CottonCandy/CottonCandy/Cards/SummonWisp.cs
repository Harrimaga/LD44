using CottonCandy.Allies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class SummonWisp : Card
    {
        public SummonWisp() : base(true, "Summon Wisp", "Summons a healing wisp on the target location. The wisp heals all units for " + CardBalance.SUMMONWISP_HEAL + " every " + CardBalance.SUMMONWISP_TURNS + " turns", CardBalance.SUMMONWISP_COST, PurchaseMode.HP, "Cards/UtilityCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            if (Globals.grid.GetTile(x,y).unit!=null||Globals.grid.GetTile(x,y).colidable!=TileType.NOTSOLID)
            {
                return false;
            }
            Wisp lc = new Wisp();
            Globals.grid.AddUnitAtPosition(lc, x, y);
            return true;
        }

    }
}
