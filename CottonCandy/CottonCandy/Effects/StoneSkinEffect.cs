using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Effects
{
    class StoneSkinEffect : Effect
    {

        public StoneSkinEffect() : base(CardBalance.STONESKIN_DURATION)
        {

        }

        public override void AddEffect()
        {
            Globals.CurrentPlayer.damageMultiplier *= (1 - CardBalance.STONESKIN_DAMAGE_REDUCTION);
        }

        public override void RemoveEffect()
        {
            Globals.CurrentPlayer.damageMultiplier /= (1 - CardBalance.STONESKIN_DAMAGE_REDUCTION);
        }
    }
}
