using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class HealingWord : Card
    {
        public HealingWord() : base(true, "Healing Word", "Heals an allied unit for (" + CardBalance.HEALINGWORD_LOWHEAL + "-" + CardBalance.HEALINGWORD_HIGHHEAL + ") hp", CardBalance.HEALINGWORD_COST, PurchaseMode.ENEMIES, "Cards/UtilityCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            if (Globals.grid.GetTile(x, y).unit != null && Globals.grid.GetTile(x, y).unit is Ally)
            {
                int amountOfHeal = Globals.rng.Next(CardBalance.HEALINGWORD_LOWHEAL, CardBalance.HEALINGWORD_HIGHHEAL + 1);
                Globals.grid.GetTile(x, y).unit.Heal(amountOfHeal);
                return true;
            }
            return false;

        }

    }
}
