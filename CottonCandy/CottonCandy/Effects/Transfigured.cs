using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Effects
{
    class Transfigured : Effect
    {

        public int damageTaken;
        public Transfigured() : base(CardBalance.TRANSFIGURE_DURATION)
        {
            damageTaken = 0;
        }

        public override void AddEffect()
        {
            
        }

        public override void RemoveEffect()
        {
            Globals.CurrentPlayer.Heal((int)(damageTaken * CardBalance.TRANSFIGURE_HEAL));
            Console.WriteLine(damageTaken);
        }
    }


}
