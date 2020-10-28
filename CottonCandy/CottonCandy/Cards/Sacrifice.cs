using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Sacrifice : Card
    {
        public Sacrifice() : base(true, "Sacrifice", "Deals " + CardBalance.SACRIFICE_DAMAGE + " damage to an enemy unit and heals you for " + CardBalance.SACRIFICE_HEAL + " health", CardBalance.SACRIFICE_COST, PurchaseMode.ENEMIES, "Cards/GoldEnemyCastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            Globals.grid.DealDamage(x, y, CardBalance.SACRIFICE_DAMAGE, "Player");
            Globals.CurrentPlayer.Heal(CardBalance.SACRIFICE_HEAL);
            return true;
        }

    }
}
