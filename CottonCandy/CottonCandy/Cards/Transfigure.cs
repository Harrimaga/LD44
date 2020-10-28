using CottonCandy.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Transfigure : Card
    {
        public Transfigure() : base(false, "Transfigure", "Remain in place for " + CardBalance.TRANSFIGURE_DURATION + " turns and heal " + (int)(CardBalance.TRANSFIGURE_HEAL * 100) + "% of all damage taken in those turns", CardBalance.TRANSFIGURE_COST, PurchaseMode.HP, "Cards/SilverSelfCastCard")
        {

        }

        public override bool Play()
        {
            Globals.CurrentPlayer.AddEffect(new Transfigured());
            Globals.CurrentPlayer.Stun(CardBalance.TRANSFIGURE_DURATION);
            return true;
        }

        public override bool Play(int x, int y)
        {
            return false;
        }

    }
}
