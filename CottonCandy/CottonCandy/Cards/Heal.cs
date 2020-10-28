using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Heal : Card
    {
        public Heal() : base(false, "Heal", "Heal yourself for "+CardBalance.HEAL_HEAL+" HP", CardBalance.HEAL_COST, PurchaseMode.HP, "Cards/SilverSelfCastCard")
        {

        }

        public override bool Play()
        {
            Globals.CurrentPlayer.Heal(CardBalance.HEAL_HEAL);
            return true;
        }

        public override bool Play(int x, int y)
        {
            return false;
        }
    }
}
