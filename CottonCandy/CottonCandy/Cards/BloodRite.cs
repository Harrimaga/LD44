using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class BloodRite : Card
    {
        public BloodRite() : base(true, "Blood Rite", "You deal "+CardBalance.BLOODRITE_SELFDAMAGE+ " damage to yourself and " + CardBalance.BLOODRITE_DAMAGE + " damage to an enemy unit", CardBalance.BLOODRITE_COST, PurchaseMode.ENEMIES, "Cards/GoldEnemyCastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            Globals.CurrentPlayer.Damage(CardBalance.BLOODRITE_SELFDAMAGE, "Player");
            Globals.grid.DealDamage(x, y, CardBalance.BLOODRITE_DAMAGE, "Player");
            return true;
        }

    }
}
