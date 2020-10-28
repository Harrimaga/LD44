using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class HealMinorWounds : Card
    {
        public HealMinorWounds() : base(false, "Heal Minor Wounds", "Heal yourself for " + CardBalance.HEALMINORWOUNDS_HEAL + "HP", CardBalance.HEALMINORWOUNDS_COST, PurchaseMode.ENEMIES, "Cards/SelfCastCard")
        {

        }

        public override bool Play()
        {
            Globals.CurrentPlayer.Heal(CardBalance.HEALMINORWOUNDS_HEAL);
            return true;
        }
        public override bool Play(int x, int y)
        {
            return false;
        }
    }
}
