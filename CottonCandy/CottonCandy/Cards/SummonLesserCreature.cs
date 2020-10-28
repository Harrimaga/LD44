using CottonCandy.Allies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class SummonLesserCreature : Card
    {
        public SummonLesserCreature() : base(true,"Summon Lesser Creature", "Summons a Lesser creature to fight for you on the target location.",CardBalance.SUMMONLESSERCREATURE_COST,PurchaseMode.HP,"Cards/SilverUtilityCard")
        {
            
        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            if (Globals.grid.GetTile(x, y).unit != null || Globals.grid.GetTile(x, y).colidable != TileType.NOTSOLID)
            {
                return false;
            }
            LesserCreature lc = new LesserCreature();
            Globals.grid.AddUnitAtPosition(lc, x, y);
            return true;
        }

    }
}
