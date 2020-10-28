using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class FurySwipes : Card
    {
        public FurySwipes() : base(true, "Fury Swipes", "Deal "+CardBalance.FURYSWIPES_DAMAGE+" damage to target creature, This card has a "+CardBalance.FURYSWIPES_CHANCE+"% chance to repeat this effect every time it triggers", CardBalance.FURYSWIPES_COST, PurchaseMode.ENEMIES, "Cards/SilverEnemyCastCard")
        {

        }
        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            if(Globals.grid.GetTile(x,y).unit==null||!(Globals.grid.GetTile(x, y).unit is Enemy))
            {
                return false;
            }
            do
            {
                Globals.grid.DealDamage(x, y, CardBalance.FURYSWIPES_DAMAGE, "Player");
            }
            while (Globals.rng.Next(100) < CardBalance.FURYSWIPES_CHANCE && !Globals.grid.GetTile(x,y).unit.IsDead());
            return true;
        }
    }
}
