using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class DemonicPulse : Card
    {
        public DemonicPulse() : base(false, "Demonic Pulse", "Deal "+CardBalance.DEMONICPULSE_DAMAGE+" damage in an "+ CardBalance.DEMONICPULSE_RANGE+ "x" + CardBalance.DEMONICPULSE_RANGE + " square around you, while dealing "+CardBalance.DEMONICPULSE_SELFDAMAGE+" damage to yourself", CardBalance.DEMONICPULSE_COST, PurchaseMode.HP, "Cards/GoldSelfCastCard")
        {

        }

        public override bool Play()
        {
            int range = CardBalance.DEMONICPULSE_RANGE / 2;
            int X = Globals.CurrentPlayer.x / Globals.tileW;
            int Y = Globals.CurrentPlayer.y / Globals.tileH;
            for (int i = X - range; i <= X + range; i++)
            {
                for (int j = Y - range; j <= Y + range; j++)
                {
                    if (i <= Globals.grid.w && i >= 0 && j <= Globals.grid.h && j >= 0)
                    {
                        if (i != X || j != Y)
                        {
                            Globals.grid.DealDamage(i, j, CardBalance.DEMONICPULSE_DAMAGE, "Player");
                        }
                    }
                }
            }

            Globals.CurrentPlayer.Damage(CardBalance.DEMONICPULSE_SELFDAMAGE, "Player");
            return true;
        }

        public override bool Play(int x, int y)
        {
            return false;
        }

    }
}
