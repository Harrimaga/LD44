using CottonCandy.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class StoneSkin : Card
    {
        public StoneSkin() : base(false, "Stone Skin", "Reduce incoming damage by " + (int)(CardBalance.STONESKIN_DAMAGE_REDUCTION * 100) + "%, to a minimum of 1 damage for " + CardBalance.STONESKIN_DURATION + " turns", CardBalance.STONESKIN_COST, PurchaseMode.ENEMIES, "Cards/SelfCastCard")
        {

        }

        public override bool Play()
        {
            Globals.CurrentPlayer.AddEffect(new StoneSkinEffect());
            return true;
        }

        public override bool Play(int x, int y)
        {
            return false;
        }

    }
}
