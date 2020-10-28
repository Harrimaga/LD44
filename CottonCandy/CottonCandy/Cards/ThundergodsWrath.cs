using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class ThundergodsWrath : Card
    {
        public ThundergodsWrath() : base(false, "Thundergod's Wrath", "Deal " + CardBalance.THUNDERGODSWRATH_DAMAGE_LOW + "-" + CardBalance.THUNDERGODSWRATH_DAMAGE_HIGH + " damage to all enemies", CardBalance.THUNDERGODSWRATH_COST, PurchaseMode.ALLIES, "Cards/PlatinumSelfCastCard")
        {

        }

        public override bool Play()
        {
            foreach (var enemy in Globals.grid.units)
            {
                if (enemy is Enemy e)
                {
                    e.Damage(Globals.rng.Next(CardBalance.THUNDERGODSWRATH_DAMAGE_LOW, CardBalance.THUNDERGODSWRATH_DAMAGE_HIGH), "Player");
                }
            }
            return true;
        }

        public override bool Play(int x, int y)
        {
            return false;
        }

    }
}
